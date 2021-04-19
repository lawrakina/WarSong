using System;
using Windows;
using Battle;
using CharacterCustomizing;
using Controller.Model;
using Data;
using Enums;
using Extension;
using Gui.Battle;
using Models;
using UniRx;
using Unit;
using Unit.Cameras;
using Unit.Enemies;
using Unit.Player;
using UnityEngine;


namespace Controller
{
    public class MainController : MonoBehaviour
    {
        #region Fields

        private CompositeDisposable _subscriptions;
        private Controllers _controllers;

        [Header("Ui")]
        [SerializeField]
        private UiWindows _uiWindows;

        [Header("Windows")]
        [SerializeField]
        private SceneWindows _windows;


        private CharacterData _characterData;
        private PlayerLevelData _playerLevelData;
        private PlayerClassesData _playerClassesData;
        private PlayerData _playerData;

        private EnemiesData _enemiesData;

        // private EnemyClassesData _enemyClassesData;
        private DungeonGeneratorData _generatorData;
        private EcsBattleData _ecsBattleData;
        private BattleInputData _battleInputData;
        private CameraSettingsInBattle _cameraSettings;
        private GeneratorDungeon _generatorDungeon;
        private BattleSettingsData _battleSettingsData;


        [Header("Plz do not set value!!! Its Simple access")]
        [SerializeField]
        public PlayerView _player;

        private CommandManager _commandManager;
        private CursorParticleSystemController _cursorParticleSystemController;

        #endregion


        //ДОДЕЛАТЬ БЛЯТЬ !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!outLine


        #region UnityMethods

        private void Awake()
        {
            GlobalLinks.SetLinkToRoot(this);
            LoadAllResources();
            _subscriptions = new CompositeDisposable();
            _controllers = new Controllers();
            _commandManager = new CommandManager(_uiWindows, _windows);

            _cursorParticleSystemController = new CursorParticleSystemController(_uiWindows.RootCanvas);
            _controllers.Add(_cursorParticleSystemController);

            var playerFactory = new PlayerFactory(_characterData,
                new PlayerCustomizerCharacter(_characterData),
                new PlayerLevelInitialization(_playerLevelData),
                new PlayerClassesInitialization(_playerClassesData));

            var generatorDungeonModel = new GeneratorDungeonModel(_windows, _generatorData);
            var generatorDungeon = new GeneratorDungeon(generatorDungeonModel);
            _commandManager.GeneratorDungeon = generatorDungeon;

            var listCharacterModel = new ListCharacterModel(_playerData, playerFactory);
            var listOfCharactersController = new ListOfCharactersController(listCharacterModel, _commandManager);
            _commandManager.ListOfCharacters = listOfCharactersController;
            _commandManager.ChangePlayer.Subscribe(value => { _player = (PlayerView) value; }).AddTo(_subscriptions);
            _commandManager.ChangePlayer.Subscribe(value => { }).AddTo(_subscriptions);

            var listOfPositionCharInMenuModel = new ListOfPositionCharInMenuModel(_windows);
            var listOfPositionCharInMenuController =
                new ListOfPositionCharInMenuController(listOfPositionCharInMenuModel, _commandManager);

            var playerModel = new BattlePlayerModel();
            var battleModel = new BattleProgressModel();
            var targetModel = new BattleTargetModel();

            _uiWindows.FightUiWindow.SetModels(battleModel, playerModel, targetModel);

            var fightCameraFactory = new CameraFactory(_cameraSettings);
            var fightCamera = fightCameraFactory.CreateCamera(_windows.RootBattleCamera);

            var battleInputControlsInitialization =
                new BattleInputControlsInitialization(_battleInputData, _uiWindows.FightUiWindow.transform);
            var enemyClassesInitialization = new EnemyClassesInitialization( /*_enemyClassesData,*/);
            var enemyFactory = new EnemyFactory(enemyClassesInitialization);
            var healthBarFactory = new HealthBarFactory();
            var enemiesInitialization = new EnemiesInitialization(_enemiesData, enemyFactory, healthBarFactory);
            var interactiveObjectsInitialization = new InteractiveObjectsInitialization(_battleSettingsData);

            var battleInitialization = new EcsBattleInitialization(
                _ecsBattleData, battleInputControlsInitialization.GetData(), _battleSettingsData, generatorDungeon,
                interactiveObjectsInitialization, _player, playerModel, battleModel, targetModel, fightCamera,
                enemiesInitialization);

            _commandManager.BattleInitialisation = battleInitialization;

            var battleController = new BattleController(battleInitialization.BattleEngine());

            _controllers.Add(_commandManager);
            _controllers.Add(generatorDungeon);
            _controllers.Add(listOfPositionCharInMenuController);
            _controllers.Add(listOfCharactersController);
            _controllers.Add(battleController);
            _controllers.Init();
        }


        #region Methods

        private void LoadAllResources()
        {
            _characterData = Resources.Load<CharacterData>("CharacterData");
            Dbg.Log($"Start resource load data - CharacterData:{_characterData}");
            _playerLevelData = Resources.Load<PlayerLevelData>("UnitLevelData");
            Dbg.Log($"Start resource load data - UnitLevelData:{_playerLevelData}");
            _playerClassesData = Resources.Load<PlayerClassesData>("ClassesData");
            Dbg.Log($"Start resource load data - PlayerClassesData:{_playerClassesData}");
            _playerData = Resources.Load<PlayerData>("PlayerData");
            Dbg.Log($"Start resource load data - PlayerData:{_playerData}");
            _cameraSettings = Resources.Load<CameraSettingsInBattle>("CameraSettingsInBattle");
            Dbg.Log($"Start resource load data - CameraSettingsInBattle:{_cameraSettings}");
            _enemiesData = Resources.Load<EnemiesData>("EnemiesData_Simple");
            Dbg.Log($"Start resource load data - EnemiesData:{_enemiesData}");
            _generatorData = Resources.Load<DungeonGeneratorData>("DungeonData");
            Dbg.Log($"Start resource load data - DungeonGeneratorData:{_generatorData}");
            _ecsBattleData = Resources.Load<EcsBattleData>("EcsBattleData");
            Dbg.Log($"Start resource load data - EcsBattleData:{_ecsBattleData}");
            _battleInputData = Resources.Load<BattleInputData>("BattleInputData");
            Dbg.Log($"Start resource load data - BattleInputData:{_battleInputData}");
            _battleSettingsData = Resources.Load<BattleSettingsData>("BattleSettingsData");
            Dbg.Log($"Start resource load data - BattleSettingsData:{_battleSettingsData}");

            LayerManager.EnemyLayer = LayerMask.NameToLayer(StringManager.ENEMY_LAYER);
            LayerManager.PlayerLayer = LayerMask.NameToLayer(StringManager.PLAYER_LAYER);
            LayerManager.PlayerAttackLayer = LayerMask.NameToLayer(StringManager.PLAYER_ATTACK_LAYER);
            LayerManager.EnemyAttackLayer = LayerMask.NameToLayer(StringManager.ENEMY_ATTACK_LAYER);
            LayerManager.EnemyAndPlayerAttackLayer = LayerMask.NameToLayer(StringManager.ENEMY_AND_PLAYER_ATTACK_LAYER);
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

        private void OnDestroy()
        {
            _subscriptions?.Dispose();
            _controllers.Cleanup();
        }

        #endregion

        #endregion
    }
}