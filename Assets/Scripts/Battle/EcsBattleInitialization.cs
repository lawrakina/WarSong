using System;
using System.Collections.Generic;
using Controller;
using CoreComponent;
using Data;
using Enums;
using Extension;
using Interface;
using Models;
using UniRx;
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
        private IReactiveProperty<EnumMainWindow> _activeWindow;
        private readonly IReactiveProperty<EnumBattleWindow> _battleState;
        private readonly BattleInputStruct _battleInputStruct;
        private readonly BattleSettingsData _battleSettings;
        private readonly IPlayerView _player;
        private readonly BattlePlayerModel _playerModel;
        private readonly BattleProgressModel _battleModel;
        private readonly IFightCamera _camera;
        private readonly EnemiesInitialization _enemiesInitialization;
        private List<IEnemyView> _listEnemies = new List<IEnemyView>();

        #endregion


        #region Properties

        public GameObject Dungeon { get; set; }
        public EcsBattle.EcsBattle BattleEngine() => _ecsBattle;

        #endregion


        #region ClassLiveCycles

        public EcsBattleInitialization(EcsBattleData ecsBattleData,
            BattleInputStruct battleInputStruct,
            BattleSettingsData battleSettings,
            IGeneratorDungeon generatorDungeon,
            IReactiveProperty<EnumBattleWindow> battleState,
            IReactiveProperty<EnumMainWindow> activeWindow,
            IPlayerView player, BattlePlayerModel playerModel,
            BattleProgressModel battleModel,
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
            _camera = camera;
            _enemiesInitialization = enemiesInitialization;
            _generatorDungeon = generatorDungeon;
            _battleState = battleState;
            _activeWindow = activeWindow;

            _ecsBattle.Inject(_player);
            _ecsBattle.Inject(_camera);
            _ecsBattle.Inject(_playerModel);
            _ecsBattle.Inject(_battleModel);
            _ecsBattle.Inject(_battleInputStruct);
            _ecsBattle.Inject(_battleSettings);
        }

        #endregion


        #region Methods

        public void StartBattle()
        {
            _listEnemies =
                _enemiesInitialization.GetListEnemies(_generatorDungeon.GetEnemiesMarkers(),
                    _generatorDungeon.Dungeon());
            _ecsBattle.Inject(_listEnemies);

            var playerPosition = _generatorDungeon.GetPlayerPosition();
            _player.Transform.SetParent(playerPosition);
            _player.Transform.localPosition = Vector3.zero;
            _battleState.Value = EnumBattleWindow.Fight;

            _ecsBattle.Init();
        }

        #endregion
    }
}