using System;
using System.Collections.Generic;
using Code.Data;
using Code.Data.Unit;
using Code.Data.Unit.Player;
using Code.Equipment;
using Code.Extension;
using Code.Profile.Models;
using Code.Unit;
using Code.Unit.Factories;
using Profile.Analytic;
using UniRx;


namespace Code.Profile
{
    public class ProfilePlayer : IExecute
    {
        private readonly DataSettings _dataSettings;
        private readonly MvcModels _models;
        private bool _isPlayerDirty = false;
        
        
        public Guid Id => Guid.Empty;
        public bool IsOn => true;
        
        public MvcModels Models => _models;
        public DataSettings Settings => _dataSettings;
        public IAnalyticTools AnalyticTools { get; }
        public CommandManager CommandManager { get; }
        public CharacterFabric CharacterFabric { get; }
        public ReactiveProperty<GameState> CurrentState { get; }
        public IPlayerView CurrentPlayer { get; set; }
        public UiWindowAfterStart WindowAfterStart { get; set; }
        public ReactiveProperty<InfoAboutCharacter> InfoAboutCurrentPlayer { get; }
        public Action BuildCharacter { get; private set; }
        public Action RebuildCurrentCharacter { get; private set; }
        
        public Action OnCharacterBuildIsComplete = () => { };
        public Action<CharacterSettings> RebuildCharacter { get; private set; }
        
        public bool IsPlayerDirty
        {
            get => _isPlayerDirty;
            set => _isPlayerDirty = value;
        }
        
#if UNITY_EDITOR
        public Action<IPlayerView> ChangePlayer_FOR_DEBUG_DONT_USE;
#endif


        public ProfilePlayer(CommandManager commandManager, DataSettings dataSettings, MvcModels models,
            IAnalyticTools analyticTools)
        {
            _dataSettings = dataSettings;
            _models = models;
            CommandManager = commandManager;
            CurrentState = new ReactiveProperty<GameState>();
            InfoAboutCurrentPlayer = new ReactiveProperty<InfoAboutCharacter>(new InfoAboutCharacter(null));
            AnalyticTools = analyticTools;

            BuildCharacter += OnBuildCharacter;
            RebuildCurrentCharacter += OnRebuildCurrentCharacter;
            RebuildCharacter += OnRebuildCharacter;

            CharacterFabric = ConstructCharacterFabric();
        }
        
        
        public void Execute(float deltaTime)
        {
            if (IsPlayerDirty)
            {
                OnRebuildCurrentCharacter();
                IsPlayerDirty = false;
            }
        }

        private CharacterFabric ConstructCharacterFabric()
        {
            //создание GameObject игрока и добавление компонентов
            var playerFactory = new PlayerFactory(Settings.CharacterData);
            var levelFactory = new LevelFactory(Settings.UnitLevelData);
            var classesFactory = new CharacterClassesFactory(Settings.PlayerClassesData);
            var resourceFactory = new ResourceFactory(Settings.PlayerClassesData);
            var characteristicsFactory = new CharacteristicsFactory(Settings.PlayerClassesData);
            var healthFactory = new HealthFactory(Settings.PlayerClassesData);
            var visionFactory = new VisionFactory(Settings.PlayerClassesData);
            var reputationFactory = new ReputationFactory();
            var equipmentFactory = new EquipmentFactory();
            var inventoryFactory = new InventoryFactory();

            //контроллер активного персонажа (отвечает за модификацию внешного вида, одетых вещей в реалтайме)
            return new CharacterFabric(
                playerFactory,
                classesFactory,
                levelFactory,
                resourceFactory,
                characteristicsFactory,
                healthFactory,
                visionFactory,
                reputationFactory,
                equipmentFactory,
                inventoryFactory);
        }

        private void OnBuildCharacter()
        {
            CurrentPlayer =
                CharacterFabric.CreatePlayer(
                    Settings.PlayerData.ListCharacters[Settings.PlayerData._numberActiveCharacter]);
            OnRebuildCurrentCharacter();
            InfoAboutCurrentPlayer.Value = new InfoAboutCharacter(CurrentPlayer);

#if UNITY_EDITOR
            ChangePlayer_FOR_DEBUG_DONT_USE?.Invoke(CurrentPlayer);
#endif
        }


        private void OnRebuildCurrentCharacter()
        {
            OnRebuildCharacter(Settings.PlayerData.ListCharacters[Settings.PlayerData._numberActiveCharacter]);
        }

        private void OnRebuildCharacter(CharacterSettings value)
        {
            Dbg.Log($"RebuildCharacter");
            CharacterFabric.RebuildCharacter(CurrentPlayer, value);
            InfoAboutCurrentPlayer.Value = new InfoAboutCharacter(CurrentPlayer);
            OnCharacterBuildIsComplete?.Invoke();
#if UNITY_EDITOR
            ChangePlayer_FOR_DEBUG_DONT_USE?.Invoke(CurrentPlayer);
#endif
        }

        ~ProfilePlayer()
        {
            BuildCharacter = null;
            RebuildCurrentCharacter = null;
            RebuildCharacter = null;
            OnCharacterBuildIsComplete = null;
        }
    }
}