using System;
using UnityEngine;


namespace Code.Data.Unit
{
    [Serializable]
    public class UnitVision
    {
        [SerializeField]
        public float distanceDetection = 15.0f;
        [SerializeField]
        public int maxCountTargets = 5;
    }
}