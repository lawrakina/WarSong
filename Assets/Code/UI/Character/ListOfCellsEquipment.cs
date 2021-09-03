using System.Collections.Generic;
using Code.Data;


namespace Code.UI.Character
{
    public class ListOfCellsEquipment : List<SlotEquipment>
    {
        public CellEquipmentClickHandler TemplateCellEquipmentClickHandler { get; set; }
        public SlotEquipment MainWeapon { get; set; }
        public SlotEquipment SecondWeapon { get; set; }
        public ActiveWeapons ActiveWeapons { get; set; }
    }
}