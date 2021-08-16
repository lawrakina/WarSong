using System;
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
        private List<BaseEquipItem> _listEquipmentItems = new List<BaseEquipItem>();
        public BaseWeapon MainWeapon = null;
        public BaseWeapon SecondWeapon = null;
        public BaseShield Shield = null;
        public ActiveWeapons ActiveWeapons;

        private float _fullAgility = 0f;
        private float _fullIntellect = 0f;
        private float _fullSpirit = 0f;
        private float _fullStamina = 0f;
        private float _fullStrength = 0f;

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

        public float FullAgility => _fullAgility;
        public float FullIntellect => _fullIntellect;
        public float FullSpirit => _fullSpirit;
        public float FullStamina => _fullStamina;
        public float FullStrength => _fullStrength;

        public void RebuildEquipment()
        {
            ClearAllSlots();
            foreach (var item in _characterEquipment.Equipment)
            {
                var ifEquipItem = false;
                switch (item.ItemType)
                {
                    case InventoryItemType.Weapon:
                        PutOnWeapon(item);
                        ifEquipItem = true;
                        break;
                    case InventoryItemType.Armor:
                        PutOnArmor(item);
                        ifEquipItem = true;
                        break;
                    case InventoryItemType.QuestItem:
                        //ToDo тут можно сделать новый квест при старте уровня. Например: "На вас черная метка, охотник уже идет по следу..."
                        ifEquipItem = false;
                        break;
                    case InventoryItemType.Trash:
                        ifEquipItem = false;
                        break;
                    case InventoryItemType.Food:
                        ifEquipItem = false;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }

                if (!ifEquipItem)
                {
                    _characterEquipment.Equipment.Remove(item);
                    _characterEquipment.Inventory.Add(item);
                }
            }
        }

        private void PutOnArmor(BaseEquipItem item)
        {
            if (item is IBaseShield shield)
            {
                if (_characterEquipment.permissionForEquipment.Shield && ActiveWeapons == ActiveWeapons.RightHand)
                {
                    Shield = Object.Instantiate(shield.GameObject, Vector3.zero, Quaternion.identity)
                        .GetComponent<BaseShield>();
                    Shield.transform.SetParent(_equipmentPoints._leftShildAttachPoint, false);
                    ActiveWeapons = ActiveWeapons.RightAndShield;
                }

                return;
            }

            if (item is IBaseArmor)
            {
                // добавить слоты армора и рассортировать по одежке
            }
        }

        private void PutOnWeapon(BaseEquipItem item)
        {
            AddCharacteristics(item);
            var weapon = item as BaseWeapon;
            switch (weapon.WeaponType)
            {
                case WeaponItemType.OneHandWeapon:
                    if (_characterEquipment.permissionForEquipment.OneHandWeapon)
                    {
                        if (!MainWeapon)
                            SetWeapon(weapon.Inst(), out MainWeapon,
                                _equipmentPoints._rightWeaponAttachPoint, ActiveWeapons.RightHand);
                        else if (!SecondWeapon && _characterEquipment.permissionForEquipment.TwoOneHandWeapon)
                            SetWeapon(weapon.Inst(), out SecondWeapon,
                                _equipmentPoints._leftWeaponAttachPoint, ActiveWeapons.RightAndLeft);
                    }

                    break;
                case WeaponItemType.TwoHandSwordWeapon:
                    if (_characterEquipment.permissionForEquipment.TwoHandSwordWeapon)
                        SetWeapon(weapon.Inst(), out MainWeapon,
                            _equipmentPoints._rightWeaponAttachPoint, ActiveWeapons.TwoHand);

                    break;
                case WeaponItemType.TwoHandSpearWeapon:
                    if (_characterEquipment.permissionForEquipment.TwoHandSpearWeapon)
                        SetWeapon(weapon.Inst(), out MainWeapon,
                            _equipmentPoints._rightWeaponAttachPoint, ActiveWeapons.TwoHand);

                    break;
                case WeaponItemType.TwoHandStaffWeapon:
                    if (_characterEquipment.permissionForEquipment.TwoHandStaffWeapon)
                        SetWeapon(weapon.Inst(), out MainWeapon,
                            _equipmentPoints._rightWeaponAttachPoint, ActiveWeapons.TwoHand);

                    break;
                case WeaponItemType.RangeTwoHandBowWeapon:
                    if (_characterEquipment.permissionForEquipment.RangeTwoHandBowWeapon)
                        SetWeapon(weapon.Inst(), out MainWeapon,
                            _equipmentPoints._leftWeaponAttachPoint, ActiveWeapons.TwoHand);

                    break;
                case WeaponItemType.RangeTwoHandCrossbowWeapon:
                    if (_characterEquipment.permissionForEquipment.RangeTwoHandCrossbowWeapon)
                        SetWeapon(weapon.Inst(), out MainWeapon,
                            _equipmentPoints._rightWeaponAttachPoint, ActiveWeapons.TwoHand);

                    break;
            }
        }

        private void AddCharacteristics(BaseEquipItem item)
        {
            _fullStrength += item.Characteristics.Strength;
            _fullAgility += item.Characteristics.Agility;
            _fullIntellect += item.Characteristics.Intellect;
            _fullSpirit += item.Characteristics.Spirit;
            _fullStamina += item.Characteristics.Stamina;
            _listEquipmentItems.Add(item);
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

            _listEquipmentItems = new List<BaseEquipItem>();
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

            switch (MainWeapon.WeaponType)
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

            if (MainWeapon.WeaponType == WeaponItemType.OneHandWeapon &&
                SecondWeapon.WeaponType == WeaponItemType.OneHandWeapon)
                return 5; //DualWeapons
            return 0;
        }
    }
}