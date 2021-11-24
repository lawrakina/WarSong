using System;
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

        public UnitBattle(UnitCharacteristics characteristics, WeaponEquipItem weaponEquipItem){
            var weapon = new Weapon(weaponEquipItem, EquipCellType.MainHand, characteristics);
            _weapons.Add(weapon);
        }

        public int GetMainWeaponType(){
            if (_weapons.Count == 0)
                return 0;
            switch (_weapons[0].WeaponType){
                case WeaponItemType.OneHandWeapon:
                    return 1;
                    break;
        
                case WeaponItemType.TwoHandSwordWeapon:
                    return 23;
                    break;
        
                case WeaponItemType.TwoHandSpearWeapon:
                    return 22;
                    break;
        
                case WeaponItemType.TwoHandStaffWeapon:
                    return 24;
                    break;
        
                case WeaponItemType.RangeTwoHandBowWeapon:
                    return 25;
                    break;
        
                case WeaponItemType.RangeTwoHandCrossbowWeapon:
                    return 26;
                    break;
            }

            return 0;
        }
    }
}