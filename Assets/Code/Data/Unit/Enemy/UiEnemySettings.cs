using System;
using UnityEngine;


namespace Code.Data.Unit.Enemy
{
    [Serializable]
    public sealed class UiEnemySettings
    {
        [SerializeField]
        public GameObject UiView;
        [SerializeField]
        public EnemyType EnemyType;
        [SerializeField]
        public Vector3 Offset = new Vector3(0.0f, 2.1f, 0.0f);
    }
}