using System.Collections.Generic;
using Code.Equipment;

namespace Code.Data.Unit
{
    public class EquipCell
    {
        private readonly EquipCellType _equipCellType;
        private readonly PermissionToCarryEquipment _permissions;
        private readonly List<TargetEquipCell> _targetEquipCells;
        private BaseEquipItem _body;
        public bool IsEmpty;
        private EquipCell _dependCell;
        private bool _isDepend = false;
        public EquipCellType EquipCellType => _equipCellType;
        public List<TargetEquipCell> TargetEquipCells => _targetEquipCells;

        public BaseEquipItem Body
        {
            get { return _body; }
            set
            {
                if (value != null)
                    IsEmpty = false;
                else
                    IsEmpty = true;
                _body = value;
            }
        }

        public EquipCell()
        {
        }

        public EquipCell(EquipCellType equipCellType, PermissionToCarryEquipment permissions,
            List<TargetEquipCell> targetEquipCells)
        {
            _equipCellType = equipCellType;
            _permissions = permissions;
            _targetEquipCells = targetEquipCells;
            Body = null;
            IsEmpty = true;
        }

        public bool IsCenEquip(BaseEquipItem equipItem)
        {
            if (!IsEmpty)
            {
                return false;
            }

            if (CheckPermissions(equipItem))
                if (CheckDependItems(equipItem))
                    return true;
            return false;
        }

        public void SetDepend(EquipCell dependCell)
        {
            _isDepend = true;
            _dependCell = dependCell;
        }

        private bool CheckPermissions(BaseEquipItem equipItem)
        {
            foreach (var targetEquipCell in _targetEquipCells)
            {
                if (targetEquipCell == equipItem.TargetEquipCell)
                {
                    switch (equipItem.EquipType)
                    {
                        case EquipItemType.Armor:
                            switch (((ArmorEquipItem) equipItem).HeavyLightMedium)
                            {
                                case HeavyLightMedium.NoRequire:
                                    return true;
                                    break;
                                case HeavyLightMedium.Heavy:
                                    if (_permissions.HeavyArmor)
                                        return true;
                                    break;
                                case HeavyLightMedium.Medium:
                                    if (_permissions.MediumArmor)
                                        return true;
                                    break;
                                case HeavyLightMedium.Light:
                                    if (_permissions.LightArmor)
                                        return true;
                                    break;
                            }

                            break;
                        case EquipItemType.Weapon:
                            switch (((WeaponEquipItem) equipItem).WeaponType)
                            {
                                case WeaponItemType.None:
                                    return true;
                                    break;
                                case WeaponItemType.OneHandWeapon:
                                    if (_permissions.OneHandWeapon)
                                        return true;
                                    break;
                                case WeaponItemType.TwoHandSwordWeapon:
                                    if (_permissions.TwoHandSwordWeapon)
                                        return true;
                                    break;
                                case WeaponItemType.TwoHandSpearWeapon:
                                    if (_permissions.TwoHandSpearWeapon)
                                        return true;
                                    break;
                                case WeaponItemType.TwoHandStaffWeapon:
                                    if (_permissions.TwoHandStaffWeapon)
                                        return true;
                                    break;
                                case WeaponItemType.RangeTwoHandBowWeapon:
                                    if (_permissions.RangeTwoHandBowWeapon)
                                        return true;
                                    break;
                                case WeaponItemType.RangeTwoHandCrossbowWeapon:
                                    if (_permissions.RangeTwoHandCrossbowWeapon)
                                        return true;
                                    break;
                            }

                            break;
                    }
                }
            }

            return false;
        }

        private bool CheckDependItems(BaseEquipItem equipItem)
        {
            if (!_isDepend) return true;

            switch (equipItem.TargetEquipCell)
            {
                //если целевая ячейка Двуручное оружие, это точно правая рука, надо проверить что левая рука пуста 
                case TargetEquipCell.TwoHand:
                    if (_dependCell.IsEmpty)
                        return true;
                    else
                        return false;
                //если целевая ячейка Щит, то это точно левая рука, надо проверить что правая не содержит двучучного оружия
                case TargetEquipCell.Shield:

                    if (_dependCell.IsEmpty)
                        return true;
                    if (_dependCell.Body.TargetEquipCell == TargetEquipCell.TwoHand)
                        return false;
                    break;
            }

            return true;
        }
    }
}