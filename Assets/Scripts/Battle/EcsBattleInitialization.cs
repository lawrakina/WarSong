using CoreComponent;
using Data;
using Enums;
using Extension;
using Interface;
using UniRx;
using Unit.Player;
using UnityEngine;


namespace Battle
{
    public sealed class EcsBattleInitialization : IBattleInit
    {
        #region Fields

        private readonly EcsBattle.EcsBattle _ecsBattle;
        private readonly IGeneratorDungeon _generatorDungeon;
        private IReactiveProperty<EnumMainWindow> _activeWindow;
        private readonly IReactiveProperty<EnumBattleWindow> _battleState;
        private readonly IPlayerView _player;

        #endregion


        #region Properties

        public GameObject Dungeon { get; set; }
        public EcsBattle.EcsBattle BattleEngine() => _ecsBattle;

        #endregion


        #region ClassLiveCycles

        public EcsBattleInitialization(EcsBattleData ecsBattleData,
            IGeneratorDungeon generatorDungeon,
            IReactiveProperty<EnumBattleWindow> battleState,
            IReactiveProperty<EnumMainWindow> activeWindow, IPlayerView player, IFightCamera camera)
        {
            _ecsBattle = Object.Instantiate(ecsBattleData.EcsBattle);
            _ecsBattle.gameObject.name = StringManager.ECS_BATTLE_GO_NAME;
            
            _player = player;
            _generatorDungeon = generatorDungeon;
            _battleState = battleState;
            _activeWindow = activeWindow;

            _ecsBattle.Inject(player);
            _ecsBattle.Inject(camera);
        }

        #endregion


        #region Methods

        public void StartBattle()
        {
            _ecsBattle.Init();
            
            var playerPosition = _generatorDungeon.GetPlayerPosition();
            _player.Transform.SetParent(playerPosition);
            _player.Transform.localPosition = Vector3.zero;
            _player.Transform.localRotation = Quaternion.identity;
            _battleState.Value = EnumBattleWindow.Fight;
        }

        #endregion
    }
}