using Code.Data;
using Code.Equipment;
using UniRx;


namespace Code.UI.Character
{
    public class SlotEquipment
    {
        public InventoryItemType ItemType { get; }
        public int SubItemType { get; }
        public ReactiveCommand<SlotEquipment> Command { get; } = new ReactiveCommand<SlotEquipment>();
        public BaseEquipItem EquipmentItem { get; }
        
        public SlotEquipment(BaseEquipItem equip, int subType = -1)
        {
            ItemType = equip == null ? InventoryItemType.None : equip.ItemType;
            SubItemType = subType;
            EquipmentItem = equip;
        }
    }
}