using System;
using System.Collections.Generic;
using System.Linq;
using Code.Equipment;
using Code.Extension;
using Code.Unit.Factories;

namespace Code.Data.Unit
{
    public class UnitEquipment
    {
        private readonly UnitPerson _person;
        private readonly CharacterEquipment _equipment;
        private readonly int _characterLevel;
        private readonly List<EquipCell> _cells = new List<EquipCell>();

        public List<EquipCell> Cells => _cells;

        #region Properties

        public float FullAgility
        {
            get
            {
                float result = 0;
                foreach (var cell in _cells.Where(cell => !cell.IsEmpty))
                    result = +cell.Body.Characteristics.Agility;
                return result;
            }
        }

        public float FullIntellect
        {
            get
            {
                float result = 0;
                foreach (var cell in _cells.Where(cell => !cell.IsEmpty))
                    result = +cell.Body.Characteristics.Intellect;
                return result;
            }
        }

        public float FullSpirit
        {
            get
            {
                float result = 0;
                foreach (var cell in _cells.Where(cell => !cell.IsEmpty))
                    result = +cell.Body.Characteristics.Spirit;
                return result;
            }
        }

        public float FullStamina
        {
            get
            {
                float result = 0;
                foreach (var cell in _cells.Where(cell => !cell.IsEmpty))
                    result = +cell.Body.Characteristics.Stamina;
                return result;
            }
        }

        public float FullStrength
        {
            get
            {
                float result = 0;
                foreach (var cell in _cells.Where(cell => !cell.IsEmpty))
                    result = +cell.Body.Characteristics.Strength;
                return result;
            }
        }

        public float FullArmor
        {
            get
            {
                float result = 0;
                foreach (var cell in _cells.Where(cell => !cell.IsEmpty))
                    result = +cell.Body.ArmorValue;
                return result;
            }
        }

        public int GetFullEquipmentItemsLevel
        {
            get
            {
                int result = 0;
                foreach (var cell in _cells.Where(cell => !cell.IsEmpty))
                    result = +cell.Body.ItemLevel;
                return result;
            }
        }

        #endregion

        public UnitEquipment(UnitPerson person, CharacterEquipment equipment, int characterLevel)
        {
            _person = person;
            _equipment = equipment;
            _characterLevel = characterLevel;

            #region Create cell list

            var mainHand = new EquipCell(EquipCellType.MainHand,
                PermissionFormMainHand(_equipment.permissionForEquipment), new List<TargetEquipCell>
                {
                    TargetEquipCell.OneHand,
                    TargetEquipCell.MainHand,
                    TargetEquipCell.TwoHand
                });
            _cells.Add(mainHand);

            var secondHand = new EquipCell(EquipCellType.SecondHand,
                PermissionFormSecondHand(_equipment.permissionForEquipment), new List<TargetEquipCell>
                {
                    TargetEquipCell.OneHand,
                    TargetEquipCell.SecondHand,
                    TargetEquipCell.Shield
                });
            _cells.Add(secondHand);

            mainHand.SetDepend(secondHand);
            secondHand.SetDepend(mainHand);

            var head = new EquipCell(EquipCellType.Head, PermissionFormArmor(_equipment.permissionForEquipment),
                new List<TargetEquipCell>
                {
                    TargetEquipCell.Head
                });
            _cells.Add(head);

            var neck = new EquipCell(EquipCellType.Neck, PermissionFormArmor(_equipment.permissionForEquipment),
                new List<TargetEquipCell>
                {
                    TargetEquipCell.Neck
                });
            _cells.Add(neck);

            var shoulder = new EquipCell(EquipCellType.Shoulder, PermissionFormArmor(_equipment.permissionForEquipment),
                new List<TargetEquipCell>
                {
                    TargetEquipCell.Shoulder
                });
            _cells.Add(shoulder);

            var body = new EquipCell(EquipCellType.Body, PermissionFormArmor(_equipment.permissionForEquipment),
                new List<TargetEquipCell>
                {
                    TargetEquipCell.Body
                });
            _cells.Add(body);

            var cloak = new EquipCell(EquipCellType.Cloak, PermissionFormArmor(_equipment.permissionForEquipment),
                new List<TargetEquipCell>
                {
                    TargetEquipCell.Cloak
                });
            _cells.Add(cloak);

            var bracelet = new EquipCell(EquipCellType.Bracelet, PermissionFormArmor(_equipment.permissionForEquipment),
                new List<TargetEquipCell>
                {
                    TargetEquipCell.Bracelet
                });
            _cells.Add(bracelet);

            var gloves = new EquipCell(EquipCellType.Gloves, PermissionFormArmor(_equipment.permissionForEquipment),
                new List<TargetEquipCell>
                {
                    TargetEquipCell.Gloves
                });
            _cells.Add(gloves);

            var belt = new EquipCell(EquipCellType.Belt, PermissionFormArmor(_equipment.permissionForEquipment),
                new List<TargetEquipCell>
                {
                    TargetEquipCell.Belt
                });
            _cells.Add(belt);

            var pants = new EquipCell(EquipCellType.Pants, PermissionFormArmor(_equipment.permissionForEquipment),
                new List<TargetEquipCell>
                {
                    TargetEquipCell.Pants
                });
            _cells.Add(pants);

            var shoes = new EquipCell(EquipCellType.Shoes, PermissionFormArmor(_equipment.permissionForEquipment),
                new List<TargetEquipCell>
                {
                    TargetEquipCell.Shoes
                });
            _cells.Add(shoes);

            var ring1 = new EquipCell(EquipCellType.Ring1, PermissionFormArmor(_equipment.permissionForEquipment),
                new List<TargetEquipCell>
                {
                    TargetEquipCell.Ring
                });
            _cells.Add(ring1);

            var ring2 = new EquipCell(EquipCellType.Ring2, PermissionFormArmor(_equipment.permissionForEquipment),
                new List<TargetEquipCell>
                {
                    TargetEquipCell.Ring
                });
            _cells.Add(ring2);

            var earring1 = new EquipCell(EquipCellType.Earring1, PermissionFormArmor(_equipment.permissionForEquipment),
                new List<TargetEquipCell>
                {
                    TargetEquipCell.Earring
                });
            _cells.Add(earring1);

            var earring2 = new EquipCell(EquipCellType.Earring2, PermissionFormArmor(_equipment.permissionForEquipment),
                new List<TargetEquipCell>
                {
                    TargetEquipCell.Earring
                });
            _cells.Add(earring2);

            #endregion

            var listForSendToInventory = new List<BaseEquipItem>();

            foreach (var equipItem in _equipment.Equipment)
            {
                // Dbg.Log($"equipItem:{equipItem}");
                if (equipItem.InventoryType != InventoryItemType.EquipItem)
                {
                    listForSendToInventory.Add(equipItem);
                    continue;
                }

                var ifCanPutOn = false;
                var equipCell = new EquipCell();
                foreach (var cell in _cells)
                {
                    if (!cell.IsCenEquip(equipItem))
                        continue;
                    ifCanPutOn = true;
                    equipCell = cell;
                    break;
                }

                if (ifCanPutOn)
                {
                    equipCell.Body = equipItem;
                    _person.PutOn(equipItem, equipCell.EquipCellType);
                }
                else
                {
                    listForSendToInventory.Add(equipItem);
                }
            }

            foreach (var item in listForSendToInventory)
            {
                SentToInventory(item);
            }

            _person.RemoveHiddenItems();
        }


        private PermissionToCarryEquipment PermissionFormArmor(PermissionToCarryEquipment settings)
        {
            var result = new PermissionToCarryEquipment();
            result.HeavyArmor = settings.HeavyArmor;
            result.MediumArmor = settings.MediumArmor;
            result.LightArmor = settings.LightArmor;
            return result;
        }

        private PermissionToCarryEquipment PermissionFormSecondHand(PermissionToCarryEquipment settings)
        {
            var result = new PermissionToCarryEquipment();
            result.OneHandWeapon = settings.OneHandWeapon;
            result.TwoOneHandWeapon = settings.TwoOneHandWeapon;
            result.Shield = settings.Shield;
            return result;
        }

        private PermissionToCarryEquipment PermissionFormMainHand(PermissionToCarryEquipment settings)
        {
            var result = new PermissionToCarryEquipment();
            result.OneHandWeapon = settings.OneHandWeapon;
            result.TwoHandSpearWeapon = settings.TwoHandSpearWeapon;
            result.TwoHandStaffWeapon = settings.TwoHandStaffWeapon;
            result.TwoHandSwordWeapon = settings.TwoHandSwordWeapon;
            result.TwoOneHandWeapon = settings.TwoOneHandWeapon;
            result.RangeTwoHandBowWeapon = settings.RangeTwoHandBowWeapon;
            result.RangeTwoHandCrossbowWeapon = settings.RangeTwoHandCrossbowWeapon;
            return result;
        }

        public void SentToInventory(BaseEquipItem item)
        {
            // Dbg.Log($"------------------- SentToInventory:{item.name}");
            _equipment.Equipment.Remove(item);
            _equipment.Inventory.Add(item);
        }

        public void SendToEquipment(BaseEquipItem item, EquipCellType fromTypeCell)
        {
            // Dbg.Log($"------------------- SentToEquipment:{item.name}");
            _equipment.Inventory.Remove(item);
            _equipment.Equipment.Add(item);
            return;
        }

        public List<EquipCell> GetWeapons()
        {
            var result = new List<EquipCell>();
            foreach (var cell in _cells)
            {
                if (cell.IsEmpty) continue;
                if (cell.Body.EquipType == EquipItemType.Weapon)
                    result.Add(cell);
            }

            return result;
        }

        public int GetWeaponType(){
            var list = GetWeapons();
            if (list.Count == 0)
                return 0;//Unarmed
            if (list.Count >= 2)
                return 5; //DualWeapons
            
            switch (((WeaponEquipItem)list[0].Body).WeaponType)
            {
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
        
                //ToDo добавить анимации ружья
                // case EquipmentType.RangeTwoHandGunWeapon:
                //     break; 
            }
            
            return 0;
        }
    }
}