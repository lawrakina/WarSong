using System;
using SensorToolkit;
using UnityEngine;


namespace Code.Data.Unit{
    [Serializable] public class CharacterVisionData{
        [SerializeField]
        private Sensor _sensorToolkit;
        [SerializeField]
        public Vector3 offsetHead = new Vector3(0.0f, 1.5f, 0.0f);
        public Sensor SensorToolkit => _sensorToolkit;
    }
}