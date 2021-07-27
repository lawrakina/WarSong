using System.Collections.Generic;
using System.Linq;
using Code.Equipment;
using Code.Extension;
using Code.Unit;
using UnityEngine;
using Object = UnityEngine.Object;


namespace Code.Data.Unit
{
    public sealed class UnitEquipment
    {
        private readonly EquipmentPoints _equipmentPoints;
        private CharacterEquipment _characterEquipment;
        public BaseWeapon MainWeapon = null;
        public BaseWeapon SecondWeapon = null;
        public BaseShield Shield = null;
        public ActiveWeapons ActiveWeapons;

        public UnitEquipment(EquipmentPoints equipmentPoints, CharacterEquipment characterEquipment)
        {
            _equipmentPoints = equipmentPoints;
            _characterEquipment = characterEquipment;

            RebuildEquipment();
        }

        public int GetEquipmentItemsLevel
        {
            get
            {
                var result = 0;
                if (MainWeapon)
                    result += MainWeapon.ItemLevel;
                if (SecondWeapon)
                    result += SecondWeapon.ItemLevel;
                if (Shield)
                    result += Shield.ItemLevel;
                //ToDo need add all slots
                
                return result;
            }
        }

        public int FullArmor
        {
            get
            {
                var result = 0;
                if (Shield)
                    result += Shield.ArmorValue;
                //ToDo need add all armors 

                return result;
            }
        }

        //ToDo сделать рассчет по каждой характеристике по всем одетым предметам
        public float FullAgility { get; set; }
        public float FullIntellect { get; set; }
        public float FullSpirit { get; set; }
        public float FullStamina { get; set; }
        public float FullStrength { get; set; }

        public void RebuildEquipment()
        {
            ClearAllSlots();
            EquipItems(_characterEquipment.Slots);
        }

        private void ClearAllSlots()
        {
            if (MainWeapon)
            {
                Object.Destroy(MainWeapon);
                MainWeapon = null;
            }

            if (SecondWeapon)
            {
                Object.Destroy(SecondWeapon);
                SecondWeapon = null;
            }

            var children
                = (from Transform child in _equipmentPoints._rightWeaponAttachPoint select child.gameObject).ToList();
            children.ForEach(Object.Destroy);

            children =
                (from Transform child in _equipmentPoints._leftWeaponAttachPoint select child.gameObject).ToList();
            children.ForEach(Object.Destroy);

            children = (from Transform child in _equipmentPoints._leftShildAttachPoint select child.gameObject)
                .ToList();
            children.ForEach(Object.Destroy);
            children = null;
        }

        private void EquipItems(IEnumerable<GameObject> slots)
        {
            foreach (var item in slots)
            {
                EquipItem(item);
            }
        }

        private void EquipItem(GameObject item)
        {
            if (item.TryGetComponent(out BaseWeapon componentWeapon))
            {
                switch (componentWeapon.itemType)
                {
                    case WeaponItemType.OneHandWeapon:
                        if (_characterEquipment.permissionForEquipment.OneHandWeapon)
                        {
                            if (!MainWeapon)
                                SetWeapon(componentWeapon.Inst(), out MainWeapon,
                                    _equipmentPoints._rightWeaponAttachPoint, ActiveWeapons.RightHand);
                            else if (!SecondWeapon && _characterEquipment.permissionForEquipment.TwoOneHandWeapon)
                                SetWeapon(componentWeapon.Inst(), out SecondWeapon,
                                    _equipmentPoints._leftWeaponAttachPoint, ActiveWeapons.RightAndLeft);
                        }

                        break;
                    case WeaponItemType.TwoHandSwordWeapon:
                        if (_characterEquipment.permissionForEquipment.TwoHandSwordWeapon)
                            SetWeapon(componentWeapon.Inst(), out MainWeapon,
                                _equipmentPoints._rightWeaponAttachPoint, ActiveWeapons.TwoHand);

                        break;
                    case WeaponItemType.TwoHandSpearWeapon:
                        if (_characterEquipment.permissionForEquipment.TwoHandSpearWeapon)
                            SetWeapon(componentWeapon.Inst(), out MainWeapon,
                                _equipmentPoints._rightWeaponAttachPoint, ActiveWeapons.TwoHand);

                        break;
                    case WeaponItemType.TwoHandStaffWeapon:
                        if (_characterEquipment.permissionForEquipment.TwoHandStaffWeapon)
                            SetWeapon(componentWeapon.Inst(), out MainWeapon,
                                _equipmentPoints._rightWeaponAttachPoint, ActiveWeapons.TwoHand);

                        break;
                    case WeaponItemType.RangeTwoHandBowWeapon:
                        if (_characterEquipment.permissionForEquipment.RangeTwoHandBowWeapon)
                            SetWeapon(componentWeapon.Inst(), out MainWeapon,
                                _equipmentPoints._leftWeaponAttachPoint, ActiveWeapons.TwoHand);

                        break;
                    case WeaponItemType.RangeTwoHandCrossbowWeapon:
                        if (_characterEquipment.permissionForEquipment.RangeTwoHandCrossbowWeapon)
                            SetWeapon(componentWeapon.Inst(), out MainWeapon,
                                _equipmentPoints._rightWeaponAttachPoint, ActiveWeapons.TwoHand);

                        break;
                }
            }

            if (item.TryGetComponent(out BaseShield componentShield))
            {
                if (_characterEquipment.permissionForEquipment.Shield && ActiveWeapons == ActiveWeapons.RightHand)
                {
                    Shield = Object.Instantiate(componentShield, Vector3.zero, Quaternion.identity);
                    Shield.transform.SetParent(_equipmentPoints._leftShildAttachPoint, false);
                    ActiveWeapons = ActiveWeapons.RightAndShield;
                }
            }
        }

        private void SetWeapon(BaseWeapon weapon, out BaseWeapon currentWeapon, Transform attachPoint,
            ActiveWeapons activeWeapons)
        {
            currentWeapon = weapon;
            currentWeapon.transform.SetParent(attachPoint, false);
            ActiveWeapons = activeWeapons;
        }

        public void SetEquipment(CharacterEquipment equipment)
        {
            _characterEquipment = equipment;
        }

        public int GetWeaponType()
        {
            if (MainWeapon == null)
                return 0; //Unarmed

            switch (MainWeapon.itemType)
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

            if (MainWeapon.itemType == WeaponItemType.OneHandWeapon &&
                SecondWeapon.itemType == WeaponItemType.OneHandWeapon)
                return 5; //DualWeapons
            return 0;
        }
    }
}