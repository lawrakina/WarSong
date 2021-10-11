using System;
using System.Collections.Generic;
using System.Linq;
using Code.CharacterCustomizing;
using Code.Equipment;
using Code.Extension;
using Code.Unit;
using UnityEngine;
using Object = UnityEngine.Object;


namespace Code.Data.Unit
{
    // public sealed class UnitEquipment_Old_cash
    // {
    //     private readonly EquipmentPoints _equipmentPoints;
    //     private CharacterEquipment _characterEquipment;
    //     private readonly PersonCharacter _personCharacter;
    //     private List<BaseArmorItem> _listEquipmentItems = new List<BaseArmorItem>();
    //     public BaseWeapon MainWeapon = null;
    //     public BaseWeapon SecondWeapon = null;
    //     public ShieldEquip ShieldEquip = null;
    //     public ActiveWeapons ActiveWeapons;
    //
    //     public HeadEquip HeadEquip = null;
    //     public NeckEquip NeckEquip = null;
    //     public ShoulderEquip ShoulderEquip = null;
    //     public BodyEquip BodyEquip = null;
    //     public CloakEquip CloakEquip = null;
    //     public BraceletEquip BraceletEquip = null;
    //     public GlovesEquip GlovesEquip = null;
    //     public BeltEquip BeltEquip = null;
    //     public PantsEquip PantsEquip = null;
    //     public ShoesEquip ShoesEquip = null;
    //     public RingEquip Ring1Equip = null;
    //     public RingEquip Ring2Equip = null;
    //     public EarringEquip Earring1Equip = null;
    //     public EarringEquip Earring2Equip = null;
    //
    //
    //     private float _fullAgility = 0f;
    //     private float _fullIntellect = 0f;
    //     private float _fullSpirit = 0f;
    //     private float _fullStamina = 0f;
    //     private float _fullStrength = 0f;
    //
    //     public UnitEquipment_Old_cash(EquipmentPoints equipmentPoints, CharacterEquipment characterEquipment,
    //         PersonCharacter personCharacter)
    //     {
    //         _equipmentPoints = equipmentPoints;
    //         _characterEquipment = characterEquipment;
    //         _personCharacter = personCharacter;
    //
    //         RebuildEquipment();
    //     }
    //
    //     public int GetEquipmentItemsLevel
    //     {
    //         get
    //         {
    //             var result = 0;
    //             if (MainWeapon)
    //                 result += MainWeapon.ItemLevel;
    //             if (SecondWeapon)
    //                 result += SecondWeapon.ItemLevel;
    //             if (ShieldEquip)
    //                 result += ShieldEquip.ItemLevel;
    //             //ToDo need add all slots
    //
    //             return result;
    //         }
    //     }
    //
    //     public int FullArmor
    //     {
    //         get
    //         {
    //             var result = 0;
    //             if (ShieldEquip)
    //                 result += ShieldEquip.ArmorValue;
    //             //ToDo need add all armors 
    //
    //             return result;
    //         }
    //     }
    //
    //     public float FullAgility => _fullAgility;
    //     public float FullIntellect => _fullIntellect;
    //     public float FullSpirit => _fullSpirit;
    //     public float FullStamina => _fullStamina;
    //     public float FullStrength => _fullStrength;
    //
    //     public void RebuildEquipment()
    //     {
    //         ClearAllSlots();
    //         _personCharacter.Regenerate();
    //         foreach (var item in _characterEquipment.Equipment)
    //         {
    //             var ifEquipItem = false;
    //             switch (item.InventoryType)
    //             {
    //                 case InventoryItemType.Weapon:
    //                     PutOnWeapon(item);
    //                     ifEquipItem = true;
    //                     break;
    //                 case InventoryItemType.Armor:
    //                     PutOnArmor(item as BaseArmorItem);
    //                     ifEquipItem = true;
    //                     break;
    //                 case InventoryItemType.QuestItem:
    //                     //ToDo тут можно сделать новый квест при старте уровня. Например: "На вас черная метка, охотник уже идет по следу..."
    //                     ifEquipItem = false;
    //                     break;
    //                 case InventoryItemType.Trash:
    //                     ifEquipItem = false;
    //                     break;
    //                 case InventoryItemType.Food:
    //                     ifEquipItem = false;
    //                     break;
    //                 default:
    //                     throw new ArgumentOutOfRangeException();
    //             }
    //
    //             PutAllEquip();
    //
    //             if (!ifEquipItem)
    //             {
    //                 SentToInventory(item);
    //             }
    //         }
    //
    //     }
    //
    //     public void PutAllEquip()
    //     {
    //         if (HeadEquip)
    //         {
    //             foreach (var view in HeadEquip.ListViews)
    //             {
    //                 view.SetActive(true);
    //                 view.active = true;
    //             }
    //         }
    //     }
    //
    //
    //     private void PutOnArmor(BaseArmorItem item)
    //     {
    //         Dbg.Log($"PuOnArmor:{item.NameInHierarchy}");
    //         if(!item)
    //             return;
    //         if (!CheckPermissionForEquipment(item))
    //         {
    //             SentToInventory(item);
    //             return;
    //         }
    //         List<GameObject> listOfGameObjectsViews = new List<GameObject>();
    //         switch (item)
    //         {
    //             case ShieldEquip shield:
    //             {
    //                 if (ActiveWeapons == ActiveWeapons.RightHand)
    //                 {
    //                     ShieldEquip = Object.Instantiate(shield.GameObject, Vector3.zero, Quaternion.identity)
    //                         .GetComponent<ShieldEquip>();
    //                     ShieldEquip.transform.SetParent(_equipmentPoints._leftShildAttachPoint, false);
    //                     ActiveWeapons = ActiveWeapons.RightAndShield;
    //                 }
    //                 return;
    //             }
    //             case HeadEquip head:
    //             {
    //                 if (!HeadEquip && _personCharacter.PutOnArmor(head, ref listOfGameObjectsViews))
    //                 {
    //                     AddCharacteristics(head);
    //                     HeadEquip = head;
    //                     HeadEquip.ListViews = listOfGameObjectsViews;
    //                 }
    //
    //                 break;
    //             }
    //             case NeckEquip neck:
    //             {
    //                 if (!NeckEquip)
    //                 {
    //                     AddCharacteristics(neck);
    //                     NeckEquip = neck;
    //                 }
    //
    //                 break;
    //             }
    //             case ShoulderEquip shoulder:
    //             {
    //                 if (!ShoulderEquip && _personCharacter.PutOnArmor(shoulder, ref listOfGameObjectsViews))
    //                 {
    //                     AddCharacteristics(shoulder);
    //                     ShoulderEquip = shoulder;
    //                     ShoulderEquip.ListViews = listOfGameObjectsViews;
    //                 }
    //
    //                 break;
    //             }
    //             case BodyEquip body:
    //             {
    //                 if (!BodyEquip && _personCharacter.PutOnArmor(body, ref listOfGameObjectsViews))
    //                 {
    //                     AddCharacteristics(body);
    //                     BodyEquip = body;
    //                     BodyEquip.ListViews = listOfGameObjectsViews;
    //                 }
    //
    //                 break;
    //             }
    //             case CloakEquip cloak:
    //             {
    //                 if (!CloakEquip && _personCharacter.PutOnArmor(cloak, ref listOfGameObjectsViews))
    //                 {
    //                     AddCharacteristics(cloak);
    //                     CloakEquip = cloak;
    //                     CloakEquip.ListViews = listOfGameObjectsViews;
    //                 }
    //
    //                 break;
    //             }
    //             case BraceletEquip bracelet:
    //             {
    //                 if (!BraceletEquip && _personCharacter.PutOnArmor(bracelet, ref listOfGameObjectsViews))
    //                 {
    //                     AddCharacteristics(bracelet);
    //                     BraceletEquip = bracelet;
    //                     BraceletEquip.ListViews = listOfGameObjectsViews;
    //                 }
    //
    //                 break;
    //             }
    //             case GlovesEquip gloves:
    //             {
    //                 if (!GlovesEquip && _personCharacter.PutOnArmor(gloves, ref listOfGameObjectsViews))
    //                 {
    //                     AddCharacteristics(gloves);
    //                     GlovesEquip = gloves;
    //                     GlovesEquip.ListViews = listOfGameObjectsViews;
    //                 }
    //
    //                 break;
    //             }
    //             case BeltEquip belt:
    //             {
    //                 if (!BeltEquip && _personCharacter.PutOnArmor(belt, ref listOfGameObjectsViews))
    //                 {
    //                     AddCharacteristics(belt);
    //                     BeltEquip = belt;
    //                     BeltEquip.ListViews = listOfGameObjectsViews;
    //                 }
    //
    //                 break;
    //             }
    //             case PantsEquip pants:
    //             {
    //                 if (!PantsEquip && _personCharacter.PutOnArmor(pants, ref listOfGameObjectsViews))
    //                 {
    //                     AddCharacteristics(pants);
    //                     PantsEquip = pants;
    //                     PantsEquip.ListViews = listOfGameObjectsViews;
    //                 }
    //
    //                 break;
    //             }
    //             case ShoesEquip shoes:
    //             {
    //                 if (!ShoesEquip && _personCharacter.PutOnArmor(shoes, ref listOfGameObjectsViews))
    //                 {
    //                     AddCharacteristics(shoes);
    //                     ShoesEquip = shoes;
    //                     ShoesEquip.ListViews = listOfGameObjectsViews;
    //                 }
    //
    //                 break;
    //             }
    //             case RingEquip ring:
    //             {
    //                 if (!Ring1Equip)
    //                 {
    //                     AddCharacteristics(ring);
    //                     Ring1Equip = ring;
    //                 }
    //
    //                 if (!Ring2Equip)
    //                 {
    //                     AddCharacteristics(ring);
    //                     Ring2Equip = ring;
    //                 }
    //
    //                 break;
    //             }
    //             case EarringEquip earring:
    //             {
    //                 if (!Earring1Equip)
    //                 {
    //                     AddCharacteristics(earring);
    //                     Earring1Equip = earring;
    //                 }
    //
    //                 if (!Earring2Equip)
    //                 {
    //                     AddCharacteristics(earring);
    //                     Earring2Equip = earring;
    //                 }
    //
    //                 break;
    //             }
    //         }
    //     }
    //
    //
    //     private bool CheckPermissionForEquipment(BaseArmorItem item)
    //     {
    //         var result = false;
    //         
    //         if (_characterEquipment.permissionForEquipment.Shield && item.SubItemType == (int)ArmorItemType.Shield)
    //             result = true;
    //
    //         if (_characterEquipment.permissionForEquipment.HeavyArmor && item.HvMdLt == HeavyLightMedium.Heavy)
    //             result = true;
    //         if (_characterEquipment.permissionForEquipment.MediumArmor && item.HvMdLt == HeavyLightMedium.Medium)
    //             result = true;
    //         if (_characterEquipment.permissionForEquipment.LightArmor && item.HvMdLt == HeavyLightMedium.Light)
    //             result = true;
    //
    //         return result;
    //     }
    //
    //     private void PutOnWeapon(BaseEquipItem item)
    //     {
    //         AddCharacteristics(item);
    //         var weapon = item as BaseWeapon;
    //         switch (weapon.WeaponType)
    //         {
    //             case WeaponItemType.OneHandWeapon:
    //                 if (_characterEquipment.permissionForEquipment.OneHandWeapon)
    //                 {
    //                     if (!MainWeapon)
    //                         SetWeapon(weapon.Inst(), out MainWeapon,
    //                             _equipmentPoints._rightWeaponAttachPoint, ActiveWeapons.RightHand);
    //                     else if (!SecondWeapon && _characterEquipment.permissionForEquipment.TwoOneHandWeapon)
    //                         SetWeapon(weapon.Inst(), out SecondWeapon,
    //                             _equipmentPoints._leftWeaponAttachPoint, ActiveWeapons.RightAndLeft);
    //                 }
    //
    //                 break;
    //             case WeaponItemType.TwoHandSwordWeapon:
    //                 if (_characterEquipment.permissionForEquipment.TwoHandSwordWeapon)
    //                     SetWeapon(weapon.Inst(), out MainWeapon,
    //                         _equipmentPoints._rightWeaponAttachPoint, ActiveWeapons.TwoHand);
    //
    //                 break;
    //             case WeaponItemType.TwoHandSpearWeapon:
    //                 if (_characterEquipment.permissionForEquipment.TwoHandSpearWeapon)
    //                     SetWeapon(weapon.Inst(), out MainWeapon,
    //                         _equipmentPoints._rightWeaponAttachPoint, ActiveWeapons.TwoHand);
    //
    //                 break;
    //             case WeaponItemType.TwoHandStaffWeapon:
    //                 if (_characterEquipment.permissionForEquipment.TwoHandStaffWeapon)
    //                     SetWeapon(weapon.Inst(), out MainWeapon,
    //                         _equipmentPoints._rightWeaponAttachPoint, ActiveWeapons.TwoHand);
    //
    //                 break;
    //             case WeaponItemType.RangeTwoHandBowWeapon:
    //                 if (_characterEquipment.permissionForEquipment.RangeTwoHandBowWeapon)
    //                     SetWeapon(weapon.Inst(), out MainWeapon,
    //                         _equipmentPoints._leftWeaponAttachPoint, ActiveWeapons.TwoHand);
    //
    //                 break;
    //             case WeaponItemType.RangeTwoHandCrossbowWeapon:
    //                 if (_characterEquipment.permissionForEquipment.RangeTwoHandCrossbowWeapon)
    //                     SetWeapon(weapon.Inst(), out MainWeapon,
    //                         _equipmentPoints._rightWeaponAttachPoint, ActiveWeapons.TwoHand);
    //
    //                 break;
    //         }
    //     }
    //
    //     private void AddCharacteristics(BaseEquipItem item)
    //     {
    //         _fullStrength += item.Characteristics.Strength;
    //         _fullAgility += item.Characteristics.Agility;
    //         _fullIntellect += item.Characteristics.Intellect;
    //         _fullSpirit += item.Characteristics.Spirit;
    //         _fullStamina += item.Characteristics.Stamina;
    //         if(item is BaseArmorItem armor)
    //             _listEquipmentItems.Add(armor);
    //     }
    //
    //     private void ClearAllSlots()
    //     {
    //         foreach (var view in _listEquipmentItems.SelectMany(equipItem => equipItem.ListViews))
    //         {
    //             view.SetActive(false);
    //         }
    //         _listEquipmentItems.Clear();
    //         if (MainWeapon)
    //         {
    //             Object.Destroy(MainWeapon);
    //             MainWeapon = null;
    //         }
    //
    //         if (SecondWeapon)
    //         {
    //             Object.Destroy(SecondWeapon);
    //             SecondWeapon = null;
    //         }
    //
    //         var children
    //             = (from Transform child in _equipmentPoints._rightWeaponAttachPoint select child.gameObject).ToList();
    //         children.ForEach(Object.Destroy);
    //
    //         children =
    //             (from Transform child in _equipmentPoints._leftWeaponAttachPoint select child.gameObject).ToList();
    //         children.ForEach(Object.Destroy);
    //
    //         children = (from Transform child in _equipmentPoints._leftShildAttachPoint select child.gameObject)
    //             .ToList();
    //         children.ForEach(Object.Destroy);
    //         children = null;
    //
    //     }
    //
    //     
    //
    //     public void SetEquipment(CharacterEquipment equipment)
    //     {
    //         _characterEquipment = equipment;
    //         foreach (var item in equipment.Equipment)
    //         {
    //             Dbg.Log($"NewEquipment.Item :{item.name}");
    //         }
    //     }
    //
    //     public int GetWeaponType()
    //     {
    //         if (MainWeapon == null)
    //             return 0; //Unarmed
    //
    //         switch (MainWeapon.WeaponType)
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
    //         if (MainWeapon.WeaponType == WeaponItemType.OneHandWeapon &&
    //             SecondWeapon.WeaponType == WeaponItemType.OneHandWeapon)
    //             return 5; //DualWeapons
    //         return 0;
    //     }
    //     
    //      private void SetWeapon(BaseWeapon weapon, out BaseWeapon currentWeapon, Transform attachPoint,
    //         ActiveWeapons activeWeapons)
    //     {
    //         currentWeapon = weapon;
    //         currentWeapon.transform.SetParent(attachPoint, false);
    //         ActiveWeapons = activeWeapons;
    //     }
    //     
    //     private void SentToInventory(BaseEquipItem item)
    //     {
    //         _characterEquipment.Equipment.Remove(item);
    //         _characterEquipment.Inventory.Add(item);
    //     }
    // }
}