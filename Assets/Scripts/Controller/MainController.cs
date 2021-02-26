using System.Collections.Generic;
using Windows;
using Battle;
using CharacterCustomizing;
using CoreComponent;
using Data;
using Enums;
using Extension;
using Gui;
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
        private IPlayerView _player;

        [Header("Ui & Windows")]
        [SerializeField]
        private UiReference _ui;

        [SerializeField]
        private WindowsReference _windows;

        [Header("Active Panel and Window at the Start")]
        [SerializeField]
        private EnumMainWindow _activePanelAndWindow;

        [Header("Plz do not set value!!! Its Simple access")]
        [SerializeField]
        private GameObject _linkToCharPlayer;

        private CharacterData _characterData;
        private UnitLevelData _unitLevelData;
        private ClassesData _classesData;
        private PlayerData _playerData;
        private EnemiesData _enemiesData;
        private DungeonGeneratorData _generatorData;
        private EcsBattleData _ecsBattleData;
        private BattleInputData _battleInputData;

        private IReactiveProperty<EnumMainWindow> _activeWindow;
        private IReactiveProperty<EnumCharacterWindow> _charWindow;
        private IReactiveProperty<EnumBattleWindow> _battleState;
        private IReactiveProperty<EnumFightCamera> _typeCameraAndCharControl;

        #endregion


        //ДОДЕЛАТЬ БЛЯТЬ !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!outLine


        #region UnityMethods

        private void Awake()
        {
            _subscriptions = new CompositeDisposable();
            LoadAllResources();

            //UI & Windows
            _activeWindow = new ReactiveProperty<EnumMainWindow>();
            _charWindow = new ReactiveProperty<EnumCharacterWindow>(EnumCharacterWindow.ListCharacters);
            _battleState = new ReactiveProperty<EnumBattleWindow>(EnumBattleWindow.DungeonGenerator);

            _activeWindow.Subscribe(_ => { Dbg.Log(_activeWindow.Value); });
            _charWindow.Subscribe(_ => { Dbg.Log(_charWindow.Value); });
            _battleState.Subscribe(_ => { Dbg.Log(_battleState.Value); });

            var playerModel = new BattlePlayerModel();

            var unitLevelInitialization = new UnitLevelInitialization(_unitLevelData);
            var classesInitialization = new ClassesInitialization(_classesData);
            var customizerCharacter = new CustomizerCharacter(_characterData);
            var playerFactory = new PlayerFactory(customizerCharacter, unitLevelInitialization, classesInitialization,
                _characterData);
            var listOfCharactersController = new ListOfCharactersController(_playerData, playerFactory);
            _player = listOfCharactersController.CurrentCharacter.Value;
            listOfCharactersController.CurrentCharacter.Subscribe(_ =>
            {
                _player = listOfCharactersController.CurrentCharacter.Value;
                _linkToCharPlayer = _player.Transform.gameObject;
            }).AddTo(_subscriptions);

            //create ui & windows
            _windows.Ctor(_activeWindow, _battleState);
            _ui.Ctor(_activeWindow, _battleState, _charWindow, listOfCharactersController, playerModel);

            //generator levels
            var generatorDungeon = new GeneratorDungeon(_generatorData, _windows.BattleWindow.Content.transform);
            _ui.BattlePanel.LevelGeneratorPanel.SetReference(generatorDungeon);

            var fightCameraFactory = new CameraFactory();
            // камера используется в рендере gui и сцены - todo все в SO и префабы
            var fightCamera = fightCameraFactory.CreateCamera(_windows.BattleWindow.Camera);

            //Positioning character in menu
            var positioningCharInMenuController = new PositioningCharacterInMenuController(_activeWindow, _battleState);
            positioningCharInMenuController.Player = _player;
            positioningCharInMenuController.GeneratorDungeon = generatorDungeon;
            positioningCharInMenuController.AddPlayerPosition(
                _windows.CharacterWindow.CharacterSpawn(), EnumMainWindow.Character);
            positioningCharInMenuController.AddPlayerPosition(
                _windows.EquipmentWindow.CharacterSpawn(), EnumMainWindow.Equip);
            positioningCharInMenuController.AddPlayerPosition(
                generatorDungeon.GetPlayerPosition(), EnumMainWindow.Battle);
            positioningCharInMenuController.AddPlayerPosition(
                _windows.SpellsWindow.CharacterSpawn(), EnumMainWindow.Spells);
            positioningCharInMenuController.AddPlayerPosition(
                _windows.TalentsWindow.CharacterSpawn(), EnumMainWindow.Talents);

            var battleInputControlsInitialization =
                new BattleInputControlsInitialization(_battleInputData, _ui.BattlePanel.FightPanel.transform);
            var enemyFactory = new EnemyFactory();
            var healthBarFactory = new HealthBarFactory();
            var enemiesInitialization = new EnemiesInitialization(_enemiesData, enemyFactory, healthBarFactory);

            var battleInitialization = new EcsBattleInitialization(
                _ecsBattleData, battleInputControlsInitialization.GetData(), generatorDungeon, _battleState,
                _activeWindow, _player, playerModel, fightCamera, enemiesInitialization);
            battleInitialization.Dungeon = generatorDungeon.Dungeon();
            _ui.BattlePanel.LevelGeneratorPanel.SetReference(battleInitialization);

            var battleController = new BattleController(battleInitialization.BattleEngine());

            _controllers = new Controllers();
            _controllers.Add(positioningCharInMenuController);
            _controllers.Add(battleController);

            var offItemMenu = new List<EnumMainWindow>();
            offItemMenu.Add(EnumMainWindow.Equip);
            offItemMenu.Add(EnumMainWindow.Spells);
            offItemMenu.Add(EnumMainWindow.Talents);
            _ui.Init(offItemMenu);
            _windows.Init();
            _controllers.Initialization();
            _activeWindow.Value = _activePanelAndWindow;
        }

        private void LoadAllResources()
        {
            _characterData = Resources.Load<CharacterData>("CharacterData");
            _unitLevelData = Resources.Load<UnitLevelData>("UnitLevelData");
            _classesData = Resources.Load<ClassesData>("ClassesData");
            _playerData = Resources.Load<PlayerData>("PlayerData");
            _enemiesData = Resources.Load<EnemiesData>("EnemiesData_Simple");
            _generatorData = Resources.Load<DungeonGeneratorData>("DungeonData");
            _ecsBattleData = Resources.Load<EcsBattleData>("EcsBattleData");
            _battleInputData = Resources.Load<BattleInputData>("BattleInputData");
            
            LayerManager.EnemyLayer = LayerMask.NameToLayer(StringManager.ENEMY_LAYER);
            LayerManager.PlayerLayer = LayerMask.NameToLayer(StringManager.PLAYER_LAYER);
            LayerManager.PlayerAttackLayer = LayerMask.NameToLayer(StringManager.PLAYER_ATTACK_LAYER);
            LayerManager.EnemyAttackLayer = LayerMask.NameToLayer(StringManager.ENEMY_ATTACK_LAYER);
            LayerManager.EnemyAndPlayerAttackLayer = LayerMask.NameToLayer(StringManager.ENEMY_AND_PLAYER_ATTACK_LAYER);
        }


        #region Methods

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