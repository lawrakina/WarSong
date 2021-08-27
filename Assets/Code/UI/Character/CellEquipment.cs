using Code.Data;
using Code.Equipment;
using UniRx;


namespace Code.UI.Character
{
    public class CellEquipment
    {
        public InventoryItemType ItemType { get; }
        public int SubItemType { get; }
        public ReactiveCommand<CellEquipment> Command { get; } = new ReactiveCommand<CellEquipment>();
        public BaseEquipItem EquipmentItem { get; }
        
        public CellEquipment(BaseEquipItem equip, int subType = -1)
        {
            ItemType = equip == null ? InventoryItemType.None : equip.ItemType;
            SubItemType = subType;
            EquipmentItem = equip;
        }
    }
}