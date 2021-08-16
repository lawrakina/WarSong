using System;
using Code.Data;
using Code.Data.Unit;
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

        [SerializeField]
        private Characteristics _characteristics;
        public override InventoryItemType ItemType => InventoryItemType.Armor;
        public override int ItemLevel => _itemLevel;
        public override Characteristics Characteristics => _characteristics;
        public GameObject GameObject => gameObject;
        public ArmorItemType ArmorItemType => ArmorItemType.Shield;
        public int ArmorValue => _armorValue;
    }
}