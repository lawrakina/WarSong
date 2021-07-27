using System;
using Code.Data;
using UnityEngine;


namespace Code.Equipment
{
    [Serializable]
    public class BaseShield : BaseEquipItem, IBaseShield, IBaseArmor
    {
        [SerializeField]
        private int _itemLevel = 1;

        [SerializeField]
        private int _armorValue = 1;
        public override InventoryItemType ItemType => InventoryItemType.Armor;
        public override int ItemLevel => _itemLevel;
        public ArmorItemType ArmorItemType => ArmorItemType.Shield;
        public int ArmorValue => _armorValue;
    }
}