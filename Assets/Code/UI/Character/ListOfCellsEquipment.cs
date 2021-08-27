using System.Collections.Generic;
using Code.Data;


namespace Code.UI.Character
{
    public class ListOfCellsEquipment : List<CellEquipment>
    {
        public CellEquipmentHandler TemplateCellEquipmentHandler { get; set; }
        public CellEquipment MainWeapon { get; set; }
        public CellEquipment SecondWeapon { get; set; }
        public ActiveWeapons ActiveWeapons { get; set; }
    }
}