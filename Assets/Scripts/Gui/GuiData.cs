using Gui.Characters;
using UnityEngine;


namespace Gui
{
    [CreateAssetMenu(fileName = "GuiData", menuName = "Data/GuiData")]
    public sealed class GuiData : ScriptableObject
    {
        public BattlePanel BattlePanel;
        public CharacterPanel CharacterPanel;
        public EquipmentPanel EquipmentPanel;
        public NavigationBar NavigationBar;
        public SpellsPanel SpellsPanel;
        public TalentsPanel TalentsPanel;
    }
}