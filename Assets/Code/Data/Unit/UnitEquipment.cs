using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Code.Equipment;
using Code.Unit.Factories;


namespace Code.Data.Unit
{
    public sealed class UnitEquipment
    {
        private readonly UnitPerson _person;
        private readonly List<BaseEquipItem> _list;
        private List<BaseArmorItem> _listArmor;

        public UnitEquipment(UnitPerson person)
        {
            _person = person;
            _list = _person.ListEquipmentItems;
            _listArmor = new List<BaseArmorItem>();
            foreach (var item in _list.Where(item => item.ItemType == InventoryItemType.Armor))
            {
                _listArmor.Add(item as BaseArmorItem);
            }
        }

        public float FullAgility => _list.Sum(item => item.Characteristics.Agility);
        public float FullIntellect => _list.Sum(item => item.Characteristics.Intellect);
        public float FullSpirit => _list.Sum(item => item.Characteristics.Spirit);
        public float FullStamina => _list.Sum(item => item.Characteristics.Stamina);
        public float FullStrength => _list.Sum(item => item.Characteristics.Strength);
        public float FullArmor => _listArmor.Sum(item => item.ArmorValue);
        public int GetEquipmentItemsLevel => _list.Sum(item => item.ItemLevel);
        public BaseWeapon MainWeapon => _person.MainWeapon;
        public BaseWeapon SecondWeapon => _person.SecondWeapon;
        public List<BaseArmorItem> ListArmor => _listArmor;
        public ActiveWeapons ActiveWeapons => _person.ActiveWeapons;
    }
}