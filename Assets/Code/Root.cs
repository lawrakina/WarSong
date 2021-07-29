using System;
using System.Collections.Generic;
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
            _controllers = new Controllers();
            var settings = LoadAllResources();
            var commandManager = new CommandManager();
            var cameraController = new CameraController(settings.CameraSettings);
            var profilePlayer = new ProfilePlayer(commandManager, settings, new UnityAnalyticTools(), cameraController);
            profilePlayer.CurrentState.Value = _gameStateAfterStart;
            profilePlayer.WindowAfterStart = _showWindowAfterStart;
            _mainController = new MainController(_controllers, _placeForUi, profilePlayer);
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
            settings.DungeonGeneratorData = Resources.Load<DungeonGeneratorData>("Configs/Config_DungeonData");
            Dbg.Log($"Start resource load data - DungeonGeneratorData:{settings.DungeonGeneratorData}");
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

    public sealed class Controllers :  IExecute, IFixedExecute, ILateExecute 
        //, IInitialization, ICleanup
    {
        public Controllers()
        {
            // _initializeControllers = new List<IInitialization>();
            _executeControllers = new List<IExecute>();
            _lateControllers = new List<ILateExecute>();
            _fixedControllers = new List<IFixedExecute>();
            // _cleanupControllers = new List<ICleanup>();
        }
        

        // private readonly List<IInitialization> _initializeControllers;
        private readonly List<IExecute> _executeControllers;
        private readonly List<IFixedExecute> _fixedControllers;
        private readonly List<ILateExecute> _lateControllers;
        // private readonly List<ICleanup> _cleanupControllers;


        public Guid Id { get; }

        public void OnExecute()
        {
            
        }

        public void OffExecute()
        {
            
        }

        public bool IsOn => true;

        public void Add(IController controller)
        {
            // if (controller is IInitialization initializeController) _initializeControllers.Add(initializeController);

            if (controller is IExecute executeController) _executeControllers.Add(executeController);

            if (controller is IFixedExecute fixedExecuteController) _fixedControllers.Add(fixedExecuteController);

            if (controller is ILateExecute lateExecuteController) _lateControllers.Add(lateExecuteController);

            // if (controller is ICleanup cleanupController) _cleanupControllers.Add(cleanupController);
        }

        // public void Init()
        // {
        //     for (var index = 0; index < _initializeControllers.Count; ++index)
        //         _initializeControllers[index].Init();
        // }

        public void Execute(float deltaTime)
        {
            for (var index = 0; index < _executeControllers.Count; ++index)
            {
                if(_executeControllers[index].IsOn)
                    _executeControllers[index].Execute(deltaTime);
            }
        }

        public void FixedExecute(float deltaTime)
        {
            for (var index = 0; index < _fixedControllers.Count; ++index)
                _fixedControllers[index].FixedExecute(deltaTime);
        }

        public void LateExecute(float deltaTime)
        {
            for (var index = 0; index < _lateControllers.Count; ++index) 
                _lateControllers[index].LateExecute(deltaTime);
        }

        // public void Cleanup()
        // {
        //     for (var index = 0; index < _cleanupControllers.Count; ++index) 
        //         _cleanupControllers[index].Cleanup();
        // }
        
        public void Remove(IController controller)
        {
            for (var index = 0; index < _executeControllers.Count; ++index)
            {
                if(_executeControllers[index].Id == controller.Id)
                    _executeControllers.RemoveAt(index);
            }
            for (var index = 0; index < _fixedControllers.Count; ++index)
            {
                if(_fixedControllers[index].Id == controller.Id)
                    _fixedControllers.RemoveAt(index);
            }
            for (var index = 0; index < _lateControllers.Count; ++index)
            {
                if(_lateControllers[index].Id == controller.Id)
                    _lateControllers.RemoveAt(index);
            }
        }
    }
}