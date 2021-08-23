using System;
using System.Collections.Generic;
using System.Linq;
using Code.Data;
using Code.Data.Unit;
using Code.Equipment;
using Code.Extension;
using UnityEngine;
using CharacterObjectGroups = Code.CharacterCustomizing.CharacterObjectGroups;
using CharacterObjectListsAllGender = Code.CharacterCustomizing.CharacterObjectListsAllGender;
using Object = UnityEngine.Object;


namespace Code.Unit.Factories
{
    public sealed class UnitPerson
    {
        private readonly GameObject _gameObject;
        private CharacterSettings _settings;
        private CharacterData _data;
        private EquipmentPoints _equipmentPoints;
        private Material _material;

        public BaseWeapon MainWeapon = null;
        public BaseWeapon SecondWeapon = null;
        public ShieldEquip ShieldEquip = null;
        public ActiveWeapons ActiveWeapons;

        private List<GameObject> _enabledObjects = new List<GameObject>();
        private CharacterObjectGroups _maleObjectGroups = new CharacterObjectGroups();
        private CharacterObjectGroups _femaleObjectGroups = new CharacterObjectGroups();
        private CharacterObjectListsAllGender _allGenderObjectGroups = new CharacterObjectListsAllGender();

        List<BaseEquipItem> _listEquipmentItems = new List<BaseEquipItem>();

        public BaseArmorItem HeadEquip = null;
        public BaseArmorItem NeckEquip = null;
        public BaseArmorItem ShoulderEquip = null;
        public BaseArmorItem BodyEquip = null;
        public BaseArmorItem CloakEquip = null;
        public BaseArmorItem BraceletEquip = null;
        public BaseArmorItem GlovesEquip = null;
        public BaseArmorItem BeltEquip = null;
        public BaseArmorItem PantsEquip = null;
        public BaseArmorItem ShoesEquip = null;
        public BaseArmorItem Ring1Equip = null;
        public BaseArmorItem Ring2Equip = null;
        public BaseArmorItem Earring1Equip = null;
        public BaseArmorItem Earring2Equip = null;

        public EquipmentPoints EquipmentPoints => _equipmentPoints;
        public List<BaseEquipItem> ListEquipmentItems => _listEquipmentItems;


        public UnitPerson(GameObject gameObject, CharacterSettings settings, CharacterData data)
        {
            _gameObject = gameObject;
            _settings = settings;
            _data = data;
            _material = _data.material;

            _equipmentPoints = new EquipmentPoints(_gameObject, _data);
            _equipmentPoints.GenerateAllPoints();

            BuildLists();
            ConstructClearPerson();
        }

        public void Generate(CharacterSettings settings)
        {
            _settings = settings;
            ClearEnabledObjects();
            ConstructClearPerson();
            ConstructEquipPerson();
        }

        private void ConstructEquipPerson()
        {
            var listForSendToInventory = new List<BaseEquipItem>();
            foreach (var item in _settings.Equipment.Equipment)
            {
                var ifEquipItem = false;
                switch (item.ItemType)
                {
                    case InventoryItemType.Weapon:
                        ifEquipItem = PutOnWeapon(item);
                        break;
                    case InventoryItemType.Armor:
                        ifEquipItem = PutOnArmor(item as BaseArmorItem);
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
                    listForSendToInventory.Add(item);
                }
            }

            foreach (var item in listForSendToInventory)
            {
                SentToInventory(item);
            }
        }

        private bool PutOnArmor(BaseArmorItem item)
        {
            Dbg.Log($"PuOnArmor:{item.NameInHierarchy}");
            if (!item)
                return false;
            if (!CheckPermissionForEquipment(item))
            {
                return false;
            }

            switch (item)
            {
                case ShieldEquip shield:
                {
                    if (ActiveWeapons == ActiveWeapons.RightHand)
                    {
                        ShieldEquip = Object.Instantiate(shield.GameObject, Vector3.zero, Quaternion.identity)
                            .GetComponent<ShieldEquip>();
                        ShieldEquip.transform.SetParent(_equipmentPoints._leftShildAttachPoint, false);
                        ActiveWeapons = ActiveWeapons.RightAndShield;

                        _listEquipmentItems.Add(shield);
                        return true;
                    }

                    break;
                }
                case HeadEquip head:
                {
                    return Put(ref HeadEquip, head);
                    break;
                }
                case NeckEquip neck:
                {
                    return Put(ref NeckEquip, neck);
                    break;
                }
                case ShoulderEquip shoulder:
                {
                    return Put(ref ShoulderEquip, shoulder);
                    break;
                }
                case BodyEquip body:
                {
                    return Put(ref BodyEquip, body);
                    break;
                }
                case CloakEquip cloak:
                {
                    return Put(ref CloakEquip, cloak);
                    break;
                }
                case BraceletEquip bracelet:
                {
                    return Put(ref BraceletEquip, bracelet);
                    break;
                }
                case GlovesEquip gloves:
                {
                    return Put(ref GlovesEquip, gloves);
                    break;
                }
                case BeltEquip belt:
                {
                    return Put(ref BeltEquip, belt);
                    break;
                }
                case PantsEquip pants:
                {
                    return Put(ref PantsEquip, pants);
                    break;
                }
                case ShoesEquip shoes:
                {
                    return Put(ref ShoesEquip, shoes);
                    break;
                }
                case RingEquip ring:
                {
                    if (!Ring1Equip)
                    {
                        return Put(ref Ring1Equip, ring);
                    }

                    if (!Ring2Equip)
                    {
                        return Put(ref Ring2Equip, ring);
                    }

                    break;
                }
                case EarringEquip earring:
                {
                    if (!Earring1Equip)
                    {
                        return Put(ref Earring1Equip, earring);
                    }

                    if (!Earring2Equip)
                    {
                        return Put(ref Earring2Equip, earring);
                    }

                    break;
                }
            }

            return false;
        }

        private bool Put(ref BaseArmorItem target, BaseArmorItem source)
        {
            Dbg.Log($"Put new Armor. Target = {target}; Source = {source};");
            if (target)
            {
                return false;
            }

            target = source;
            if (target.NameInHierarchy != string.Empty)
            {
                SearchViewsOnBaseArmor(target);
                ActivateItem(target.ListViews);
            }

            _listEquipmentItems.Add(target);
            return true;
        }

        private void SearchViewsOnBaseArmor(BaseArmorItem armor)
        {
            var list = _femaleObjectGroups.SearchByName(armor.NameInHierarchy).ToList();
            list.AddRange(_maleObjectGroups.SearchByName(armor.NameInHierarchy));
            list.AddRange(_allGenderObjectGroups.SearchByName(armor.NameInHierarchy));
            armor.ListViews = list;
        }

        private bool CheckPermissionForEquipment(BaseArmorItem item)
        {
            var result = false;

            if (_settings.Equipment.permissionForEquipment.Shield && item.ArmorItemType == ArmorItemType.Shield)
                result = true;

            if (_settings.Equipment.permissionForEquipment.HeavyArmor && item.HvMdLt == HeavyLightMedium.Heavy)
                result = true;
            if (_settings.Equipment.permissionForEquipment.MediumArmor && item.HvMdLt == HeavyLightMedium.Medium)
                result = true;
            if (_settings.Equipment.permissionForEquipment.LightArmor && item.HvMdLt == HeavyLightMedium.Light)
                result = true;

            return result;
        }

        private bool PutOnWeapon(BaseEquipItem item)
        {
            var weapon = item as BaseWeapon;
            switch (weapon.WeaponType)
            {
                case WeaponItemType.OneHandWeapon:
                    if (_settings.Equipment.permissionForEquipment.OneHandWeapon)
                    {
                        if (!MainWeapon)
                        {
                            SetWeapon(weapon.Inst(), out MainWeapon,
                                _equipmentPoints._rightWeaponAttachPoint, ActiveWeapons.RightHand);
                            return true;
                        }

                        if (!SecondWeapon && _settings.Equipment.permissionForEquipment.TwoOneHandWeapon)
                        {
                            SetWeapon(weapon.Inst(), out SecondWeapon,
                                _equipmentPoints._leftWeaponAttachPoint, ActiveWeapons.RightAndLeft);
                            return true;
                        }
                    }

                    break;
                case WeaponItemType.TwoHandSwordWeapon:
                    if (_settings.Equipment.permissionForEquipment.TwoHandSwordWeapon)
                    {
                        SetWeapon(weapon.Inst(), out MainWeapon,
                            _equipmentPoints._rightWeaponAttachPoint, ActiveWeapons.TwoHand);
                        return true;
                    }

                    break;
                case WeaponItemType.TwoHandSpearWeapon:
                    if (_settings.Equipment.permissionForEquipment.TwoHandSpearWeapon)
                    {
                        SetWeapon(weapon.Inst(), out MainWeapon,
                            _equipmentPoints._rightWeaponAttachPoint, ActiveWeapons.TwoHand);
                        return true;
                    }

                    break;
                case WeaponItemType.TwoHandStaffWeapon:
                    if (_settings.Equipment.permissionForEquipment.TwoHandStaffWeapon)
                    {
                        SetWeapon(weapon.Inst(), out MainWeapon,
                            _equipmentPoints._rightWeaponAttachPoint, ActiveWeapons.TwoHand);
                        return true;
                    }

                    break;
                case WeaponItemType.RangeTwoHandBowWeapon:
                    if (_settings.Equipment.permissionForEquipment.RangeTwoHandBowWeapon)
                    {
                        SetWeapon(weapon.Inst(), out MainWeapon,
                            _equipmentPoints._leftWeaponAttachPoint, ActiveWeapons.TwoHand);
                        return true;
                    }

                    break;
                case WeaponItemType.RangeTwoHandCrossbowWeapon:
                    if (_settings.Equipment.permissionForEquipment.RangeTwoHandCrossbowWeapon)
                    {
                        SetWeapon(weapon.Inst(), out MainWeapon,
                            _equipmentPoints._rightWeaponAttachPoint, ActiveWeapons.TwoHand);
                        return true;
                    }

                    break;
            }

            return false;
        }

        private void SetWeapon(BaseWeapon weapon, out BaseWeapon currentWeapon, Transform attachPoint,
            ActiveWeapons activeWeapons)
        {
            currentWeapon = weapon;
            currentWeapon.transform.SetParent(attachPoint, false);
            ActiveWeapons = activeWeapons;
            ActivateItem(currentWeapon.GameObject);
        }

        private void SentToInventory(BaseEquipItem item)
        {
            Dbg.Log($"------------------- SentToInventory:{item.name}");
            _settings.Equipment.Equipment.Remove(item);
            _settings.Equipment.Inventory.Add(item);
        }

        private void ConstructClearPerson()
        {
            switch (_settings.CharacterGender)
            {
                case CharacterGender.Male:
                    ActivateItem(_maleObjectGroups.headAllElements[0]);
                    ActivateItem(_maleObjectGroups.eyebrow[0]);
                    ActivateItem(_maleObjectGroups.facialHair[0]);
                    ActivateItem(_maleObjectGroups.torso[0]);
                    ActivateItem(_maleObjectGroups.arm_Upper_Right[0]);
                    ActivateItem(_maleObjectGroups.arm_Upper_Left[0]);
                    ActivateItem(_maleObjectGroups.arm_Lower_Right[0]);
                    ActivateItem(_maleObjectGroups.arm_Lower_Left[0]);
                    ActivateItem(_maleObjectGroups.hand_Right[0]);
                    ActivateItem(_maleObjectGroups.hand_Left[0]);
                    ActivateItem(_maleObjectGroups.hips[0]);
                    ActivateItem(_maleObjectGroups.leg_Right[0]);
                    ActivateItem(_maleObjectGroups.leg_Left[0]);
                    break;
                case CharacterGender.Female:
                    ActivateItem(_femaleObjectGroups.headAllElements[0]);
                    ActivateItem(_femaleObjectGroups.eyebrow[0]);
                    ActivateItem(_femaleObjectGroups.torso[0]);
                    ActivateItem(_femaleObjectGroups.arm_Upper_Right[0]);
                    ActivateItem(_femaleObjectGroups.arm_Upper_Left[0]);
                    ActivateItem(_femaleObjectGroups.arm_Lower_Right[0]);
                    ActivateItem(_femaleObjectGroups.arm_Lower_Left[0]);
                    ActivateItem(_femaleObjectGroups.hand_Right[0]);
                    ActivateItem(_femaleObjectGroups.hand_Left[0]);
                    ActivateItem(_femaleObjectGroups.hips[0]);
                    ActivateItem(_femaleObjectGroups.leg_Right[0]);
                    ActivateItem(_femaleObjectGroups.leg_Left[0]);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void ActivateItem(List<GameObject> list)
        {
            foreach (var item in list)
            {
                ActivateItem(item);
            }
        }

        private void ActivateItem(GameObject gameObject)
        {
            gameObject.SetActive(true);
            _enabledObjects.Add(gameObject);
        }

        private void BuildLists()
        {
            //build out male lists
            BuildList(_maleObjectGroups.headAllElements, "Male_Head_All_Elements");
            BuildList(_maleObjectGroups.headNoElements, "Male_Head_No_Elements");
            BuildList(_maleObjectGroups.eyebrow, "Male_01_Eyebrows");
            BuildList(_maleObjectGroups.facialHair, "Male_02_FacialHair");
            BuildList(_maleObjectGroups.torso, "Male_03_Torso");
            BuildList(_maleObjectGroups.arm_Upper_Right, "Male_04_Arm_Upper_Right");
            BuildList(_maleObjectGroups.arm_Upper_Left, "Male_05_Arm_Upper_Left");
            BuildList(_maleObjectGroups.arm_Lower_Right, "Male_06_Arm_Lower_Right");
            BuildList(_maleObjectGroups.arm_Lower_Left, "Male_07_Arm_Lower_Left");
            BuildList(_maleObjectGroups.hand_Right, "Male_08_Hand_Right");
            BuildList(_maleObjectGroups.hand_Left, "Male_09_Hand_Left");
            BuildList(_maleObjectGroups.hips, "Male_10_Hips");
            BuildList(_maleObjectGroups.leg_Right, "Male_11_Leg_Right");
            BuildList(_maleObjectGroups.leg_Left, "Male_12_Leg_Left");

            //build out female lists
            BuildList(_femaleObjectGroups.headAllElements, "Female_Head_All_Elements");
            BuildList(_femaleObjectGroups.headNoElements, "Female_Head_No_Elements");
            BuildList(_femaleObjectGroups.eyebrow, "Female_01_Eyebrows");
            BuildList(_femaleObjectGroups.facialHair, "Female_02_FacialHair");
            BuildList(_femaleObjectGroups.torso, "Female_03_Torso");
            BuildList(_femaleObjectGroups.arm_Upper_Right, "Female_04_Arm_Upper_Right");
            BuildList(_femaleObjectGroups.arm_Upper_Left, "Female_05_Arm_Upper_Left");
            BuildList(_femaleObjectGroups.arm_Lower_Right, "Female_06_Arm_Lower_Right");
            BuildList(_femaleObjectGroups.arm_Lower_Left, "Female_07_Arm_Lower_Left");
            BuildList(_femaleObjectGroups.hand_Right, "Female_08_Hand_Right");
            BuildList(_femaleObjectGroups.hand_Left, "Female_09_Hand_Left");
            BuildList(_femaleObjectGroups.hips, "Female_10_Hips");
            BuildList(_femaleObjectGroups.leg_Right, "Female_11_Leg_Right");
            BuildList(_femaleObjectGroups.leg_Left, "Female_12_Leg_Left");

            // build out all gender lists
            BuildList(_allGenderObjectGroups.all_Hair, "All_01_Hair");
            BuildList(_allGenderObjectGroups.all_Head_Attachment, "All_02_Head_Attachment");
            BuildList(_allGenderObjectGroups.headCoverings_Base_Hair, "HeadCoverings_Base_Hair");
            BuildList(_allGenderObjectGroups.headCoverings_No_FacialHair, "HeadCoverings_No_FacialHair");
            BuildList(_allGenderObjectGroups.headCoverings_No_Hair, "HeadCoverings_No_Hair");
            BuildList(_allGenderObjectGroups.chest_Attachment, "All_03_Chest_Attachment");
            BuildList(_allGenderObjectGroups.back_Attachment, "All_04_Back_Attachment");
            BuildList(_allGenderObjectGroups.shoulder_Attachment_Right, "All_05_Shoulder_Attachment_Right");
            BuildList(_allGenderObjectGroups.shoulder_Attachment_Left, "All_06_Shoulder_Attachment_Left");
            BuildList(_allGenderObjectGroups.elbow_Attachment_Right, "All_07_Elbow_Attachment_Right");
            BuildList(_allGenderObjectGroups.elbow_Attachment_Left, "All_08_Elbow_Attachment_Left");
            BuildList(_allGenderObjectGroups.hips_Attachment, "All_09_Hips_Attachment");
            BuildList(_allGenderObjectGroups.knee_Attachement_Right, "All_10_Knee_Attachement_Right");
            BuildList(_allGenderObjectGroups.knee_Attachement_Left, "All_11_Knee_Attachement_Left");
            BuildList(_allGenderObjectGroups.elf_Ear, "Elf_Ear");
        }

        private void BuildList(List<GameObject> targetList, string characterPart)
        {
            Transform[] rootTransform = _gameObject.GetComponentsInChildren<Transform>();
            // declare target root transform
            Transform targetRoot = null;
            // find character parts parent object in the scene
            foreach (Transform item in rootTransform)
            {
                if (item.gameObject.name == characterPart)
                {
                    targetRoot = item;
                    break;
                }
            }

            // clears targeted list of all objects
            targetList.Clear();
            // cycle through all child objects of the parent object
            for (int i = 0; i < targetRoot.childCount; i++)
            {
                // get child gameobject index i
                GameObject item = targetRoot.GetChild(i).gameObject;
                // disable child object
                item.SetActive(false);
                // add object to the targeted object list
                targetList.Add(item);
                if (item.TryGetComponent(out SkinnedMeshRenderer skin))
                {
                    skin.material = _material;
                }

                // collect the material for the random character, only if null in the inspector;
                // if (!_material)
                // {
                //     if (item.GetComponent<SkinnedMeshRenderer>())
                //         _material = item.GetComponent<SkinnedMeshRenderer>().material;
                // }
            }
        }

        private void ClearEnabledObjects()
        {
            if (_enabledObjects.Count != 0)
                foreach (GameObject item in _enabledObjects)
                    item.SetActive(false);

            _enabledObjects.Clear();

            if (MainWeapon)
            {
                Object.Destroy(MainWeapon.GameObject);
                MainWeapon = null;
            }

            if (SecondWeapon)
            {
                Object.Destroy(SecondWeapon.GameObject);
                SecondWeapon = null;
            }

            if (ShieldEquip)
            {
                Object.Destroy(ShieldEquip.GameObject);
                ShieldEquip = null;
            }
            if (HeadEquip)
            {
                HeadEquip = null;
            }

            if (NeckEquip)
            {
                NeckEquip = null;
            }

            if (ShoulderEquip)
            {
                ShoulderEquip = null;
            }

            if (BodyEquip)
            {
                BodyEquip = null;
            }

            if (CloakEquip)
            {
                CloakEquip = null;
            }

            if (BraceletEquip)
            {
                BraceletEquip = null;
            }

            if (GlovesEquip)
            {
                GlovesEquip = null;
            }

            if (BeltEquip)
            {
                BeltEquip = null;
            }

            if (PantsEquip)
            {
                PantsEquip = null;
            }

            if (ShoesEquip)
            {
                ShoesEquip = null;
            }

            if (Ring1Equip)
            {
                Ring1Equip = null;
            }

            if (Ring2Equip)
            {
                Ring2Equip = null;
            }

            if (Earring1Equip)
            {
                Earring1Equip = null;
            }

            if (Earring2Equip)
            {
                Earring2Equip = null;
            }
            _listEquipmentItems.Clear();
        }
    }
}