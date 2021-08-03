using Code.Data;
using Code.Data.Dungeon;
using Code.Data.Unit;
using Code.Data.Unit.Player;
using Code.Extension;
using Code.GameCamera;
using Code.Profile;
using Code.Profile.Models;
using Profile.Analytic;
using UnityEngine;


namespace Code
{
    public class Root : MonoBehaviour
    {
        [SerializeField] private Transform _placeForUi;

        [SerializeField] private GameState _gameStateAfterStart = GameState.ListOfCharacter;

        [SerializeField] private UiWindowAfterStart _showWindowAfterStart = UiWindowAfterStart.Adventure;

        private MainController _mainController;
        private Controllers _controllers;

        private void Awake()
        {
            var settings = LoadAllResources();
            var models = LoadAllModels();
            _controllers = new Controllers();
            var commandManager = new CommandManager();
            var profilePlayer = new ProfilePlayer(commandManager, settings, models, new UnityAnalyticTools());
            profilePlayer.CurrentState.Value = _gameStateAfterStart;
            profilePlayer.WindowAfterStart = _showWindowAfterStart;
            _mainController = new MainController(_controllers, _placeForUi, profilePlayer);
            _controllers.Init();
        }

        private MvcModels LoadAllModels()
        {
            var result = new MvcModels();
            result.DungeonGeneratorModel = ResourceLoader.LoadModel<DungeonGeneratorModel>();
            result.FightModel = ResourceLoader.LoadModel<FightDungeonModel>();
            result.EnemiesLevelModel = ResourceLoader.LoadModel<EnemiesLevelModel>();
            result.InOutControlFightModel = ResourceLoader.LoadModel<InOutControlFightModel>();
            return result;
        }

        private DataSettings LoadAllResources()
        {
            var settings = new DataSettings();
            settings.CharacterData = ResourceLoader.LoadConfig<CharacterData>();
            settings.UnitLevelData = ResourceLoader.LoadConfig<UnitLevelData>();
            settings.PlayerClassesData = ResourceLoader.LoadConfig<PlayerClassesData>();
            settings.PlayerData = ResourceLoader.LoadConfig<PlayerData>();
            settings.CameraSettings = ResourceLoader.LoadConfig<CameraSettings>();
            settings.UiViews = ResourceLoader.LoadConfig<UiViewsData>();
            // settings.EnemiesData = ResourceLoader.LoadConfig<EnemiesData>("Configs/EnemiesData_Simple");
            // Dbg.Log($"{StringManager.RESULF_OF_LOADING_RESOURCES} - {nameof(EnemiesData)}:{_enemiesData}");
            settings.DungeonGeneratorData = ResourceLoader.LoadConfig<DungeonGeneratorData>();
            settings.EcsBattleData = ResourceLoader.LoadConfig<EcsBattleData>();
            // Dbg.Log($"{StringManager.RESULF_OF_LOADING_RESOURCES} - {nameof(EcsBattleData)}:{_ecsBattleData}");
            // settings.BattleInputData = ResourceLoader.LoadConfig<BattleInputData>("Configs/BattleInputData");
            // Dbg.Log($"{StringManager.RESULF_OF_LOADING_RESOURCES} - {nameof(BattleInputData)}:{_battleInputData}");
            // settings.BattleSettingsData = ResourceLoader.LoadConfig<BattleSettingsData>("Configs/BattleSettingsData");
            // Dbg.Log($"{StringManager.RESULF_OF_LOADING_RESOURCES} - {nameof(BattleSettingsData)}:{_battleSettingsData}");

            LayerManager.EnemyLayer = LayerMask.NameToLayer(StringManager.ENEMY_LAYER);
            LayerManager.PlayerLayer = LayerMask.NameToLayer(StringManager.PLAYER_LAYER);
            LayerManager.PlayerAttackLayer = LayerMask.NameToLayer(StringManager.PLAYER_ATTACK_LAYER);
            LayerManager.EnemyAttackLayer = LayerMask.NameToLayer(StringManager.ENEMY_ATTACK_LAYER);
            LayerManager.EnemyAndPlayerAttackLayer = LayerMask.NameToLayer(StringManager.ENEMY_AND_PLAYER_ATTACK_LAYER);

            return settings;
        }

        private void Update()
        {
            var deltaTime = Time.deltaTime;
            _controllers.Execute(deltaTime);
        }

        private void LateUpdate()
        {
            var deltaTime = Time.deltaTime;
            _controllers.LateExecute(deltaTime);
        }

        private void FixedUpdate()
        {
            var deltaTime = Time.fixedDeltaTime;
            _controllers.FixedExecute(deltaTime);
        }

        protected void OnDestroy()
        {
            _mainController?.Dispose();
        }
    }
}