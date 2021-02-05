using System.Collections.Generic;
using Windows;
using Battle;
using CoreComponent;
using Data;
using Enums;
using Extension;
using Gui;
using InputMovement;
using Interface;
using JetBrains.Annotations;
using UniRx;
using Unit.Cameras;
using Unit.Player;
using UnityEngine;
using EcsBattle = EcsBattle.EcsBattle;


namespace Controller
{
    public class MainController : MonoBehaviour
    {
        #region Fields

        private CompositeDisposable _subscriptions;
        private Controllers _controllers;

        [Header("Game Layers")]
        [SerializeField]
        private LayerMask _groundLayer;

        [Header("Game Data")]
        [SerializeField]
        private PlayerData _playerData;

        [SerializeField]
        private CharacterData _characterData;

        [SerializeField]
        private DungeonGeneratorData _generatorData;
        
        [SerializeField]
        private EcsBattleData _ecsBattleData;

        [Header("Ui & Windows")]
        [SerializeField]
        private UiReference _ui;

        [SerializeField]
        private WindowsReference _windows;

        [Header("Active Panel and Window at the Start")]
        [SerializeField]
        private EnumMainWindow _activePanelAndWindow;

        // [Header("Type of camera and char control")]
        // [SerializeField]
        // private EnumFightCamera _fightCameraType = EnumFightCamera.ThirdPersonView;

        private IPlayerView _player;

        [Header("For debug")]
        [SerializeField]
        private GameObject _linkToCharPlayer;

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
            LayerManager.GroundLayer = _groundLayer;

            //UI & Windows
            _activeWindow = new ReactiveProperty<EnumMainWindow>();
            _charWindow = new ReactiveProperty<EnumCharacterWindow>(EnumCharacterWindow.ListCharacters);
            _battleState = new ReactiveProperty<EnumBattleWindow>(EnumBattleWindow.DungeonGenerator);

            _activeWindow.Subscribe(_ => { Dbg.Log(_activeWindow.Value); });
            _charWindow.Subscribe(_ => { Dbg.Log(_charWindow.Value); });
            _battleState.Subscribe(_ => { Dbg.Log(_battleState.Value); });

            var customizerCharacter = new CustomizerCharacter(_characterData);
            var playerFactory = new PlayerFactory(customizerCharacter, _characterData);
            var listOfCharactersController = new ListOfCharactersController(_playerData, playerFactory);
            _player = listOfCharactersController.CurrentCharacter.Value;
            listOfCharactersController.CurrentCharacter.Subscribe(_ =>
            {
                _player = listOfCharactersController.CurrentCharacter.Value;
                _linkToCharPlayer = _player.Transform.gameObject;
            }).AddTo(_subscriptions);
            
            //create ui & windows
            _windows.Ctor(_activeWindow, _battleState);
            _ui.Ctor(_activeWindow, _battleState, _charWindow, listOfCharactersController);

            

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

            var battleInitialization = new EcsBattleInitialization(_ecsBattleData, generatorDungeon, _battleState, _activeWindow, _player, fightCamera);
            battleInitialization.Dungeon = generatorDungeon.Dungeon();
            _ui.BattlePanel.LevelGeneratorPanel.SetReference(battleInitialization);
            
            var battleController = new BattleController(battleInitialization.BattleEngine());
            
            // var battleInitialization = new BattleInitialization(generatorDungeon, _battleState, _activeWindow, _player);
            // battleInitialization.Dungeon = generatorDungeon.Dungeon();
            // _ui.BattlePanel.LevelGeneratorPanel.SetReference(battleInitialization);

            // _typeCameraAndCharControl = new ReactiveProperty<EnumFightCamera>(_fightCameraType);
            // var battleCameraController =
            //     new FightCameraController(_battleState, _player, fightCamera, _typeCameraAndCharControl);
            // var battlePlayerMoveController =
            //     new MovePlayerController(_battleState, inputInitialization.GetInput(), _player,
            //         _typeCameraAndCharControl);
            // var inputController = new InputController(inputInitialization.GetInput());

            _controllers = new Controllers();
            // _controllers.Add(customizingCharacter);
            _controllers.Add(positioningCharInMenuController);
            _controllers.Add(battleController);
            // _controllers.Add(battleCameraController);
            // _controllers.Add(inputInitialization);
            // _controllers.Add(battlePlayerMoveController);
            // _controllers.Add(inputController);

            var offItemMenu = new List<EnumMainWindow>();
            offItemMenu.Add(EnumMainWindow.Equip);
            offItemMenu.Add(EnumMainWindow.Spells);
            offItemMenu.Add(EnumMainWindow.Talents);
            _ui.Init(offItemMenu);
            _windows.Init();
            _controllers.Initialization();
            _activeWindow.Value = _activePanelAndWindow;
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

    public sealed class BattleController : BaseController, IExecute
    {
        private readonly global::EcsBattle.EcsBattle _ecsEngine;

        public BattleController(global::EcsBattle.EcsBattle ecsEngine)
        {
            _ecsEngine = ecsEngine;
        }

        public void Execute(float deltaTime)
        {
            _ecsEngine.Execute(deltaTime);
        }
    }
}