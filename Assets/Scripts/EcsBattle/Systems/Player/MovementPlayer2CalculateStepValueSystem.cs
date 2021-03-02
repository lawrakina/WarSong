using EcsBattle.Components;
using Leopotam.Ecs;
using UnityEngine;


namespace EcsBattle
{
    public sealed class MovementPlayer2CalculateStepValueSystem : IEcsRunSystem
    {
        private EcsFilter<PlayerComponent, DirectionMoving, BaseUnitComponent> _filter;

        public void Run()
        {
            foreach (var index in _filter)
            {
                ref var directionMoving = ref _filter.Get2(index).Value;
                ref var playerTransform = ref _filter.Get3(index).transform;
                ref var goTargetTransform = ref _filter.GetEntity(index).Get<GoTargetComponent>().Value.Get<TransformComponent>().Value;

                goTargetTransform.localPosition = directionMoving;

                _filter.GetEntity(index).Get<NeedStepComponent>().Value = playerTransform.position - goTargetTransform.position;
            }
        }
    }

    public struct NeedStepComponent
    {
        public Vector3 Value;
    }
}