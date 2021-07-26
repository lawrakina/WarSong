using System;
using Code.Data;

namespace Code.Equipment
{
    [Serializable]
    public class BaseShield : BaseEquipItem
    {
        public override InventoryItemType ItemType => InventoryItemType.Armor;
        public ArmorItemType ArmorItemType => ArmorItemType.Shield;
    }
}