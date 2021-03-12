using EcsBattle.Components;
using Leopotam.Ecs;
using UnityEngine;


namespace EcsBattle
{
    public sealed class CameraRotateOnPlayerSystem : IEcsRunSystem
    {
        private EcsFilter<FightCameraComponent, TransformComponent> _filter;

        public void Run()
        {
            foreach (var index in _filter)
            {
                ref var transform = ref _filter.Get2(index).value;
                ref var target = ref _filter.Get1(index);

                // transform.rotation = Quaternion.LookRotation(target.positionThirdTarget.position);
                transform.LookAt(target.positionPlayerTransform);
            }
        }
    }
}