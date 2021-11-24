using Code.Data.Unit;
using Code.Data.Unit.Enemy;
using UnityEngine;


namespace Code.Unit.Factories{
    internal class NpsVisionFactory{
        private readonly EnemiesData _data;

        public NpsVisionFactory(EnemiesData data){
            _data = data;
        }

        public UnitVision GenerateVision(EnemySettings settings, Transform parent){
            var visor = new UnitVision();
            visor.Visor = Object.Instantiate(
                !settings.characterVisionDataComponent.VisionSensor
                    ? _data.DefaultSensor
                    : settings.characterVisionDataComponent.VisionSensor,
                parent, true);

            visor.Visor.transform.localPosition = settings.characterVisionDataComponent.offsetHead;
            visor.Visor.name = $"-->Visor<--";
            visor.Visor.enabled = true;

            return visor;
        }
    }
}