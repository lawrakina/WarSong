using System;
using UnityEngine;


namespace Unit
{
    [Serializable]
    public class Vision
    {
        [SerializeField]
        public float BattleDistance = 15.0f;
        [SerializeField]
        public int MaxCountTaggets = 5;
        [SerializeField]
        public LayerMask LayersEnemies;
    }
}