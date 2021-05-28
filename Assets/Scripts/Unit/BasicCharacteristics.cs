using System;
using UnityEngine;


namespace Unit
{
    [Serializable] public class BasicCharacteristics
    {
        [Serializable] public struct Characteristics
        {
            [SerializeField]
            public float Strength;

            [SerializeField]
            public float Agility;

            [SerializeField]
            public float Stamina;

            [SerializeField]
            public float Intellect;

            [SerializeField]
            public float Spirit;

            public Characteristics(
                float strength = 1.0f,
                float agility = 1.0f,
                float stamina = 1.0f,
                float intellect = 1.0f,
                float spirit = 1.0f)
            {
                Strength = strength;
                Agility = agility;
                Stamina = stamina;
                Spirit = spirit;
                Intellect = intellect;
            }
        }

        [SerializeField]
        public Characteristics Start;

        [SerializeField]
        public Characteristics ForOneLevel;

        [NonSerialized]
        public Characteristics Values;

        // [Header("Start value")]
        //
        // public float StartHp = 1;
        // public float StartResource = 1;
        // public float StartStrength = 1;
        // public float StartAgility = 1;
        // public float StartStamina = 1;
        // public float StartIntellect = 1;
        // public float StartSpirit = 1;
        //
        //
        // [Header("Up for 1 level")]
        // public float StrengthForLevel;
        // public float AgilityForLevel;
        // public float StaminaForLevel;
        // public float IntellectForLevel;
        // public float SpiritForLevel;
        //
    }
}