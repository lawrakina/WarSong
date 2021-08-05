using Leopotam.Ecs;
using UnityEngine;


namespace Code.Fight.EcsBattle.Unit.Move
{
    public sealed class MovementPlayer4RotateTransformSystem : IEcsRunSystem
    {
        private EcsFilter<NeedRotateComponent, PlayerComponent,UnitComponent> _filter;

        public void Run()
        {
            foreach (var i in _filter)
            {
                ref var entity = ref _filter.GetEntity(i);
                ref var needRotate = ref _filter.Get1(i);
                ref var rootTransform = ref _filter.Get3(i)._rootTransform;
                ref var modelTransform = ref _filter.Get3(i)._modelTransform;
                ref var rotateSpeed = ref _filter.Get3(i)._characteristics.RotateSpeedPlayer;

                rootTransform.RotateAround(
                    rootTransform.position,
                    Vector3.up,
                    rotateSpeed * Time.fixedDeltaTime * needRotate._value.localPosition.x
                );
                modelTransform.localRotation = Quaternion.identity;

                entity.Del<NeedRotateComponent>();
            }
        }
    }
}