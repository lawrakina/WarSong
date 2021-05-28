using System;
using System.Collections.Generic;
using System.Linq;
using Windows;
using AppAds;
using Analytic;
using Battle;
using CharacterCustomizing;
using ContentDataSource;
using ContentDataSource.AbilityItems;
using Controller.Model;
using Data;
using Enums;
using Enums.Abilities;
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
        
        private UnityAnalyticTools _analyticTools;
        [SerializeField]
        private UnityAdsTools _adsTools;
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

        private EnemyClassesData _enemyClassesData;
        private DungeonGeneratorData _generatorData;
        private EcsBattleData _ecsBattleData;
        private BattleInputData _battleInputData;
        private CameraSettingsInBattle _cameraSettings;
        private GeneratorDungeon _generatorDungeon;
        private BattleSettingsData _battleSettingsData;
        private AbilitiesConfigData _abilityCollectionData;


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
            _analyticTools = new UnityAnalyticTools();
            _commandManager = new CommandManager(_uiWindows, _windows, _analyticTools, _adsTools);

            _cursorParticleSystemController = new CursorParticleSystemController(_uiWindows.RootCanvas);
            _controllers.Add(_cursorParticleSystemController);
            
            // var abilitiesInitialization = new AbilitiesRepository(_abilityCollectionData);
            // var abilitiesController = new AbilitiesController(abilitiesInitialization);

            var playerFactory = new PlayerFactory(_characterData,
                new PlayerCustomizerCharacter(_characterData),
                new PlayerLevelInitialization(_playerLevelData),
                new PlayerClassesInitialization(_playerClassesData)
                // new PlayerAbilitiesInitialization(_abilityCollectionData)
                );

            var generatorDungeonModel = new GeneratorDungeonModel(_windows, _generatorData);
            var generatorDungeon = new GeneratorDungeon(generatorDungeonModel);

            var listCharacterModel = new ListCharacterModel(_playerData, playerFactory);
            var listOfCharactersController = new ListOfCharactersController(listCharacterModel, _commandManager);

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
            var enemyFactory = new EnemyFactory(_enemyClassesData,enemyClassesInitialization);
            var healthBarFactory = new HealthBarFactory();
            var enemiesInitialization = new EnemiesInitialization(_enemiesData, enemyFactory, healthBarFactory);
            var interactiveObjectsInitialization = new InteractiveObjectsInitialization(_battleSettingsData);

            var battleInitialization = new EcsBattleInitialization(
                _ecsBattleData, battleInputControlsInitialization.GetData(), _battleSettingsData, generatorDungeon,
                interactiveObjectsInitialization, _player, playerModel, battleModel, targetModel, fightCamera,
                enemiesInitialization);
            var battleController = new BattleController(battleInitialization.BattleEngine());

            _commandManager.ListOfCharacters = listOfCharactersController;
            _commandManager.ChangePlayer.Subscribe(value => 
                { _player = (PlayerView) value; }).AddTo(_subscriptions);
            // _commandManager.ChangePlayer.Subscribe(value => 
                // { abilitiesController.ChangeClass(value); }).AddTo(_subscriptions);
            _commandManager.ChangePlayer.Subscribe(value => 
                { }).AddTo(_subscriptions);
            _commandManager.BattleInitialisation = battleInitialization;
            _commandManager.PlayerModel = playerModel;
            // _commandManager.AbilitiesController = abilitiesController;


            _commandManager.GeneratorDungeon = generatorDungeon;
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
            _characterData = Resources.Load<CharacterData>("Configs/CharacterData");
            Dbg.Log($"Start resource load data - CharacterData:{_characterData}");
            _playerLevelData = Resources.Load<PlayerLevelData>("Configs/UnitLevelData");
            Dbg.Log($"Start resource load data - UnitLevelData:{_playerLevelData}");
            _playerClassesData = Resources.Load<PlayerClassesData>("Configs/ClassesData");
            Dbg.Log($"Start resource load data - PlayerClassesData:{_playerClassesData}");
            _playerData = Resources.Load<PlayerData>("Configs/PlayerData");
            Dbg.Log($"Start resource load data - PlayerData:{_playerData}");
            _cameraSettings = Resources.Load<CameraSettingsInBattle>("Configs/CameraSettingsInBattle");
            Dbg.Log($"Start resource load data - CameraSettingsInBattle:{_cameraSettings}");
            _enemiesData = Resources.Load<EnemiesData>("Configs/EnemiesData_Simple");
            Dbg.Log($"Start resource load data - EnemiesData:{_enemiesData}");
            _enemyClassesData = Resources.Load<EnemyClassesData>("Configs/EnemyClassesData");
            Dbg.Log($"Start resource load data - EnemyClassesData:{_enemyClassesData}");
            _generatorData = Resources.Load<DungeonGeneratorData>("Configs/DungeonData");
            Dbg.Log($"Start resource load data - DungeonGeneratorData:{_generatorData}");
            _ecsBattleData = Resources.Load<EcsBattleData>("Configs/EcsBattleData");
            Dbg.Log($"Start resource load data - EcsBattleData:{_ecsBattleData}");
            _battleInputData = Resources.Load<BattleInputData>("Configs/BattleInputData");
            Dbg.Log($"Start resource load data - BattleInputData:{_battleInputData}");
            _battleSettingsData = Resources.Load<BattleSettingsData>("Configs/BattleSettingsData");
            Dbg.Log($"Start resource load data - BattleSettingsData:{_battleSettingsData}");
            _abilityCollectionData = Resources.Load<AbilitiesConfigData>("DataSource/Ability/AbilitiesConfigData");
            Dbg.Log($"Start resource load data - _abilityItemsConfigCollection:{_abilityCollectionData}");

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




                // Id = config.Id,
                // Title = config.itemConfig.title,
                // Description = config.itemConfig.description,
                // Icon = config.itemConfig.icon,
                // View = config.view, //Object.Instantiate(config.view),
                // TargetType = config.abilityTargetType,
                // CharacteristicType = config.changeableCharacteristicType,
                // TimeOfAction = config.timeOfAction,
                // CastTime = config.castTime,
                // BulletSpeed = config.bulletSpeed,
                // Value = config.value,
                // Cooldown = config.cooldown,
                // WeaponValueModifier = config.weaponValueModifier,
                // RequiredResource = config.requiredResource,
                // CostResource = config.costResource,
                // EffectValueType = config.effectValueType,
                // RequiredClass = config.requiredClass,
                // RequiredLevel = config.requiredLevel
           

    // public interface IAbility
    // {
    //     int Id { get; set; }
    //     string Title { get; set; }
    //     string Description { get; set; }
    //     Sprite Icon { get; set; }
    //     GameObject View { get; set; }
    //     AbilityTargetType TargetType { get; set; }
    //     ChangeableCharacteristicType CharacteristicType { get; set; }
    //     float TimeLagBeforeAction { get; set; }
    //     float CastTime { get; set; }
    //     float TimeOfAction { get; set; }
    //     float BulletSpeed { get; set; }
    //     float Value { get; set; }
    //     float WeaponValueModifier { get; set; }
    //     ResourceEnum RequiredResource { get; set; }
    //     float CostResource { get; set; }
    //     EffectValueType EffectValueType { get; set; }
    //     CharacterClass RequiredClass { get; set; }
    //     int RequiredLevel { get; set; }
    //     bool enabled { get; set; }
    // }
    //
    // public class AbilityItem : IAbility
    // {
    //     private int _id;
    //     private string _title;
    //     private string _description;
    //     private Sprite _icon;
    //
    //     private GameObject _view;
    //
    //     //Who is the target of the ability?
    //     private AbilityTargetType _abilityTargetType;
    //
    //     //What characteristic does it affect?
    //     private ChangeableCharacteristicType _changeableCharacteristicType;
    //
    //     //Lag before apply ability
    //     private float _timeLagBeforeAction;
    //
    //     //Duration of the casting. -1 momentum
    //     private float _castTime = 0.0f;
    //
    //     //Duration of the action
    //     private float _timeOfAction = 0.0f;
    //
    //     //Speed of approach to the target. -1 momentum
    //     private float _bulletSpeed = 0.0f;
    //
    //     //Numerical effect on the parameter
    //     private float _value = 0.0f;
    //
    //     //Add the attack value from the weapon multiplied by the coefficient\n300% = 3.0, 0 = Disabled
    //     private float _weaponValueModifier = 0.0f;
    //
    //     //Required resource
    //     private ResourceEnum _requiredResource;
    //
    //     //Requires resource units
    //     private float _costResource = 10.0f;
    //
    //     //Positive(x*1) or Negative(x*-1) effect
    //     private EffectValueType _effectValueType = EffectValueType.Negative;
    //
    //     //Class owner.
    //     private CharacterClass _requiredClass;
    //
    //     //Minimum required unit level
    //     private int _requiredLevel = 0;
    //
    //     public int Id
    //     {
    //         get => _id;
    //         set => _id = value;
    //     }
    //
    //     public string Title
    //     {
    //         get => _title;
    //         set => _title = value;
    //     }
    //
    //     public string Description
    //     {
    //         get => _description;
    //         set => _description = value;
    //     }
    //
    //     public Sprite Icon
    //     {
    //         get => _icon;
    //         set => _icon = value;
    //     }
    //
    //     public GameObject View
    //     {
    //         get => _view;
    //         set => _view = value;
    //     }
    //
    //     public AbilityTargetType TargetType
    //     {
    //         get => _abilityTargetType;
    //         set => _abilityTargetType = value;
    //     }
    //
    //     public ChangeableCharacteristicType CharacteristicType
    //     {
    //         get => _changeableCharacteristicType;
    //         set => _changeableCharacteristicType = value;
    //     }
    //
    //     public float TimeLagBeforeAction
    //     {
    //         get => _timeLagBeforeAction;
    //         set => _timeLagBeforeAction = value;
    //     }
    //
    //     public float CastTime
    //     {
    //         get => _castTime;
    //         set => _castTime = value;
    //     }
    //
    //     public float TimeOfAction
    //     {
    //         get => _timeOfAction;
    //         set => _timeOfAction = value;
    //     }
    //
    //     public float BulletSpeed
    //     {
    //         get => _bulletSpeed;
    //         set => _bulletSpeed = value;
    //     }
    //
    //     public float Value
    //     {
    //         get => _value;
    //         set => _value = value;
    //     }
    //
    //     public float WeaponValueModifier
    //     {
    //         get => _weaponValueModifier;
    //         set => _weaponValueModifier = value;
    //     }
    //
    //     public ResourceEnum RequiredResource
    //     {
    //         get => _requiredResource;
    //         set => _requiredResource = value;
    //     }
    //
    //     public float CostResource
    //     {
    //         get => _costResource;
    //         set => _costResource = value;
    //     }
    //
    //     public EffectValueType EffectValueType
    //     {
    //         get => _effectValueType;
    //         set => _effectValueType = value;
    //     }
    //
    //     public CharacterClass RequiredClass
    //     {
    //         get => _requiredClass;
    //         set => _requiredClass = value;
    //     }
    //
    //     public int RequiredLevel
    //     {
    //         get => _requiredLevel;
    //         set => _requiredLevel = value;
    //     }
    //
    //     public bool enabled { get; set; }
    //     public float Cooldown { get; set; }
    // }
}