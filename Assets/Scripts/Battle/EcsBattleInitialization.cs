using System;
using System.Collections.Generic;
using Controller;
using Data;
using Enums;
using Extension;
using Interface;
using Models;
using UniRx;
using Unit;
using Unit.Enemies;
using Unit.Player;
using UnityEngine;
using Object = UnityEngine.Object;


namespace Battle
{
    public sealed class EcsBattleInitialization : IBattleInit
    {
        #region Fields

        private readonly EcsBattle.EcsBattle _ecsBattle;
        private readonly IGeneratorDungeon _generatorDungeon;
        private readonly InteractiveObjectsInitialization _interactiveObjectsInitialization;
        private readonly BattleInputStruct _battleInputStruct;
        private readonly BattleSettingsData _battleSettings;
        private IPlayerView _player;
        private readonly BattlePlayerModel _playerModel;
        private readonly BattleProgressModel _battleModel;
        private readonly BattleTargetModel _targetModel;
        private readonly IFightCamera _camera;
        private readonly EnemiesInitialization _enemiesInitialization;
        private List<IEnemyView> _listEnemies = new List<IEnemyView>();
        private GoalLevelView _goal;

        #endregion


        #region Properties

        public EcsBattle.EcsBattle BattleEngine() => _ecsBattle;

        #endregion


        #region ClassLiveCycles

        public EcsBattleInitialization(EcsBattleData ecsBattleData,
            BattleInputStruct battleInputStruct,
            BattleSettingsData battleSettings,
            IGeneratorDungeon generatorDungeon,
            InteractiveObjectsInitialization interactiveObjectsInitialization,
            IPlayerView player, BattlePlayerModel playerModel,
            BattleProgressModel battleModel,
            BattleTargetModel targetModel,
            IFightCamera camera,
            EnemiesInitialization enemiesInitialization)
        {
            _ecsBattle = Object.Instantiate(ecsBattleData.EcsBattle);
            _ecsBattle.gameObject.name = StringManager.ECS_BATTLE_GO_NAME;

            _battleInputStruct = battleInputStruct;
            _battleSettings = battleSettings;
            _player = player;
            _playerModel = playerModel;
            _battleModel = battleModel;
            _targetModel = targetModel;
            _camera = camera;
            _enemiesInitialization = enemiesInitialization;
            _generatorDungeon = generatorDungeon;
            _interactiveObjectsInitialization = interactiveObjectsInitialization;
            
            _ecsBattle.Inject(_camera);
            _ecsBattle.Inject(_playerModel);
            _ecsBattle.Inject(_battleModel);
            _ecsBattle.Inject(_targetModel);
            _ecsBattle.Inject(_battleInputStruct);
            _ecsBattle.Inject(_battleSettings);
        }

        #endregion


        #region Methods

        public void StartBattle()
        {
            _player = GlobalLinks.Player;
            _ecsBattle.Inject(_player);
            
            _listEnemies =
                _enemiesInitialization.GetListEnemies(_generatorDungeon.GetEnemiesMarkers(),
                    _generatorDungeon.Dungeon());
            _ecsBattle.Inject(_listEnemies);

            _goal = _interactiveObjectsInitialization.GetGoal(_generatorDungeon.GetGoalLevelMarker());
            _ecsBattle.Inject(_goal);

            var playerPosition = _generatorDungeon.GetPlayerPosition();
            _player.Transform.SetParent(playerPosition);
            _player.Transform.localPosition = Vector3.zero;

            _ecsBattle.Init();
        }

        #endregion
    }
}