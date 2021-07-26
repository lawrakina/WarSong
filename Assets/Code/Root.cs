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

        private void Awake()
        {
            var settings = LoadAllResources();
            var commandManager = new CommandManager();
            var cameraController = new CameraController(settings.CameraSettings);
            var profilePlayer = new ProfilePlayer(commandManager, settings, new UnityAnalyticTools(), cameraController);
            profilePlayer.CurrentState.Value = _gameStateAfterStart;
            profilePlayer.WindowAfterStart = _showWindowAfterStart;
            _mainController = new MainController(_placeForUi, profilePlayer);
        }

        private DataSettings LoadAllResources()
        {
            var settings = new DataSettings();
            settings.CharacterData = Resources.Load<CharacterData>("Configs/Config_CharacterData");
            Dbg.Log($"Start resource load data - CharacterData:{settings.CharacterData}");
            settings.UnitLevelData = Resources.Load<UnitLevelData>("Configs/Config_UnitLevelData");
            Dbg.Log($"Start resource load data - UnitLevelData:{settings.UnitLevelData}");
            settings.PlayerClassesData = Resources.Load<PlayerClassesData>("Configs/Config_PlayerClassesData");
            Dbg.Log($"Start resource load data - PlayerClassesData:{settings.PlayerClassesData}");
            settings.PlayerData = Resources.Load<PlayerData>("Configs/Config_PlayerData");
            Dbg.Log($"Start resource load data - PlayerData:{settings.PlayerData}");
            settings.CameraSettings = Resources.Load<CameraSettings>("Configs/Config_CameraSettings");
            Dbg.Log($"Start resource load data - CameraSettingsInBattle:{settings.CameraSettings}");
            settings.UiViews = Resources.Load<UiViewsData>("Configs/Config_UiViewsData");
            Dbg.Log($"Start resource load data - UiViewsData:{settings.UiViews}");
            // settings.EnemiesData = Resources.Load<EnemiesData>("Configs/EnemiesData_Simple");
            // Dbg.Log($"Start resource load data - EnemiesData:{_enemiesData}");
            // settings.GeneratorData = Resources.Load<DungeonGeneratorData>("Configs/DungeonData");
            // Dbg.Log($"Start resource load data - DungeonGeneratorData:{_generatorData}");
            // settings.EcsBattleData = Resources.Load<EcsBattleData>("Configs/EcsBattleData");
            // Dbg.Log($"Start resource load data - EcsBattleData:{_ecsBattleData}");
            // settings.BattleInputData = Resources.Load<BattleInputData>("Configs/BattleInputData");
            // Dbg.Log($"Start resource load data - BattleInputData:{_battleInputData}");
            // settings.BattleSettingsData = Resources.Load<BattleSettingsData>("Configs/BattleSettingsData");
            // Dbg.Log($"Start resource load data - BattleSettingsData:{_battleSettingsData}");

            LayerManager.EnemyLayer = LayerMask.NameToLayer(StringManager.ENEMY_LAYER);
            LayerManager.PlayerLayer = LayerMask.NameToLayer(StringManager.PLAYER_LAYER);
            LayerManager.PlayerAttackLayer = LayerMask.NameToLayer(StringManager.PLAYER_ATTACK_LAYER);
            LayerManager.EnemyAttackLayer = LayerMask.NameToLayer(StringManager.ENEMY_ATTACK_LAYER);
            LayerManager.EnemyAndPlayerAttackLayer = LayerMask.NameToLayer(StringManager.ENEMY_AND_PLAYER_ATTACK_LAYER);

            return settings;
        }

        protected void OnDestroy()
        {
            _mainController?.Dispose();
        }
    }
}