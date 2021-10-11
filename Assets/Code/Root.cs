using Code.Data;
using Code.Data.Dungeon;
using Code.Data.Unit;
using Code.Data.Unit.Enemy;
using Code.Data.Unit.Player;
using Code.Extension;
using Code.Profile;
using Code.Profile.Models;
using Code.Unit;
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
        private ProfilePlayer _profilePlayer;
#if UNITY_EDITOR
        [SerializeField]
        [Header("DON SET VALUE - IT`S FOR DEBUG, LINK to PlayerChar")]
        private GameObject _characterPlayer;

#endif

        private void Awake()
        {
            var settings = LoadAllResources();
            var models = LoadAllModels();
            _controllers = new Controllers();
            var commandManager = new CommandManager();
            _profilePlayer = new ProfilePlayer(commandManager, settings, models, new UnityAnalyticTools());
            _profilePlayer.CurrentState.Value = _gameStateAfterStart;
            _profilePlayer.WindowAfterStart = _showWindowAfterStart;
            _mainController = new MainController(_controllers, _placeForUi, _profilePlayer);
            _controllers.Add(_profilePlayer);
            _controllers.Init();
            
#if UNITY_EDITOR
            _profilePlayer.ChangePlayer_FOR_DEBUG_DONT_USE += ChangePlayerForDebugDontUse;
            _characterPlayer = _profilePlayer.CurrentPlayer.Transform.gameObject;
#endif
        }

#if UNITY_EDITOR
        private void ChangePlayerForDebugDontUse(IPlayerView obj)
        {
            _characterPlayer = _profilePlayer.CurrentPlayer.Transform.gameObject;
        }
#endif
        
        private MvcModels LoadAllModels()
        {
            var result = new MvcModels();
            result.DungeonGeneratorModel = ResourceLoader.LoadModel<DungeonGeneratorModel>();
            result.FightModel = ResourceLoader.LoadModel<FightDungeonModel>();
            result.EnemiesLevelModel = ResourceLoader.LoadModel<EnemiesLevelModel>();
            result.InOutControlFightModel = ResourceLoader.LoadModel<InOutControlFightModel>();
            return result;
        }

        private DataSettings LoadAllResources() {
            var settings = new DataSettings();
            settings.CharacterData = ResourceLoader.LoadConfig<CharacterData>();
            settings.UnitLevelData = ResourceLoader.LoadConfig<UnitLevelData>();
            settings.PlayerClassesData = ResourceLoader.LoadConfig<PlayerClassesData>();
            settings.PlayerData = ResourceLoader.LoadConfig<PlayerData>();
            settings.CameraSettings = ResourceLoader.LoadConfig<CameraSettings>();
            settings.UiViews = ResourceLoader.LoadConfig<UiViewsData>();
            settings.EnemiesData = ResourceLoader.LoadConfig<EnemiesData>();
            // Dbg.Log($"{StringManager.RESULF_OF_LOADING_RESOURCES} - {nameof(EnemiesData)}:{_enemiesData}");
            settings.DungeonGeneratorData = ResourceLoader.LoadConfig<DungeonGeneratorData>();
            settings.EcsBattleData = ResourceLoader.LoadConfig<EcsBattleData>();
            // Dbg.Log($"{StringManager.RESULF_OF_LOADING_RESOURCES} - {nameof(EcsBattleData)}:{_ecsBattleData}");
            settings.FightInputData = ResourceLoader.LoadConfig<FightInputData>();
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