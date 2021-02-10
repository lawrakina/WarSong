using System;
using Controller;
using Enums;
using Unit.Enemies;
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
    }
}