using System;
using Data;
using Unit.Player;
using UnityEngine;
using Object = UnityEngine.Object;


namespace CharacterCustomizing
{
    public sealed class EquipmentWeapon
    {
        private readonly IPlayerView _character;
        private readonly CharacterEquipment _equipment;

        public EquipmentWeapon(IPlayerView character, CharacterEquipment equipment)
        {
            _character = character;
            _equipment = equipment;

            _character.EquipmentItems = new EquipmentItems();
        }

        public void GetWeapons()
        {
            //MainHand Init
            _character.EquipmentItems.MainWeapon = new EquipmentSlot
            {
                View = Object.Instantiate(_equipment.MainWeapon.View, Vector3.zero, Quaternion.identity),
                Type = _equipment.MainWeapon.Type
            };
            //SecondHand Init
            _character.EquipmentItems.SecondWeapon = new EquipmentSlot
            {
                View = Object.Instantiate(_equipment.SecondWeapon.View, Vector3.zero, Quaternion.identity),
                Type = _equipment.SecondWeapon.Type
            };
            //Set Parent
            switch (_equipment.MainWeapon.Type)
            {
                case EquipmentType.OneHandWeapon:
                    _character.EquipmentItems.MainWeapon.View.transform.SetParent(
                        _character.EquipmentPoints._rightWeaponAttachPoint.transform, false);
                    break;

                case EquipmentType.TwoHandSwordWeapon:
                    _character.EquipmentItems.MainWeapon.View.transform.SetParent(
                        _character.EquipmentPoints._rightWeaponAttachPoint.transform, false);
                    break;

                case EquipmentType.TwoHandSpearWeapon:
                    _character.EquipmentItems.MainWeapon.View.transform.SetParent(
                        _character.EquipmentPoints._rightWeaponAttachPoint.transform, false);
                    break;

                case EquipmentType.TwoHandStaffWeapon:
                    _character.EquipmentItems.MainWeapon.View.transform.SetParent(
                        _character.EquipmentPoints._rightWeaponAttachPoint.transform, false);
                    break;

                case EquipmentType.RangeTwoHandBowWeapon:
                    _character.EquipmentItems.MainWeapon.View.transform.SetParent(
                        _character.EquipmentPoints._leftWeaponAttachPoint.transform, false);
                    break;

                case EquipmentType.RangeTwoHandCrossbowWeapon:
                    _character.EquipmentItems.MainWeapon.View.transform.SetParent(
                        _character.EquipmentPoints._rightWeaponAttachPoint.transform, false);
                    break;
            }
            
            //Set Parent
            switch (_equipment.SecondWeapon.Type)
            {
                case EquipmentType.OneHandWeapon:
                    _character.EquipmentItems.SecondWeapon.View.transform.SetParent(
                        _character.EquipmentPoints._leftWeaponAttachPoint.transform, false);
                    break;

                case EquipmentType.Shild:
                    _character.EquipmentItems.SecondWeapon.View.transform.SetParent(
                        _character.EquipmentPoints._leftShildAttachPoint.transform, false);
                    break;

                case EquipmentType.ExtraWeapon:
                    _character.EquipmentItems.SecondWeapon.View.transform.SetParent(
                        _character.EquipmentPoints._leftWeaponAttachPoint.transform, false);
                    break;
            }
            
        }

        public int GetWeaponType()
        {
            if (_character.EquipmentItems.MainWeapon == null)
                return 0;//Unarmed
            if (_character.EquipmentItems.MainWeapon.Type == EquipmentType.OneHandWeapon && 
                _character.EquipmentItems.SecondWeapon.Type == EquipmentType.OneHandWeapon)
                return 5;//DualWeapons
            switch (_character.EquipmentItems.MainWeapon.Type)
            {
                case EquipmentType.OneHandWeapon:
                    return 1;
                    break;

                case EquipmentType.TwoHandSwordWeapon:
                    return 23;
                    break;

                case EquipmentType.TwoHandSpearWeapon:
                    return 22;
                    break;

                case EquipmentType.TwoHandStaffWeapon:
                    return 24;
                    break;

                case EquipmentType.RangeTwoHandBowWeapon:
                    return 25;
                    break;

                case EquipmentType.RangeTwoHandCrossbowWeapon:
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