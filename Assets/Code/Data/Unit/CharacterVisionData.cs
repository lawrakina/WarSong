using System;
using SensorToolkit;
using UnityEngine;
using Object = UnityEngine.Object;


namespace Code.Data.Unit{
    [Serializable] public class CharacterVisionData{
        [SerializeField]
        private Sensor angleSensor;
        [SerializeField]
        public Vector3 offsetHead = new Vector3(0.0f, 1.5f, 0.0f);
        // public RangeSensor SphereSensor;
        public Sensor AngleSensor => angleSensor;
    }
}