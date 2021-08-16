using System;
using Code.Equipment;
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
        public AttackValue AttackValue;
        [SerializeField]
        public UnitLevel unitLevel;
    }
}