using System;
using System.Collections.Generic;
using Battle;
using Controller;
using Enums;
using Gui.Characters;
using Interface;
using Models;
using UniRx;
using Unit.Player;


namespace Gui
{
    [Serializable] public sealed class UiReference : IInitialization, ICleanup
    {
        #region Fields

        private IReactiveProperty<EnumMainWindow> _activeWindow;
        private IReactiveProperty<EnumCharacterWindow> _charWindow;
        private IReactiveProperty<EnumBattleWindow> _battleState;

        public CharacterPanel CharacterPanel;
        public EquipmentPanel EquipmentPanel;
        public BattlePanel BattlePanel;
        public SpellsPanel SpellsPanel;
        public TalentsPanel TalentsPanel;
        public NavigationBar NavigationBar;
        private ListOfCharactersController _listOfCharactersController;
        private BattlePlayerModel _playerModel;
        private BattleProgressModel _battleModel;

        #endregion


        #region ICleanup

        public void Cleanup()
        {
            CharacterPanel.Cleanup();
            EquipmentPanel.Cleanup();
            BattlePanel.Cleanup();
            SpellsPanel.Cleanup();
            TalentsPanel.Cleanup();
            NavigationBar.Cleanup();
        }

        #endregion


        #region IInit

        public void Initialization()
        {
        }

        public void Init(List<EnumMainWindow> offItemMenu)
        {
            Initialization();
            CharacterPanel.Initialization();
            EquipmentPanel.Initialization();
            BattlePanel.Initialization();
            SpellsPanel.Initialization();
            TalentsPanel.Initialization();
            NavigationBar.Init(offItemMenu);
        }

        #endregion


        #region ClassLiveCycles

        public void Ctor(IReactiveProperty<EnumMainWindow> activeWindow,
            IReactiveProperty<EnumBattleWindow> battleState, IReactiveProperty<EnumCharacterWindow> charWindow,
            ListOfCharactersController listOfCharactersController, BattlePlayerModel playerModel,
            BattleProgressModel battleModel)
        {
            _activeWindow = activeWindow;
            _battleState = battleState;
            _charWindow = charWindow;
            _listOfCharactersController = listOfCharactersController;
            _playerModel = playerModel;
            _battleModel = battleModel;

            CharacterPanel.Ctor(_activeWindow, _charWindow, _listOfCharactersController);
            EquipmentPanel.Ctor();
            BattlePanel.Ctor(_battleState, _playerModel, _battleModel);
            SpellsPanel.Ctor();
            TalentsPanel.Ctor();
            NavigationBar.Ctor(_activeWindow, _battleState);

            _activeWindow.Subscribe(_ => { ShowOnlyActivePanel(); });
            _battleState.Subscribe(_ => { ShowBattleOnlyActivePanel(); });
        }

        #endregion


        #region PrivateMethods

        private void ShowBattleOnlyActivePanel()
        {
            if (_battleState.Value == EnumBattleWindow.DungeonGenerator)
                NavigationBar.Show();
            else NavigationBar.Hide();
        }

        private void ShowOnlyActivePanel()
        {
            // Debug.Log($"ShowOnlyActivePanel:{_activeWindow.Value}");
            // Debug.Log($"CharacterPanel:{CharacterPanel}, BattlePanel:{BattlePanel},{BattlePanel.enabled},{BattlePanel.name}");
            if (_activeWindow.Value == EnumMainWindow.Character)
                CharacterPanel.Show();
            else CharacterPanel.Hide();
            if (_activeWindow.Value == EnumMainWindow.Equip)
                EquipmentPanel.Show();
            else EquipmentPanel.Hide();
            if (_activeWindow.Value == EnumMainWindow.Battle)
                BattlePanel.Show();
            else BattlePanel.Hide();
            if (_activeWindow.Value == EnumMainWindow.Spells)
                SpellsPanel.Show();
            else SpellsPanel.Hide();
            if (_activeWindow.Value == EnumMainWindow.Talents)
                TalentsPanel.Show();
            else TalentsPanel.Hide();
        }

        #endregion
    }
}