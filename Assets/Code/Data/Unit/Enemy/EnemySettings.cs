using System;
using UnityEngine;


namespace Code.Data.Unit.Enemy
{
    [Serializable]
    public sealed class EnemySettings
    {
        [SerializeField]
        public GameObject EnemyView;
        [SerializeField]
        public EnemyType EnemyType;
        [SerializeField]
        public UnitVision unitVisionComponent;
        [SerializeField]
        public float MaxHp;
        [SerializeField]
        public UnitEnemyEquipment unitEquipment;
        [SerializeField]
        public UnitLevel unitLevel;
    }
}