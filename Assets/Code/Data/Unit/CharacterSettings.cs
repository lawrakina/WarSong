using System;
using Code.Data.Abilities;
using UnityEngine;


namespace Code.Data.Unit
{
    [Serializable]
    public class CharacterSettings
    {
        public CharacterClass CharacterClass;
        public CharacterGender CharacterGender;
        public CharacterRace CharacterRace;
        public int CharacterSkin;
        public int ExperiencePoints = 1;
        public int CharacterHair;
        public int CharacterStubble;
        public int CharacterScar;
        public CharacterEquipment Equipment;
        public ClassAbilities Abilities;
    }

    // [Serializable]
    // public class PersonSettings
    // {
    //     [SerializeField] private Elements _elements = Elements.Yes;
    //     [SerializeField] private HeadCovering _headCovering = HeadCovering.HeadCoverings_No_Hair;
    //     [SerializeField] private FacialHair _facialHair = FacialHair.Yes;
    //     public Elements Elements => _elements;
    //     public HeadCovering HeadCovering => _headCovering;
    //     public FacialHair FacialHair => _facialHair;
    // }
}