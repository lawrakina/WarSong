using CoreComponent;
using Enums;
using UniRx;
using Unit.Player;
using UnityEngine;


namespace Battle
{
    public sealed class BattleInitialization : IBattleInit
    {
        #region Fields

        private readonly IGeneratorDungeon _generatorDungeon;
        private IReactiveProperty<EnumMainWindow> _activeWindow;
        private readonly IReactiveProperty<EnumBattleWindow> _battleState;
        private readonly IPlayerView _player;

        #endregion


        #region Properties

        public GameObject Dungeon { get; set; }

        #endregion


        #region ClassLiveCycles

        public BattleInitialization(IGeneratorDungeon generatorDungeon,
            IReactiveProperty<EnumBattleWindow> battleState,
            IReactiveProperty<EnumMainWindow> activeWindow, IPlayerView player)
        {
            _player = player;
            _generatorDungeon = generatorDungeon;
            _battleState = battleState;
            _activeWindow = activeWindow;
        }

        #endregion


        #region Methods

        public void StartBattle()
        {
            var playerPosition = _generatorDungeon.GetPlayerPosition();

            _player.Transform.SetParent(playerPosition);
            _player.Transform.localPosition = Vector3.zero;
            _player.Transform.localRotation = Quaternion.identity;
            _battleState.Value = EnumBattleWindow.Fight;
        }

        #endregion
    }
}