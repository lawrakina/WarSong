using System;
using UnityEngine;


namespace Unit
{
    [Serializable] public class BasicCharacteristics
    {
        [Header("Start value")]
        public float StartHp = 1;
        public float StartMana = 1;
        public float StartStrength = 1;
        public float StartAgility = 1;
        public float StartStamina = 1;
        public float StartIntellect = 1;
        public float StartSpirit = 1;


        [Header("Up for 1 level")]
        public float StrengthForLevel;
        public float AgilityForLevel;
        public float StaminaForLevel;
        public float IntellectForLevel;
        public float SpiritForLevel;
    }
}