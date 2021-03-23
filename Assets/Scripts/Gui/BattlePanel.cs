using System;
using Battle;
using Enums;
using Gui.Battle;
using Models;
using UniRx;


namespace Gui
{
    [Serializable] public sealed class BattlePanel : BasePanel
    {
        #region Fields

        private IReactiveProperty<EnumBattleWindow> _battleState;

        public LevelGeneratorPanel LevelGeneratorPanel;
        public FightPanel FightPanel;
        public VictoryPanel VictoryPanel;
        public PausePanel PausePanel;
        public FailPanel FailPanel;
        private BattlePlayerModel _playerModel;
        private BattleProgressModel _battleModel;

        #endregion


        public void Ctor(IReactiveProperty<EnumBattleWindow> battleState, BattlePlayerModel playerModel,
            BattleProgressModel battleModel)
        {
            base.Ctor();

            _playerModel = playerModel;
            _battleModel = battleModel;

            LevelGeneratorPanel.Ctor();
            FightPanel.Ctor(_battleModel,_playerModel);
            VictoryPanel.Ctor();
            PausePanel.Ctor();
            FailPanel.Ctor();

            _battleState = battleState;

            _battleState.Subscribe(_ =>
            {
                if (!enabled) return;
                if (_battleState.Value == EnumBattleWindow.DungeonGenerator)
                    LevelGeneratorPanel.Show();
                else LevelGeneratorPanel.Hide();
                if (_battleState.Value == EnumBattleWindow.Fight)
                    FightPanel.Show();
                else FightPanel.Hide();
                if (_battleState.Value == EnumBattleWindow.Victory)
                    VictoryPanel.Show();
                else VictoryPanel.Hide();
                if (_battleState.Value == EnumBattleWindow.Fail)
                    FailPanel.Show();
                else FailPanel.Hide();
                if (_battleState.Value == EnumBattleWindow.Pause)
                    PausePanel.Show();
                else PausePanel.Hide();
            }).AddTo(_subscriptions);
        }
    }
}