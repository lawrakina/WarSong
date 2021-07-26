using Code.Data;
using Code.Data.Unit;
using Code.Equipment;
using Code.Extension;
using Code.Unit;
using UnityEngine;

namespace Code.CharacterCustomizing
{
    // public sealed class EquipmentWeapon
    // {
    //     private readonly IPlayerView _character;
    //     private UnitEquipment _equipment;
    //
    //     public EquipmentWeapon(IPlayerView character, UnitEquipment equipment)
    //     {
    //         _character = character;
    //         _equipment = equipment;
    //     }
    //
    //     public void GetWeapons(UnitEquipment newEquipment)
    //     {
    //         //clear old items
    //         Object.Destroy(_equipment.MainWeapon);
    //         Object.Destroy(_equipment.SecondWeapon);
    //         _equipment.MainWeapon = null;
    //         _equipment.SecondWeapon = null;
    //
    //         _equipment = newEquipment;
    //         
    //         //MainHand Init
    //         var goBaseWeapon = Object.Instantiate(_equipment.MainWeapon.gameObject, Vector3.zero, Quaternion.identity);
    //         var mainWeapon = goBaseWeapon.GetComponent<BaseWeapon>();
    //         mainWeapon.itemType = _equipment.MainWeapon.itemType;
    //         // Dbg.Log($"22 GoBaseWeapon {goBaseWeapon}");
    //         // Dbg.Log($"mainWeapon.StandardBullet:{mainWeapon.StandardBullet}");
    //         // Dbg.Log($"_equipment.MainWeapon.gameObject:{_equipment.MainWeapon.StandardBullet}");
    //         _character.EquipmentItems.MainWeapon = mainWeapon;
    //         // Dbg.Log($"22222 {_character.EquipmentItems.MainWeapon}");
    //         //todo create global database for storage Items
    //         //SecondHand Init
    //         var goSecondWeapon = Object.Instantiate(_equipment.SecondWeapon.gameObject, Vector3.zero, Quaternion.identity);
    //         var secondWeapon = goSecondWeapon.GetComponent<BaseWeapon>();
    //         secondWeapon.itemType = _equipment.SecondWeapon.itemType;
    //         _character.EquipmentItems.SecondWeapon = secondWeapon;
    //
    //         //Set Parent and set Types for Animator
    //         switch (_equipment.MainWeapon.itemType)
    //         {
    //             case WeaponItemType.OneHandWeapon:
    //                 _character.EquipmentItems.MainWeapon.transform.SetParent(
    //                     _character.EquipmentPoints._rightWeaponAttachPoint.transform, false);
    //                 _character.EquipmentItems.ActiveWeapons = ActiveWeapons.RightHand;
    //                 break;
    //
    //             case WeaponItemType.TwoHandSwordWeapon:
    //                 _character.EquipmentItems.MainWeapon.transform.SetParent(
    //                     _character.EquipmentPoints._rightWeaponAttachPoint.transform, false);
    //                 _character.EquipmentItems.ActiveWeapons = ActiveWeapons.TwoHand;
    //                 break;
    //
    //             case WeaponItemType.TwoHandSpearWeapon:
    //                 _character.EquipmentItems.MainWeapon.transform.SetParent(
    //                     _character.EquipmentPoints._rightWeaponAttachPoint.transform, false);
    //                 _character.EquipmentItems.ActiveWeapons = ActiveWeapons.TwoHand;
    //                 break;
    //
    //             case WeaponItemType.TwoHandStaffWeapon:
    //                 _character.EquipmentItems.MainWeapon.transform.SetParent(
    //                     _character.EquipmentPoints._rightWeaponAttachPoint.transform, false);
    //                 _character.EquipmentItems.ActiveWeapons = ActiveWeapons.TwoHand;
    //                 break;
    //
    //             case WeaponItemType.RangeTwoHandBowWeapon:
    //                 _character.EquipmentItems.MainWeapon.transform.SetParent(
    //                     _character.EquipmentPoints._leftWeaponAttachPoint.transform, false);
    //                 _character.EquipmentItems.ActiveWeapons = ActiveWeapons.TwoHand;
    //                 break;
    //
    //             case WeaponItemType.RangeTwoHandCrossbowWeapon:
    //                 _character.EquipmentItems.MainWeapon.transform.SetParent(
    //                     _character.EquipmentPoints._rightWeaponAttachPoint.transform, false);
    //                 _character.EquipmentItems.ActiveWeapons = ActiveWeapons.TwoHand;
    //                 break;
    //         }
    //
    //         //Set Parent
    //         switch (_equipment.SecondWeapon.itemType)
    //         {
    //             case WeaponItemType.OneHandWeapon:
    //                 _character.EquipmentItems.SecondWeapon.transform.SetParent(
    //                     _character.EquipmentPoints._leftWeaponAttachPoint.transform, false);
    //                 if (_character.EquipmentItems.ActiveWeapons == ActiveWeapons.RightHand)
    //                     _character.EquipmentItems.ActiveWeapons = ActiveWeapons.RightAndLeft;
    //                 break;
    //
    //             case WeaponItemType.Shield:
    //                 _character.EquipmentItems.SecondWeapon.transform.SetParent(
    //                     _character.EquipmentPoints._leftShildAttachPoint.transform, false);
    //                 if (_character.EquipmentItems.ActiveWeapons == ActiveWeapons.RightHand)
    //                     _character.EquipmentItems.ActiveWeapons = ActiveWeapons.RightAndShield;
    //                 break;
    //
    //             case WeaponItemType.ExtraWeapon:
    //                 _character.EquipmentItems.SecondWeapon.transform.SetParent(
    //                     _character.EquipmentPoints._leftWeaponAttachPoint.transform, false);
    //                 break;
    //         }
    //
    //         //ToDo сейчас все считает только по главному орудию, следать расчет на обе руки
    //         //Set Characteristics
    //         _character.EquipmentItems.MainWeapon = mainWeapon;
    //         _character.EquipmentItems.SecondWeapon = secondWeapon;
    //     }
    //
    //
    //     public int GetWeaponType()
    //     {
    //         if (_character.EquipmentItems.MainWeapon == null)
    //             return 0; //Unarmed
    //         if (_character.EquipmentItems.MainWeapon.Type == WeaponItemType.OneHandWeapon &&
    //             _character.EquipmentItems.SecondWeapon.Type == WeaponItemType.OneHandWeapon)
    //             return 5; //DualWeapons
    //         switch (_character.EquipmentItems.MainWeapon.Type)
    //         {
    //             case WeaponItemType.OneHandWeapon:
    //                 return 1;
    //                 break;
    //
    //             case WeaponItemType.TwoHandSwordWeapon:
    //                 return 23;
    //                 break;
    //
    //             case WeaponItemType.TwoHandSpearWeapon:
    //                 return 22;
    //                 break;
    //
    //             case WeaponItemType.TwoHandStaffWeapon:
    //                 return 24;
    //                 break;
    //
    //             case WeaponItemType.RangeTwoHandBowWeapon:
    //                 return 25;
    //                 break;
    //
    //             case WeaponItemType.RangeTwoHandCrossbowWeapon:
    //                 return 26;
    //                 break;
    //
    //             //ToDo добавить анимации ружья
    //             // case EquipmentType.RangeTwoHandGunWeapon:
    //             //     break; 
    //         }
    //
    //         return 0;
    //     }
    // }
}