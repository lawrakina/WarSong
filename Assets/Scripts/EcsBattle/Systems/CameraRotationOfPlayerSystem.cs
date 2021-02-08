using EcsBattle.Components;
using Leopotam.Ecs;
using UnityEngine;


namespace EcsBattle
{
    public sealed class CameraRotationOfPlayerSystem : IEcsRunSystem
    {
        private EcsFilter<FightCameraComponent, TransformComponent, TargetCameraComponent> _filter;

        public void Run()
        {
            foreach (var index in _filter)
            {
                ref var transform = ref _filter.Get2(index).Value;
                ref var target = ref _filter.Get3(index);
                transform.LookAt(target.Rotate);
            }
        }
    }
}