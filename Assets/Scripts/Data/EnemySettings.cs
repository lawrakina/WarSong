using System;
using Enums;
using Unit;
using UnityEngine;


namespace Data
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
        public UnitAttributes unitAttributes;
    }
}