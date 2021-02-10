using System;
using Enums;
using UnityEngine;


namespace Data
{
    [Serializable] public sealed class CharacterSettings
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

        [SerializeField]
        public CharacterEquipment Equipment;
    }

    [Serializable]
    public sealed class CharacterEquipment
    {
        public EquipmentSlot MainWeapon;
        public EquipmentSlot SecondWeapon;
    }

    public class EquipmentSlot
    {
        public Weapon
    }
}