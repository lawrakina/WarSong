using Leopotam.Ecs;
using UnityEngine;


namespace Code.Fight.EcsBattle.Unit.Move
{
    public sealed class MovementPlayer2CalculateStepValueSystem : IEcsRunSystem
    {
        private EcsFilter<PlayerComponent,UnitComponent, DirectionMovementComponent> _filter;

        public void Run()
        {
            foreach (var index in _filter)
            {
                ref var entity = ref _filter.GetEntity(index);
                ref var playerTransform = ref _filter.Get2(index)._rootTransform;
                ref var directionMoving = ref _filter.Get3(index)._value;

                if (directionMoving.localPosition.sqrMagnitude > Vector3.kEpsilon)
                {
                    entity.Get<NeedStepComponent>()._value = playerTransform.position - directionMoving.position;
                    entity.Get<NeedRotateComponent>()._value = directionMoving;
                }
            }
        }
    }
}