using Code.Data;
using Code.Data.Unit;
using Code.Data.Unit.Player;
using Code.Extension;
using Code.GameCamera;
using Code.Profile;
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
            var cameraController = new CameraController(settings.CameraSettings);
            var profilePlayer = new ProfilePlayer(commandManager, settings, models, new UnityAnalyticTools(), cameraController);
            profilePlayer.CurrentState.Value = _gameStateAfterStart;
            profilePlayer.WindowAfterStart = _showWindowAfterStart;
            _mainController = new MainController(_controllers, _placeForUi, profilePlayer);
        }

        private MvcModels LoadAllModels()
        {
            var result = new MvcModels();
            result.DungeonGeneratorModel = Resources.Load<DungeonGeneratorModel>("Models/DungeonGeneratorModel");
            Dbg.Log($"{StringManager.RESULT_OF_LOADING_DATA_MODEL} - {nameof(DungeonGeneratorModel)}:{result.DungeonGeneratorModel}");
            return result;
        }

        private DataSettings LoadAllResources()
        {
            var settings = new DataSettings();
            settings.CharacterData = Resources.Load<CharacterData>("Configs/Config_CharacterData");
            Dbg.Log($"{StringManager.RESULF_OF_LOADING_RESOURCES} - {nameof(CharacterData)}:{settings.CharacterData}");
            settings.UnitLevelData = Resources.Load<UnitLevelData>("Configs/Config_UnitLevelData");
            Dbg.Log($"{StringManager.RESULF_OF_LOADING_RESOURCES} - {nameof(UnitLevelData)}:{settings.UnitLevelData}");
            settings.PlayerClassesData = Resources.Load<PlayerClassesData>("Configs/Config_PlayerClassesData");
            Dbg.Log($"{StringManager.RESULF_OF_LOADING_RESOURCES} - {nameof(PlayerClassesData)}:{settings.PlayerClassesData}");
            settings.PlayerData = Resources.Load<PlayerData>("Configs/Config_PlayerData");
            Dbg.Log($"{StringManager.RESULF_OF_LOADING_RESOURCES} - {nameof(PlayerData)}:{settings.PlayerData}");
            settings.CameraSettings = Resources.Load<CameraSettings>("Configs/Config_CameraSettings");
            Dbg.Log($"{StringManager.RESULF_OF_LOADING_RESOURCES} - {nameof(CameraSettings)}:{settings.CameraSettings}");
            settings.UiViews = Resources.Load<UiViewsData>("Configs/Config_UiViewsData");
            Dbg.Log($"{StringManager.RESULF_OF_LOADING_RESOURCES} - {nameof(UiViewsData)}:{settings.UiViews}");
            // settings.EnemiesData = Resources.Load<EnemiesData>("Configs/EnemiesData_Simple");
            // Dbg.Log($"{StringManager.RESULF_OF_LOADING_RESOURCES} - {nameof(EnemiesData)}:{_enemiesData}");
            settings.DungeonGeneratorData = Resources.Load<DungeonGeneratorData>("Configs/Config_DungeonData");
            Dbg.Log($"{StringManager.RESULF_OF_LOADING_RESOURCES} - {nameof(DungeonGeneratorData)}:{settings.DungeonGeneratorData}");
            // settings.EcsBattleData = Resources.Load<EcsBattleData>("Configs/EcsBattleData");
            // Dbg.Log($"{StringManager.RESULF_OF_LOADING_RESOURCES} - {nameof(EcsBattleData)}:{_ecsBattleData}");
            // settings.BattleInputData = Resources.Load<BattleInputData>("Configs/BattleInputData");
            // Dbg.Log($"{StringManager.RESULF_OF_LOADING_RESOURCES} - {nameof(BattleInputData)}:{_battleInputData}");
            // settings.BattleSettingsData = Resources.Load<BattleSettingsData>("Configs/BattleSettingsData");
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