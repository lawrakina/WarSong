using System;
using System.Collections.Generic;
using System.Linq;
using Code.CharacterCustomizing;
using Code.Equipment;
using Code.Unit;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Code.Data.Unit
{
    public class UnitPerson
    {
        private readonly GameObject _root;
        private readonly CharacterSettings _settings;
        private readonly Material _rootMaterial;
        private readonly CharacterData _data;
        private readonly RaceCharacteristics _raceCharacteristics;
        private readonly EquipmentPoints _equipmentPoints;

        private List<GameObject> _enabledObjects = new List<GameObject>();
        private CharacterObjectGroups _maleObjectGroups = new CharacterObjectGroups();
        private CharacterObjectGroups _femaleObjectGroups = new CharacterObjectGroups();
        private CharacterObjectListsAllGender _allGenderObjectGroups = new CharacterObjectListsAllGender();

        public UnitPerson(GameObject root, CharacterSettings settings, CharacterData data,
            RaceCharacteristics raceCharacteristics)
        {
            _root = root;
            _settings = settings;
            _data = data;
            _raceCharacteristics = raceCharacteristics;
            _rootMaterial = new Material(_data.material);

            _equipmentPoints = new EquipmentPoints(_root, _data);
            _equipmentPoints.GenerateAllPoints();

            BuildLists();
            ConstructClearPerson();
        }

        #region BuildLists

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
            var rootTransform = _root.GetComponentsInChildren<Transform>();
            var targetRoot = rootTransform.FirstOrDefault(item => item.gameObject.name == characterPart);

            targetList.Clear();
            for (var i = 0; i < targetRoot.childCount; i++)
            {
                var item = targetRoot.GetChild(i).gameObject;
                item.SetActive(false);
                targetList.Add(item);
                if (item.TryGetComponent(out SkinnedMeshRenderer skin))
                {
                    skin.material = _rootMaterial;
                }
            }
        }

        #endregion

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

            _rootMaterial.SetColor("_Color_Skin", _data.GetColorSkin(_settings.CharacterRace, _settings.CharacterSkin));
            _rootMaterial.SetColor("_Color_Hair", _data.GetColorHair(_settings.CharacterHair));
            _rootMaterial.SetColor("_Color_Stubble", _data.GetColorStubble(_settings.CharacterStubble));
            _rootMaterial.SetColor("_Color_Scar", _data.GetColorScar(_settings.CharacterScar));

            _root.transform.localScale = _raceCharacteristics.ScaleModelByRace;
        }

        private void ActivateItem(GameObject gameObject)
        {
            gameObject.SetActive(true);
            _enabledObjects.Add(gameObject);
        }

        public void PutOn(BaseEquipItem equipItem, EquipCellType targetIsMainOrSecondHand)
        {
            //Если объект внешний то инстанцируем его в AttachPoint
            if (equipItem.IsNeedInstantiate)
            {
                if (equipItem.EquipType == EquipItemType.Weapon)
                {
                    var weapon = equipItem as WeaponEquipItem;
                    switch (weapon.WeaponType)
                    {
                        case WeaponItemType.OneHandWeapon:
                            if (targetIsMainOrSecondHand == EquipCellType.MainHand)
                                Object.Instantiate(weapon.GameObject, _equipmentPoints._rightWeaponAttachPoint);
                            if (targetIsMainOrSecondHand == EquipCellType.SecondHand)
                                Object.Instantiate(weapon.GameObject, _equipmentPoints._leftWeaponAttachPoint);
                            break;
                        case WeaponItemType.TwoHandSwordWeapon:
                            Object.Instantiate(weapon.GameObject, _equipmentPoints._rightWeaponAttachPoint);
                            break;
                        case WeaponItemType.TwoHandSpearWeapon:
                            Object.Instantiate(weapon.GameObject, _equipmentPoints._rightWeaponAttachPoint);
                            break;
                        case WeaponItemType.TwoHandStaffWeapon:
                            Object.Instantiate(weapon.GameObject, _equipmentPoints._rightWeaponAttachPoint);
                            break;
                        case WeaponItemType.RangeTwoHandBowWeapon:
                            Object.Instantiate(weapon.GameObject, _equipmentPoints._leftWeaponAttachPoint);
                            break;
                        case WeaponItemType.RangeTwoHandCrossbowWeapon:
                            Object.Instantiate(weapon.GameObject, _equipmentPoints._leftWeaponAttachPoint);
                            break;
                    }
                }

                if (equipItem.EquipType == EquipItemType.Armor)
                {
                    var armor = equipItem as ArmorEquipItem;
                    if (armor.TargetEquipCell == TargetEquipCell.Shield)
                    {
                        Object.Instantiate(armor.GameObject, _equipmentPoints._leftShildAttachPoint);
                    }
                    ////////////////////////////////////////////////
                    //тут можно лепить на модель внешние объекты///
                    //////////////////////////////////////////////
                }
                
            }
            else // если объект внутренний то ищем по имени и активируем
            {
                foreach (var view in equipItem.Views)
                {
                    var name = view.name.Replace("_Static", "");
                    name = name.Replace(" ", "");
                    name = name.Replace("Variant", "");
                    SearchAndActiveViewInHierarchy(name);
                }
            }
        }

        private void SearchAndActiveViewInHierarchy(string name)
        {
            var list = _femaleObjectGroups.SearchByName(name).ToList();
            list.AddRange(_maleObjectGroups.SearchByName(name));
            list.AddRange(_allGenderObjectGroups.SearchByName(name));
            foreach (var item in list)
            {
                ActivateItem(item);
            }
        }

        public void RemoveHiddenItems()
        {
            var listForRemove = _femaleObjectGroups.ListByActive(false).ToList();
            listForRemove.AddRange(_maleObjectGroups.ListByActive(false));
            listForRemove.AddRange(_allGenderObjectGroups.ListByActive(false));

            foreach (var item in listForRemove)
            {
                Object.Destroy(item);
            }
        }
    }
}