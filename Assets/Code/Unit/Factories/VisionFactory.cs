using Code.Data.Unit;
using Code.Data.Unit.Player;
using UnityEngine;


namespace Code.Unit.Factories{
    public sealed class VisionFactory{
        private readonly PlayerClassesData _settings;

        public VisionFactory(PlayerClassesData settings){
            _settings = settings;
        }

        public UnitVision GenerateVision(IPlayerView character){
            var visionSensor = new UnitVision();
            visionSensor.Sensor =
                Object.Instantiate(_settings.characterVisionData.SensorToolkit, character.Transform, true);
            visionSensor.Sensor.transform.localPosition = _settings.characterVisionData.offsetHead;
            visionSensor.Sensor.name = $"-->Sensor<--";
            visionSensor.Sensor.enabled = false;

            return visionSensor;
        }
    }
}