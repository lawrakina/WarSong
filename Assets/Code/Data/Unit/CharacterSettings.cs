using System;
using PsychoticLab;
using UnityEngine;


namespace Code.Data.Unit
{
    [Serializable]
    public class CharacterSettings
    {
        [SerializeField]
        public CharacterClass CharacterClass;
        
        [SerializeField]
        public CharacterGender CharacterGender;

        [SerializeField]
        public CharacterRace CharacterRace;

        [SerializeField] 
        public CharacterEquipment Equipment;

        [SerializeField] 
        public int ExperiencePoints = 1;

        // [SerializeField] public PersonSettings PersonSettings;
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