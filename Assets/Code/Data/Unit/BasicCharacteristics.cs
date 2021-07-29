using System;
using UnityEngine;


namespace Code.Data.Unit
{
    [Serializable] public class BasicCharacteristics
    {
        [SerializeField] 
        public CharacterClass CharacterClass;

        [SerializeField]
        public Characteristics Start;

        [SerializeField]
        public Characteristics ForOneLevel;

        [NonSerialized]
        public Characteristics Values;
    }
}