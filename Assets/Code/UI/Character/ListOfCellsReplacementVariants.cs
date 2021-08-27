using System.Collections.Generic;
using Code.Equipment;


namespace Code.UI.Character
{
    public class ListOfCellsReplacementVariants : List<CellEquipment>
    {
        private CellEquipment _activeCell;
        private List<BaseEquipItem> _source;
        private CellEquipmentHandler _cellTemplate;
        
        public CellEquipmentHandler CellTemplate => _cellTemplate;

        public IEnumerable<BaseEquipItem> GetListByType => _source.FindAll(x =>
            x.ItemType == _activeCell.ItemType && x.SubItemType == _activeCell.SubItemType);
        
        public ListOfCellsReplacementVariants(List<BaseEquipItem> inventory, CellEquipmentHandler cellTemplate,
            CellEquipment activeCell)
        {
            _source = inventory;
            _activeCell = activeCell;
            _cellTemplate = cellTemplate;
        }
    }
}