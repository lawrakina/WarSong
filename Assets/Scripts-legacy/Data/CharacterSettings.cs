using System;
using Enums;
using Unit;
using UnityEngine;


namespace Data
{
    [Serializable] public sealed class CharacterSettings
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

        [SerializeField]
        public UnitVision unitVisionParameters;
    }
}