using System;
using Code.Unit;
using UnityEngine;


namespace Code.Data.Unit.Enemy
{
    [Serializable]
    public sealed class UiEnemySettings
    {
        [SerializeField]
        public HealthBarView UiView;
        [SerializeField]
        public EnemyType EnemyType;
        [SerializeField]
        public Vector3 Offset = new Vector3(0.0f, 2.1f, 0.0f);
    }
}