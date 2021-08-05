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
        [SerializeField]
        public Vector3 offsetHead = new Vector3(0.0f,1.5f,0.0f);
    }
}