using System;
using UnityEngine;


namespace Unit
{
    [Serializable]
    public class UnitVision
    {
        [SerializeField]
        public float BattleDistance = 15.0f;
        [SerializeField]
        public int MaxCountTargets = 5;
        [SerializeField]
        public Vector3 OffsetHead = new Vector3(0.0f,1.5f,0.0f);
    }
}