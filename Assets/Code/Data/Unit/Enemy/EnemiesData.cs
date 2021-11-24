using System.Collections.Generic;
using System.ComponentModel;
using Code.Equipment;
using SensorToolkit;
using UnityEngine;


namespace Code.Data.Unit.Enemy{
    [CreateAssetMenu(fileName = nameof(EnemiesData), menuName = "Configs/" + nameof(EnemiesData))]
    public sealed class EnemiesData : ScriptableObject{
        [SerializeField]
        public GameObject StorageRootPrefab;
        [SerializeField]
        public Sensor DefaultSensor;

        [Header("Constants")]
        [SerializeField]
        public float _baseMoveSpeed = 5f;
        [SerializeField]
        [Description("Main value")]
        public AttackValue _baseAttackValue;
        [SerializeField]
        [Description("For View")]
        public List<WeaponEquipItem> DefaultWeapons;
        [SerializeField]
        [Range(0, 10000)]
        public int _baseHp = 10;
        [SerializeField]
        [Range(0, 10000)]
        public int _hpForLevel = 17;
        [SerializeField]
        [Range(0, 100000)]
        public int DefaultRewardForKillind = 1;
        [SerializeField]
        public EnemySettings[] Enemies;
        [SerializeField]
        public UiEnemySettings[] uiElement;

        public WeaponEquipItem RandomWeapon(){
            return DefaultWeapons[Random.Range(0, DefaultWeapons.Count)];
        }
    }
}