using System.Collections.Generic;
using Code.Equipment;


namespace Code.Data.Unit{
    public class UnitBattle{
        private readonly List<Weapon> _weapons = new List<Weapon>();
        public List<Weapon> Weapons => _weapons;

        public UnitBattle(UnitCharacteristics characteristics, UnitEquipment equip){
            foreach (var equipCell in equip.GetWeapons()){
                var weaponEquip = equipCell.Body as WeaponEquipItem;
                var weapon = new Weapon(weaponEquip, equipCell.EquipCellType, characteristics);
                _weapons.Add(weapon);
            }
        }
    }
}