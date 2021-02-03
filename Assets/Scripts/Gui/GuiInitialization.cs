using Gui.Characters;
using UnityEngine;


namespace Gui
{
    public sealed class GuiInitialization
    {
        #region Fields

        private readonly GuiData _guiData;

        #endregion


        #region CyclerLiveMethods

        public GuiInitialization(Canvas uiRoot, GuiData guiData)
        {
            _guiData = guiData;
            CharacterPanel = Object.Instantiate(_guiData.CharacterPanel, uiRoot.transform);
            EquipmentPanel = Object.Instantiate(_guiData.EquipmentPanel, uiRoot.transform);
            BattlePanel = Object.Instantiate(_guiData.BattlePanel, uiRoot.transform);
            SpellsPanel = Object.Instantiate(_guiData.SpellsPanel, uiRoot.transform);
            TalentsPanel = Object.Instantiate(_guiData.TalentsPanel, uiRoot.transform);
            NavigationBar = Object.Instantiate(_guiData.NavigationBar, uiRoot.transform);
        }

        #endregion


        #region Properties

        public BattlePanel BattlePanel;
        public CharacterPanel CharacterPanel;
        public TalentsPanel TalentsPanel;
        public SpellsPanel SpellsPanel;
        public EquipmentPanel EquipmentPanel;
        public NavigationBar NavigationBar;

        #endregion
    }
}