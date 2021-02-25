﻿using System;
using Data;
using Extension;
using Unit;
using Unit.Player;
using UnityEngine;
using Weapons;
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
            _character.UnitBattle = new UnitBattle();
        }

        public void GetWeapons()
        {
            //MainHand Init
            var goBaseWeapon =
                Object.Instantiate(_equipment.MainWeapon.gameObject, Vector3.zero, Quaternion.identity);
            var mainWeapon = goBaseWeapon.GetComponent<BaseWeapon>();
            mainWeapon.Type = _equipment.MainWeapon.Type;
            Dbg.Log($"mainWeapon.StandardBullet:{mainWeapon.StandardBullet}");
            Dbg.Log($"_equipment.MainWeapon.gameObject:{_equipment.MainWeapon.StandardBullet}");
            _character.EquipmentItems.MainWeapon = mainWeapon;
            //SecondHand Init
            var goSecondWeapon =
                Object.Instantiate(_equipment.SecondWeapon.gameObject, Vector3.zero, Quaternion.identity);
            var secondWeapon = goSecondWeapon.GetComponent<BaseWeapon>();
            secondWeapon.Type = _equipment.SecondWeapon.Type;
            _character.EquipmentItems.SecondWeapon = secondWeapon;
            //Set Parent
            switch (_equipment.MainWeapon.Type)
            {
                case WeaponType.OneHandWeapon:
                    _character.EquipmentItems.MainWeapon.transform.SetParent(
                        _character.EquipmentPoints._rightWeaponAttachPoint.transform, false);
                    break;

                case WeaponType.TwoHandSwordWeapon:
                    _character.EquipmentItems.MainWeapon.transform.SetParent(
                        _character.EquipmentPoints._rightWeaponAttachPoint.transform, false);
                    break;

                case WeaponType.TwoHandSpearWeapon:
                    _character.EquipmentItems.MainWeapon.transform.SetParent(
                        _character.EquipmentPoints._rightWeaponAttachPoint.transform, false);
                    break;

                case WeaponType.TwoHandStaffWeapon:
                    _character.EquipmentItems.MainWeapon.transform.SetParent(
                        _character.EquipmentPoints._rightWeaponAttachPoint.transform, false);
                    break;

                case WeaponType.RangeTwoHandBowWeapon:
                    _character.EquipmentItems.MainWeapon.transform.SetParent(
                        _character.EquipmentPoints._leftWeaponAttachPoint.transform, false);
                    break;

                case WeaponType.RangeTwoHandCrossbowWeapon:
                    _character.EquipmentItems.MainWeapon.transform.SetParent(
                        _character.EquipmentPoints._rightWeaponAttachPoint.transform, false);
                    break;
            }
            
            //Set Parent
            switch (_equipment.SecondWeapon.Type)
            {
                case WeaponType.OneHandWeapon:
                    _character.EquipmentItems.SecondWeapon.transform.SetParent(
                        _character.EquipmentPoints._leftWeaponAttachPoint.transform, false);
                    break;

                case WeaponType.Shild:
                    _character.EquipmentItems.SecondWeapon.transform.SetParent(
                        _character.EquipmentPoints._leftShildAttachPoint.transform, false);
                    break;

                case WeaponType.ExtraWeapon:
                    _character.EquipmentItems.SecondWeapon.transform.SetParent(
                        _character.EquipmentPoints._leftWeaponAttachPoint.transform, false);
                    break;
            }
            
            //ToDo сейчас все считает только по главному орудию, следать расчет на обе руки
            //Set Characteristics
            _character.UnitBattle.Weapon = mainWeapon;
        }

        public int GetWeaponType()
        {
            if (_character.EquipmentItems.MainWeapon == null)
                return 0;//Unarmed
            if (_character.EquipmentItems.MainWeapon.Type == WeaponType.OneHandWeapon && 
                _character.EquipmentItems.SecondWeapon.Type == WeaponType.OneHandWeapon)
                return 5;//DualWeapons
            switch (_character.EquipmentItems.MainWeapon.Type)
            {
                case WeaponType.OneHandWeapon:
                    return 1;
                    break;

                case WeaponType.TwoHandSwordWeapon:
                    return 23;
                    break;

                case WeaponType.TwoHandSpearWeapon:
                    return 22;
                    break;

                case WeaponType.TwoHandStaffWeapon:
                    return 24;
                    break;

                case WeaponType.RangeTwoHandBowWeapon:
                    return 25;
                    break;

                case WeaponType.RangeTwoHandCrossbowWeapon:
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