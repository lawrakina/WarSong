using System;
using System.Collections.Generic;
using Data;
using Enums;
using PsychoticLab;
using UnityEngine;
using CharacterObjectGroups = Data.CharacterObjectGroups;
using CharacterObjectListsAllGender = Data.CharacterObjectListsAllGender;
using Random = UnityEngine.Random;
using SkinColor = CharacterCustomizing.SkinColor;


namespace CharacterCustomizing
{
    [Serializable]
    public class PersonCharacter
    {
        private GameObject _player;
        private CharacterData _characterData;

        [Header("Material")]
        public Material _material;
    
        // list of enabed objects on character
        public List<GameObject> _enabledObjects = new List<GameObject>();

        // character object lists
        // male list
        public CharacterObjectGroups _maleObjectGroups = new CharacterObjectGroups();

        // female list
        public CharacterObjectGroups _femaleObjectGroups = new CharacterObjectGroups();

        // universal list
        public CharacterObjectListsAllGender _allGenderObjectGroups = new CharacterObjectListsAllGender();
        
        private SkinColor SkinColor{ get; set; }
        public CharacterRace CharacterRace { get; set; }
        public CharacterGender CharacterGender{ get; set; }
        [SerializeField] private Elements _elements = Elements.Yes;
        [SerializeField] private HeadCovering _headCovering = HeadCovering.HeadCoverings_No_FacialHair;
        [SerializeField] private FacialHair _facialHair = FacialHair.No;
        
        
        public PersonCharacter(GameObject player, CharacterData characterData)
        {
            _characterData = characterData;
            _player = player;
            
            BuildLists();

            if (_enabledObjects.Count != 0)
            {
                foreach (GameObject item in _enabledObjects)
                {
                    item.SetActive(false);
                }
            }

            _enabledObjects.Clear();

            // set default male character
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
        }
        
        public void Generate()
        {
            if (_enabledObjects.Count != 0)
                foreach (var item in _enabledObjects)
                    item.SetActive(false);
            _enabledObjects.Clear();

            switch (CharacterGender)
            {
                case CharacterGender.Male:
                    RandomizeByVariable(_maleObjectGroups, CharacterGender, _elements, CharacterRace, _facialHair, SkinColor, _headCovering);
                    break;

                case CharacterGender.Female:
                    RandomizeByVariable(_femaleObjectGroups, CharacterGender, _elements, CharacterRace, _facialHair, SkinColor, _headCovering);
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }
            
        }

        void RandomizeByVariable(CharacterObjectGroups cog, CharacterGender gender, Elements elements, CharacterRace race, FacialHair facialHair, SkinColor skinColor, HeadCovering headCovering)
        {
            // if facial elements are enabled
            switch (elements)
            {
                case Elements.Yes:
                    //select head with all elements
                    if (cog.headAllElements.Count != 0)
                        ActivateItem(cog.headAllElements[Random.Range(0, cog.headAllElements.Count)]);

                    //select eyebrows
                    if (cog.eyebrow.Count != 0)
                        ActivateItem(cog.eyebrow[Random.Range(0, cog.eyebrow.Count)]);

                    //select facial hair (conditional)
                    if (cog.facialHair.Count != 0 && facialHair == FacialHair.Yes && gender == CharacterGender.Male && headCovering != HeadCovering.HeadCoverings_No_FacialHair)
                        ActivateItem(cog.facialHair[Random.Range(0, cog.facialHair.Count)]);

                    // select hair attachment
                    switch (headCovering)
                    {
                        case HeadCovering.HeadCoverings_Base_Hair:
                            // set hair attachment to index 1
                            if (_allGenderObjectGroups.all_Hair.Count != 0)
                                ActivateItem(_allGenderObjectGroups.all_Hair[1]);
                            if (_allGenderObjectGroups.headCoverings_Base_Hair.Count != 0)
                                ActivateItem(_allGenderObjectGroups.headCoverings_Base_Hair[Random.Range(0, _allGenderObjectGroups.headCoverings_Base_Hair.Count)]);
                            break;
                        case HeadCovering.HeadCoverings_No_FacialHair:
                            // no facial hair attachment
                            if (_allGenderObjectGroups.all_Hair.Count != 0)
                                ActivateItem(_allGenderObjectGroups.all_Hair[Random.Range(0, _allGenderObjectGroups.all_Hair.Count)]);
                            if (_allGenderObjectGroups.headCoverings_No_FacialHair.Count != 0)
                                ActivateItem(_allGenderObjectGroups.headCoverings_No_FacialHair[Random.Range(0, _allGenderObjectGroups.headCoverings_No_FacialHair.Count)]);
                            break;
                        case HeadCovering.HeadCoverings_No_Hair:
                            // select hair attachment
                            if (_allGenderObjectGroups.headCoverings_No_Hair.Count != 0)
                                ActivateItem(_allGenderObjectGroups.all_Hair[Random.Range(0, _allGenderObjectGroups.all_Hair.Count)]);
                            // if not human
                            if (race != CharacterRace.Human)
                            {
                                // select elf ear attachment
                                if (_allGenderObjectGroups.elf_Ear.Count != 0)
                                    ActivateItem(_allGenderObjectGroups.elf_Ear[Random.Range(0, _allGenderObjectGroups.elf_Ear.Count)]);
                            }
                            break;
                    }
                    break;

                case Elements.No:
                    //select head with no elements
                    if (cog.headNoElements.Count != 0)
                        ActivateItem(cog.headNoElements[Random.Range(0, cog.headNoElements.Count)]);
                    break;
            }

            // select torso starting at index 1
            if (cog.torso.Count != 0)
                ActivateItem(cog.torso[Random.Range(1, cog.torso.Count)]);

            // determine chance for upper arms to be different and activate
            if (cog.arm_Upper_Right.Count != 0)
                RandomizeLeftRight(cog.arm_Upper_Right, cog.arm_Upper_Left, 15);

            // determine chance for lower arms to be different and activate
            if (cog.arm_Lower_Right.Count != 0)
                RandomizeLeftRight(cog.arm_Lower_Right, cog.arm_Lower_Left, 15);

            // determine chance for hands to be different and activate
            if (cog.hand_Right.Count != 0)
                RandomizeLeftRight(cog.hand_Right, cog.hand_Left, 15);

            // select hips starting at index 1
            if (cog.hips.Count != 0)
                ActivateItem(cog.hips[Random.Range(1, cog.hips.Count)]);

            // determine chance for legs to be different and activate
            if (cog.leg_Right.Count != 0)
                RandomizeLeftRight(cog.leg_Right, cog.leg_Left, 15);

            // select chest attachment
            if (_allGenderObjectGroups.chest_Attachment.Count != 0)
                ActivateItem(_allGenderObjectGroups.chest_Attachment[Random.Range(0, _allGenderObjectGroups.chest_Attachment.Count)]);

            // select back attachment
            if (_allGenderObjectGroups.back_Attachment.Count != 0)
                ActivateItem(_allGenderObjectGroups.back_Attachment[Random.Range(0, _allGenderObjectGroups.back_Attachment.Count)]);

            // determine chance for shoulder attachments to be different and activate
            if (_allGenderObjectGroups.shoulder_Attachment_Right.Count != 0)
                RandomizeLeftRight(_allGenderObjectGroups.shoulder_Attachment_Right, _allGenderObjectGroups.shoulder_Attachment_Left, 10);

            // determine chance for elbow attachments to be different and activate
            if (_allGenderObjectGroups.elbow_Attachment_Right.Count != 0)
                RandomizeLeftRight(_allGenderObjectGroups.elbow_Attachment_Right, _allGenderObjectGroups.elbow_Attachment_Left, 10);

            // select hip attachment
            if (_allGenderObjectGroups.hips_Attachment.Count != 0)
                ActivateItem(_allGenderObjectGroups.hips_Attachment[Random.Range(0, _allGenderObjectGroups.hips_Attachment.Count)]);

            // determine chance for knee attachments to be different and activate
            if (_allGenderObjectGroups.knee_Attachement_Right.Count != 0)
                RandomizeLeftRight(_allGenderObjectGroups.knee_Attachement_Right, _allGenderObjectGroups.knee_Attachement_Left, 10);

            // start randomization of the random characters colors
            RandomizeColors(skinColor);
        }
        
        // handle randomization of the random characters colors
        void RandomizeColors(SkinColor skinColor)
        {
            // set skin and hair colors based on skin color roll
            switch (skinColor)
            {
                case SkinColor.Human:
                    RandomizeAndSetHairSkinColors("Human", _characterData.humanSkin,  _characterData.allHairAndSubble, _characterData.allHairAndSubble, _characterData.allHairAndSubble);
                    break;

                case SkinColor.Elf:
                    RandomizeAndSetHairSkinColors("Elf", _characterData.elfSkin,  _characterData.allHairAndSubble, _characterData.allHairAndSubble, _characterData.allHairAndSubble);
                    break;
                
                case SkinColor.Orc:
                    RandomizeAndSetHairSkinColors("Orc", _characterData.orcSkin,   _characterData.allHairAndSubble, _characterData.allHairAndSubble, _characterData.allHairAndSubble);
                    break;
            }

            // randomize and set primary color
            if (_characterData.primary.Length != 0)
                _material.SetColor("_Color_Primary", _characterData.primary[Random.Range(0, _characterData.primary.Length)]);
            else
                Debug.Log("No Primary Colors Specified In The Inspector");

            // randomize and set secondary color
            if (_characterData.secondary.Length != 0)
                _material.SetColor("_Color_Secondary", _characterData.secondary[Random.Range(0, _characterData.secondary.Length)]);
            else
                Debug.Log("No Secondary Colors Specified In The Inspector");

            // randomize and set primary metal color
            if (_characterData.metalPrimary.Length != 0)
                _material.SetColor("_Color_Metal_Primary", _characterData.metalPrimary[Random.Range(0, _characterData.metalPrimary.Length)]);
            else
                Debug.Log("No Primary Metal Colors Specified In The Inspector");

            // randomize and set secondary metal color
            if (_characterData.metalSecondary.Length != 0)
                _material.SetColor("_Color_Metal_Secondary", _characterData.metalSecondary[Random.Range(0, _characterData.metalSecondary.Length)]);
            else
                Debug.Log("No Secondary Metal Colors Specified In The Inspector");

            // randomize and set primary leather color
            if (_characterData.leatherPrimary.Length != 0)
                _material.SetColor("_Color_Leather_Primary", _characterData.leatherPrimary[Random.Range(0, _characterData.leatherPrimary.Length)]);
            else
                Debug.Log("No Primary Leather Colors Specified In The Inspector");

            // randomize and set secondary leather color
            if (_characterData.leatherSecondary.Length != 0)
                _material.SetColor("_Color_Leather_Secondary", _characterData.leatherSecondary[Random.Range(0, _characterData.leatherSecondary.Length)]);
            else
                Debug.Log("No Secondary Leather Colors Specified In The Inspector");

            // randomize and set body art color
            if (_characterData.bodyArt.Length != 0)
                _material.SetColor("_Color_BodyArt", _characterData.bodyArt[Random.Range(0, _characterData.bodyArt.Length)]);
            else
                Debug.Log("No Body Art Colors Specified In The Inspector");

            // randomize and set body art amount
            _material.SetFloat("_BodyArt_Amount", Random.Range(0.0f, 1.0f));
        }
        
        void RandomizeAndSetHairSkinColors(string info, Color[] skin, Color[] hair, Color[] stubble, Color[] scar)
        {
            // randomize and set elf skin color
            if (skin.Length != 0)
            {
                _material.SetColor("_Color_Skin", skin[Random.Range(0, skin.Length)]);
            }
            else
            {
                Debug.Log("No " + info + " Skin Colors Specified In The Inspector");
            }

            // randomize and set elf hair color
            if (hair.Length != 0)
            {
                _material.SetColor("_Color_Hair", hair[Random.Range(0, hair.Length)]);
            }
            else
            {
                Debug.Log("No " + info + " Hair Colors Specified In The Inspector");
            }

            // set stubble color
            _material.SetColor("_Color_Stubble", stubble[Random.Range(0, stubble.Length)]);

            // set scar color
            _material.SetColor("_Color_Scar", scar[Random.Range(0, scar.Length)]);
        }
        
        // method for handling the chance of left/right items to be differnt (such as shoulders, hands, legs, arms)
        void RandomizeLeftRight(List<GameObject> objectListRight, List<GameObject> objectListLeft, int rndPercent)
        {
            // rndPercent = chance for left item to be different

            // stored right index
            int index = Random.Range(0, objectListRight.Count);

            // enable item from list using index
            ActivateItem(objectListRight[index]);

            // roll for left item mismatch, if true randomize index based on left item list
            if (GetPercent(rndPercent))
                index = Random.Range(0, objectListLeft.Count);

            // enable left item from list using index
            ActivateItem(objectListLeft[index]);
        }
        
        private void ActivateItem(GameObject gameObject)
        {
            gameObject.SetActive(true);
            _enabledObjects.Add(gameObject);
        }
        
        // method for rolling percentages (returns true/false)
        bool GetPercent(int pct)
        {
            bool p = false;
            int roll = Random.Range(0, 100);
            if (roll <= pct)
            {
                p = true;
            }
            return p;
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
        
        void BuildList(List<GameObject> targetList, string characterPart)
        {
            Transform[] rootTransform = _player.GetComponentsInChildren<Transform>();

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

                // collect the material for the random character, only if null in the inspector;
                if (!_material)
                {
                    if (item.GetComponent<SkinnedMeshRenderer>())
                        _material = item.GetComponent<SkinnedMeshRenderer>().material;
                }
            }
        }
    }
    public enum SkinColor { Human, Elf, Orc }
}