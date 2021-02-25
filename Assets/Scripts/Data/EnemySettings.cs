using System;
using Enums;
using Unit;
using UnityEngine;
using UnityEngine.Serialization;


namespace Data
{
    [Serializable]
    public sealed class EnemySettings
    {
        [SerializeField]
        public GameObject EnemyView;
        [SerializeField]
        public EnemyType EnemyType;
        [FormerlySerializedAs("VisionComponent")]
        [SerializeField]
        public UnitVision unitVisionComponent;
    }
}