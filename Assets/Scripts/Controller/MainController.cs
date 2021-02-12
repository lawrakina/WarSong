using System.Collections.Generic;
using Windows;
using Battle;
using CharacterCustomizing;
using CoreComponent;
using Data;
using Enums;
using Extension;
using Gui;
using Models;
using UniRx;
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

        [Header("Game Data")]
        [SerializeField]
        private CharacterData _characterData;

        [SerializeField]
        private ClassesData _classesData;
        
        [SerializeField]
        private PlayerData _playerData;

        [SerializeField]
        private EnemiesData _enemiesData;

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

            //UI & Windows
            _activeWindow = new ReactiveProperty<EnumMainWindow>();
            _charWindow = new ReactiveProperty<EnumCharacterWindow>(EnumCharacterWindow.ListCharacters);
            _battleState = new ReactiveProperty<EnumBattleWindow>(EnumBattleWindow.DungeonGenerator);

            _activeWindow.Subscribe(_ => { Dbg.Log(_activeWindow.Value); });
            _charWindow.Subscribe(_ => { Dbg.Log(_charWindow.Value); });
            _battleState.Subscribe(_ => { Dbg.Log(_battleState.Value); });

            var playerModel = new BattlePlayerModel();
            
            var classesInitialization = new ClassesInitialization(_classesData);
            var customizerCharacter = new CustomizerCharacter(_characterData);
            var playerFactory = new PlayerFactory(customizerCharacter, classesInitialization, _characterData);
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

            var enemyFactory = new EnemyFactory();
            var enemiesInitialization = new EnemiesInitialization(_enemiesData, enemyFactory);

            var battleInitialization = new EcsBattleInitialization(
                _ecsBattleData, generatorDungeon, _battleState, _activeWindow, _player, playerModel, fightCamera, enemiesInitialization);
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