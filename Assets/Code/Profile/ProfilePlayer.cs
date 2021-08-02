using System.Linq;
using Code.Data;
using Code.Data.Unit;
using Code.Data.Unit.Player;
using Code.Extension;
using Code.GameCamera;
using Code.Profile.Models;
using Code.UI.Adventure;
using Code.Unit;
using Code.Unit.Factories;
using Profile.Analytic;
using UniRx;


namespace Code.Profile
{
    public class ProfilePlayer
    {
        private readonly DataSettings _dataSettings;
        private readonly MvcModels _models;
        private readonly CameraController _cameraController;
        public MvcModels Models => _models;
        public DataSettings Settings => _dataSettings;
        public IAnalyticTools AnalyticTools { get; }
        public CommandManager CommandManager { get; }
        public CharacterFabric CharacterFabric { get; }
        public ReactiveProperty<GameState> CurrentState { get; }
        public IPlayerView CurrentPlayer { get; set; }
        public UiWindowAfterStart WindowAfterStart { get; set; }
        public ReactiveProperty<InfoAboutCharacter> InfoAboutCurrentPlayer { get; }

        public ProfilePlayer(CommandManager commandManager, DataSettings dataSettings, MvcModels models,
            IAnalyticTools analyticTools,
            CameraController cameraController)
        {
            _dataSettings = dataSettings;
            _models = models;
            _cameraController = cameraController;
            CommandManager = commandManager;
            CurrentState = new ReactiveProperty<GameState>();
            InfoAboutCurrentPlayer = new ReactiveProperty<InfoAboutCharacter>(new InfoAboutCharacter(null));
            AnalyticTools = analyticTools;

            CharacterFabric = ConstructCharacterFabric();
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

            //контроллер активного персонажа (отвечает за модификацию внешного вида, одетых вещей в реалтайме)
            return new CharacterFabric(
                playerFactory,
                classesFactory,
                levelFactory,
                resourceFactory,
                characteristicsFactory,
                healthFactory);
        }
        
        public void BuildPlayer()
        {
            CurrentPlayer =
                CharacterFabric.CreatePlayer(
                    Settings.PlayerData.ListCharacters[Settings.PlayerData._numberActiveCharacter]);
            RebuildCurrentCharacter();
            InfoAboutCurrentPlayer.Value = new InfoAboutCharacter(CurrentPlayer);
        }
        
        public void RebuildCurrentCharacter()
        {
            RebuildCharacter(Settings.PlayerData.ListCharacters[Settings.PlayerData._numberActiveCharacter]);
        }

        public void RebuildCharacter(CharacterSettings value)
        {
            Dbg.Log($"RebuildCharacter");
            CharacterFabric.RebuildCharacter(CurrentPlayer, value);
            InfoAboutCurrentPlayer.Value = new InfoAboutCharacter(CurrentPlayer);
        }
    }
}