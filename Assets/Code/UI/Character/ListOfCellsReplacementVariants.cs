using System.Collections.Generic;
using Code.Equipment;
using UnityEngine;


namespace Code.UI.Character
{
    public class ListOfCellsReplacementVariants : List<SlotEquipment>
    {
        private SlotEquipment _activeSlot;
        private List<BaseEquipItem> _source;
        private CellEquipmentDragAndDropHandler _cellTemplate;
        private SlotDropHandler _slotDropHandler;

        public CellEquipmentDragAndDropHandler CellTemplate => _cellTemplate;

        public SlotEquipment ActiveSlot
        {
            get => _activeSlot;
            set => _activeSlot = value;
        }
        
        public IEnumerable<BaseEquipItem> GetListByType => _source.FindAll(x =>
            x.ItemType == _activeSlot.ItemType && x.SubItemType == _activeSlot.SubItemType);

        public SlotDropHandler SlotHandler => _slotDropHandler;

        public ListOfCellsReplacementVariants(
            List<BaseEquipItem> inventory, CellEquipmentDragAndDropHandler cellTemplate, 
            SlotDropHandler slotDropHandler, SlotEquipment activeSlot)
        {
            _source = inventory;
            _activeSlot = activeSlot;
            _cellTemplate = cellTemplate;
            _slotDropHandler = slotDropHandler;
        }
    }
}