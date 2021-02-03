using System;
using Enums;
using UnityEngine;


namespace Data
{
    [Serializable] public class CharacterSettings
    {
        [SerializeField]
        public float AgroDistance = 10.0f;

        [SerializeField]
        public CharacterClass CharacterClass;

        [SerializeField]
        public CharacterGender CharacterGender;

        [SerializeField]
        public CharacterRace CharacterRace;

        [SerializeField]
        public float PlayerMoveSpeed = 10.0f;

        [SerializeField]
        public float RotateSpeedPlayer = 90.0f;
    }
}