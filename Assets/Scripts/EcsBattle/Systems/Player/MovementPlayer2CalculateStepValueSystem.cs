using EcsBattle.Components;
using EcsBattle.Systems.Player;
using Leopotam.Ecs;
using UnityEngine;


namespace EcsBattle
{
    public sealed class MovementPlayer2CalculateStepValueSystem : IEcsRunSystem
    {
        private EcsFilter<PlayerComponent,UnitComponent, DirectionMovementComponent> _filter;

        public void Run()
        {
            foreach (var index in _filter)
            {
                ref var entity = ref _filter.GetEntity(index);
                ref var playerTransform = ref _filter.Get2(index).rootTransform;
                ref var directionMoving = ref _filter.Get3(index).value;

                if (directionMoving.localPosition.sqrMagnitude > Vector3.kEpsilon)
                {
                    entity.Get<NeedStepComponent>().value = playerTransform.position - directionMoving.position;
                    entity.Get<NeedRotateComponent>().value = directionMoving;
                }
            }
        }
    }
}