using System;
using Code.Data;
using Code.Data.Unit;
using Code.Data.Unit.Player;
using Code.Extension;
using Code.GameCamera;
using Code.Unit;
using Profile.Analytic;
using UniRx;


namespace Code.Profile
{
    public class ProfilePlayer
    {
        private readonly DataSettings _dataSettings;
        private readonly CameraController _cameraController;

        public DataSettings Settings => _dataSettings;
        public IAnalyticTools AnalyticTools { get; }
        public CommandManager CommandManager { get; }
        public CharacterFabric CharacterFabric { get; }
        public ReactiveProperty<GameState> CurrentState { get; }
        public IPlayerView CurrentPlayer { get; set; }
        public UiWindowAfterStart WindowAfterStart { get; set; }
        public ReactiveProperty<InfoAboutCharacter> InfoAboutCurrentPlayer { get; }

        public ProfilePlayer(CommandManager commandManager, DataSettings dataSettings, IAnalyticTools analyticTools,
            CameraController cameraController)
        {
            _dataSettings = dataSettings;
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

            //контроллер активного персонажа (отвечает за модификацию внешного вида, одетых вещей в реалтайме)
            return new CharacterFabric(playerFactory, classesFactory, levelFactory);
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

        public void BuildPlayer()
        {
            CurrentPlayer =
                CharacterFabric.CreatePlayer(
                    Settings.PlayerData.ListCharacters[Settings.PlayerData._numberActiveCharacter]);
            RebuildCurrentCharacter();
            InfoAboutCurrentPlayer.Value = new InfoAboutCharacter(CurrentPlayer);
        }
    }
}