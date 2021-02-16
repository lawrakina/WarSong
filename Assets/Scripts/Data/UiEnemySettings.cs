using System;
using Enums;
using UnityEngine;


namespace Data
{
    [Serializable]
    public sealed class UiEnemySettings
    {
        [SerializeField]
        public GameObject UiView;
        [SerializeField]
        public EnemyType EnemyType;
    }
}