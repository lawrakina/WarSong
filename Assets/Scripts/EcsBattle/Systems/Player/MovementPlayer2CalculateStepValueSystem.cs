using EcsBattle.Components;
using EcsBattle.Systems.Player;
using Leopotam.Ecs;


namespace EcsBattle
{
    public sealed class MovementPlayer2CalculateStepValueSystem : IEcsRunSystem
    {
        private EcsFilter<PlayerComponent, DirectionMovementComponent> _filter;

        public void Run()
        {
            foreach (var index in _filter)
            {
                ref var entity = ref _filter.GetEntity(index);
                ref var playerTransform = ref _filter.Get1(index).rootTransform;
                ref var directionMoving = ref _filter.Get2(index).value;

                entity.Get<NeedStepComponent>().value = playerTransform.position - directionMoving.position;
                entity.Get<NeedStepComponent>().needMove = true;
                entity.Get<NeedStepComponent>().needRotate = true;
            }
        }
    }
}