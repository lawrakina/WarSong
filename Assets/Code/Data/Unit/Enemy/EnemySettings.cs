using System;
using Code.Equipment;
using SensorToolkit;
using UnityEngine;


namespace Code.Data.Unit.Enemy{
    [Serializable] public sealed class EnemySettings{
        [SerializeField]
        public GameObject EnemyView;
        [SerializeField]
        public EnemyType EnemyType;
        [SerializeField]
        public string DisplayName = $"Вражина";
        [SerializeField]
        public CharacterVisionData characterVisionDataComponent;
        [SerializeField]
        public float AdditionalHp;
        [SerializeField]
        public float SpeedModifier = 1f;
        [SerializeField]
        [Range(-5,5)]
        public int LevelOffset = 1;
        [SerializeField]
        public WeaponEquipItem Weapon;
        [SerializeField]
        [Range(-10,10)]
        public int RewardForKillingModifier = 4;
    }
}