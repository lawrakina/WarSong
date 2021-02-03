using System;
using System.Collections.Generic;
using Data;
using Enums;
using Extension;
using Interface;
using UniRx;
using UnityEngine;


namespace Unit.Player
{
    public sealed class CustomizingCharacter : IController, ICleanup
    {
        #region Fields

        private readonly CompositeDisposable _subscriptions;
        private IPlayerView _player;
        private readonly PrototypePlayerModel _prototype;
        private PlayerData _playerData;
        private CharacterData _characterData;

        #endregion
        
        
        public CustomizingCharacter(PlayerData playerData, IPlayerView player)
        {
            _subscriptions = new CompositeDisposable();
            _player = player;
            _playerData = playerData;

            _prototype.CharacterClass.Subscribe(charClass =>
            {
                Dbg.Log($"_prototype.CharacterClass:{charClass}");
                switch (charClass)
                {
                    case CharacterClass.Warrior:
                        break;

                    case CharacterClass.Rogue:
                        break;

                    case CharacterClass.Hunter:
                        break;

                    case CharacterClass.Mage:
                        break;

                    default:
                        throw new ArgumentOutOfRangeException(nameof(charClass), charClass, null);
                }
            }).AddTo(_subscriptions);

            // ClearLists();
            // rebuild all lists
            BuildLists();

            // disable any enabled objects before clear
            // if (_playerData.enabledObjects.Count != 0)
            // {
            //     foreach (GameObject g in _playerData.enabledObjects)
            //     {
            //         g.SetActive(false);
            //     }
            // }
            //
            // // clear enabled objects list
            // _playerData.enabledObjects.Clear();
            //
            // // set default male character
            // ActivateItem(_playerData.male.headAllElements[0]);
            // ActivateItem(_playerData.male.eyebrow[0]);
            // ActivateItem(_playerData.male.facialHair[0]);
            // ActivateItem(_playerData.male.torso[0]);
            // ActivateItem(_playerData.male.arm_Upper_Right[0]);
            // ActivateItem(_playerData.male.arm_Upper_Left[0]);
            // ActivateItem(_playerData.male.arm_Lower_Right[0]);
            // ActivateItem(_playerData.male.arm_Lower_Left[0]);
            // ActivateItem(_playerData.male.hand_Right[0]);
            // ActivateItem(_playerData.male.hand_Left[0]);
            // ActivateItem(_playerData.male.hips[0]);
            // ActivateItem(_playerData.male.leg_Right[0]);
            // ActivateItem(_playerData.male.leg_Left[0]);
        }

        public CustomizingCharacter(CharacterData characterData)
        {
            _characterData = characterData;
        }

        public void Cleanup()
        {
            _subscriptions?.Dispose();
        }


        public void On()
        {
        }

        public void Off()
        {
        }


        private void BuildLists()
        {
            //build out male lists
            // BuildList(_playerData.male.headAllElements, "Male_Head_All_Elements");
            // BuildList(_playerData.male.headNoElements, "Male_Head_No_Elements");
            // BuildList(_playerData.male.eyebrow, "Male_01_Eyebrows");
            // BuildList(_playerData.male.facialHair, "Male_02_FacialHair");
            // BuildList(_playerData.male.torso, "Male_03_Torso");
            // BuildList(_playerData.male.arm_Upper_Right, "Male_04_Arm_Upper_Right");
            // BuildList(_playerData.male.arm_Upper_Left, "Male_05_Arm_Upper_Left");
            // BuildList(_playerData.male.arm_Lower_Right, "Male_06_Arm_Lower_Right");
            // BuildList(_playerData.male.arm_Lower_Left, "Male_07_Arm_Lower_Left");
            // BuildList(_playerData.male.hand_Right, "Male_08_Hand_Right");
            // BuildList(_playerData.male.hand_Left, "Male_09_Hand_Left");
            // BuildList(_playerData.male.hips, "Male_10_Hips");
            // BuildList(_playerData.male.leg_Right, "Male_11_Leg_Right");
            // BuildList(_playerData.male.leg_Left, "Male_12_Leg_Left");
            //
            // //build out female lists
            // BuildList(_playerData.female.headAllElements, "Female_Head_All_Elements");
            // BuildList(_playerData.female.headNoElements, "Female_Head_No_Elements");
            // BuildList(_playerData.female.eyebrow, "Female_01_Eyebrows");
            // BuildList(_playerData.female.facialHair, "Female_02_FacialHair");
            // BuildList(_playerData.female.torso, "Female_03_Torso");
            // BuildList(_playerData.female.arm_Upper_Right, "Female_04_Arm_Upper_Right");
            // BuildList(_playerData.female.arm_Upper_Left, "Female_05_Arm_Upper_Left");
            // BuildList(_playerData.female.arm_Lower_Right, "Female_06_Arm_Lower_Right");
            // BuildList(_playerData.female.arm_Lower_Left, "Female_07_Arm_Lower_Left");
            // BuildList(_playerData.female.hand_Right, "Female_08_Hand_Right");
            // BuildList(_playerData.female.hand_Left, "Female_09_Hand_Left");
            // BuildList(_playerData.female.hips, "Female_10_Hips");
            // BuildList(_playerData.female.leg_Right, "Female_11_Leg_Right");
            // BuildList(_playerData.female.leg_Left, "Female_12_Leg_Left");
            //
            // // build out all gender lists
            // BuildList(_playerData.allGender.all_Hair, "All_01_Hair");
            // BuildList(_playerData.allGender.all_Head_Attachment, "All_02_Head_Attachment");
            // BuildList(_playerData.allGender.headCoverings_Base_Hair, "HeadCoverings_Base_Hair");
            // BuildList(_playerData.allGender.headCoverings_No_FacialHair, "HeadCoverings_No_FacialHair");
            // BuildList(_playerData.allGender.headCoverings_No_Hair, "HeadCoverings_No_Hair");
            // BuildList(_playerData.allGender.chest_Attachment, "All_03_Chest_Attachment");
            // BuildList(_playerData.allGender.back_Attachment, "All_04_Back_Attachment");
            // BuildList(_playerData.allGender.shoulder_Attachment_Right, "All_05_Shoulder_Attachment_Right");
            // BuildList(_playerData.allGender.shoulder_Attachment_Left, "All_06_Shoulder_Attachment_Left");
            // BuildList(_playerData.allGender.elbow_Attachment_Right, "All_07_Elbow_Attachment_Right");
            // BuildList(_playerData.allGender.elbow_Attachment_Left, "All_08_Elbow_Attachment_Left");
            // BuildList(_playerData.allGender.hips_Attachment, "All_09_Hips_Attachment");
            // BuildList(_playerData.allGender.knee_Attachement_Right, "All_10_Knee_Attachement_Right");
            // BuildList(_playerData.allGender.knee_Attachement_Left, "All_11_Knee_Attachement_Left");
            // BuildList(_playerData.allGender.elf_Ear, "Elf_Ear");
        }

        // private void ClearLists()
        // {
        //     //build out male lists
        //     ClearList(_playerData.male.headAllElements, "Male_Head_All_Elements");
        //     ClearList(_playerData.male.headNoElements, "Male_Head_No_Elements");
        //     ClearList(_playerData.male.eyebrow, "Male_01_Eyebrows");
        //     ClearList(_playerData.male.facialHair, "Male_02_FacialHair");
        //     ClearList(_playerData.male.torso, "Male_03_Torso");
        //     ClearList(_playerData.male.arm_Upper_Right, "Male_04_Arm_Upper_Right");
        //     ClearList(_playerData.male.arm_Upper_Left, "Male_05_Arm_Upper_Left");
        //     ClearList(_playerData.male.arm_Lower_Right, "Male_06_Arm_Lower_Right");
        //     ClearList(_playerData.male.arm_Lower_Left, "Male_07_Arm_Lower_Left");
        //     ClearList(_playerData.male.hand_Right, "Male_08_Hand_Right");
        //     ClearList(_playerData.male.hand_Left, "Male_09_Hand_Left");
        //     ClearList(_playerData.male.hips, "Male_10_Hips");
        //     ClearList(_playerData.male.leg_Right, "Male_11_Leg_Right");
        //     ClearList(_playerData.male.leg_Left, "Male_12_Leg_Left");
        //
        //     //ClearList out female lists
        //     ClearList(_playerData.female.headAllElements, "Female_Head_All_Elements");
        //     ClearList(_playerData.female.headNoElements, "Female_Head_No_Elements");
        //     ClearList(_playerData.female.eyebrow, "Female_01_Eyebrows");
        //     ClearList(_playerData.female.facialHair, "Female_02_FacialHair");
        //     ClearList(_playerData.female.torso, "Female_03_Torso");
        //     ClearList(_playerData.female.arm_Upper_Right, "Female_04_Arm_Upper_Right");
        //     ClearList(_playerData.female.arm_Upper_Left, "Female_05_Arm_Upper_Left");
        //     ClearList(_playerData.female.arm_Lower_Right, "Female_06_Arm_Lower_Right");
        //     ClearList(_playerData.female.arm_Lower_Left, "Female_07_Arm_Lower_Left");
        //     ClearList(_playerData.female.hand_Right, "Female_08_Hand_Right");
        //     ClearList(_playerData.female.hand_Left, "Female_09_Hand_Left");
        //     ClearList(_playerData.female.hips, "Female_10_Hips");
        //     ClearList(_playerData.female.leg_Right, "Female_11_Leg_Right");
        //     ClearList(_playerData.female.leg_Left, "Female_12_Leg_Left");
        //
        //     // build out all gender lists
        //     ClearList(_playerData.allGender.all_Hair, "All_01_Hair");
        //     ClearList(_playerData.allGender.all_Head_Attachment, "All_02_Head_Attachment");
        //     ClearList(_playerData.allGender.headCoverings_Base_Hair, "HeadCoverings_Base_Hair");
        //     ClearList(_playerData.allGender.headCoverings_No_FacialHair, "HeadCoverings_No_FacialHair");
        //     ClearList(_playerData.allGender.headCoverings_No_Hair, "HeadCoverings_No_Hair");
        //     ClearList(_playerData.allGender.chest_Attachment, "All_03_Chest_Attachment");
        //     ClearList(_playerData.allGender.back_Attachment, "All_04_Back_Attachment");
        //     ClearList(_playerData.allGender.shoulder_Attachment_Right, "All_05_Shoulder_Attachment_Right");
        //     ClearList(_playerData.allGender.shoulder_Attachment_Left, "All_06_Shoulder_Attachment_Left");
        //     ClearList(_playerData.allGender.elbow_Attachment_Right, "All_07_Elbow_Attachment_Right");
        //     ClearList(_playerData.allGender.elbow_Attachment_Left, "All_08_Elbow_Attachment_Left");
        //     ClearList(_playerData.allGender.hips_Attachment, "All_09_Hips_Attachment");
        //     ClearList(_playerData.allGender.knee_Attachement_Right, "All_10_Knee_Attachement_Right");
        //     ClearList(_playerData.allGender.knee_Attachement_Left, "All_11_Knee_Attachement_Left");
        //     ClearList(_playerData.allGender.elf_Ear, "Elf_Ear");
        // }

        // called from the BuildLists method
        private void BuildList(List<GameObject> targetList, string characterPart)
        {
            // Transform[] rootTransform = _playerData.StoragePlayerPrefab.gameObject.GetComponentsInChildren<Transform>();
            //
            // // declare target root transform
            // Transform targetRoot = null;
            //
            // // find character parts parent object in the scene
            // foreach (Transform t in rootTransform)
            // {
            //     if (t.gameObject.name == characterPart)
            //     {
            //         targetRoot = t;
            //         break;
            //     }
            // }
            //
            // // clears targeted list of all objects
            // targetList.Clear();
            //
            // // cycle through all child objects of the parent object
            // for (int i = 0; i < targetRoot.childCount; i++)
            // {
            //     // get child gameobject index i
            //     GameObject go = targetRoot.GetChild(i).gameObject;
            //
            //     // disable child object
            //     go.SetActive(false);
            //
            //     // add object to the targeted object list
            //     targetList.Add(go);
            //
            //     // collect the material for the random character, only if null in the inspector;
            //     if (!_playerData.mat)
            //     {
            //         if (go.GetComponent<SkinnedMeshRenderer>())
            //             _playerData.mat = go.GetComponent<SkinnedMeshRenderer>().material;
            //     }
            // }
        }

        private void ClearList(List<GameObject> targetList, string characterPart)
        {
            if (targetList.Count == 0) return;
            targetList.Clear();
        }

        private void ActivateItem(GameObject go)
        {
            // enable item
            go.SetActive(true);

            // add item to the enabled items list
            // _playerData.enabledObjects.Add(go);
        }
    }
}