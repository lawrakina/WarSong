using System;
using Code.Data.Abilities;
using UnityEngine;


namespace Code.Data.Unit.Player
{
    [CreateAssetMenu(fileName = nameof(PlayerClassesData), menuName = "Configs/" + nameof(PlayerClassesData))]
    public sealed class PlayerClassesData : ScriptableObject
    {
        [Header("Absolute value")]
        [Range(0f,100f)]
        public float HealthPerStamina = 10.0f;
        [Range(0f,100f)]
        public float ManaPointsPerIntellect = 15.0f;
        [Range(0f,200f)]
        public float MaxRageValue = 100.0f;
        [Range(0f,200f)]
        public float MaxEnergyValue = 100.0f;
        [Range(0f,200f)]
        public float MaxConcentrationValue = 100.0f;
        [Range(0f,10f)]
        public float MoveSpeed = 7.0f;
        [Range(90f,200f)]
        public float RotateSpeed = 120.0f;
        [SerializeField]
        public CharacterVisionData characterVisionData;
        [Space]
        [Header("Class abilities")]
        [SerializeField]
        public ClassAbilities[] _classAbilities;
        [Space]
        
        [Header("Race characteristics")]
        [SerializeField]
        public RaceCharacteristics[] _racesStartCharacteristics;
        [Space]
        
        [Header("Classes start value")]
        [SerializeField]
        public UnitCharacteristics[] _classesStartCharacteristics;
        [Space]

        [Header("Classes start value")]
        [SerializeField]
        public CharacteristicsModifier[] _characteristicsModifiers;
        [Space]
        
        [Header("Presets list characters")]
        [SerializeField]
        public PresetCharacters _presetCharacters;
    }
}