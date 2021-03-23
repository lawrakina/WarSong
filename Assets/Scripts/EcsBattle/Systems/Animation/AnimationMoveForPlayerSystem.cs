using EcsBattle.Components;
using Extension;
using Leopotam.Ecs;


namespace EcsBattle.Systems.Animation
{
    public sealed class AnimationMoveForPlayerSystem : IEcsRunSystem
    {
        private EcsFilter<PlayerComponent, UnitComponent, DirectionMovementComponent> _filter;
        public void Run()
        {
            foreach (var index in _filter)
            {
                ref var unit = ref _filter.Get2(index);
                ref var direction = ref _filter.Get3(index);
                // if(direction.value.localPosition.x < Vector3.kEpsilon && 
                //    direction.value.localPosition.z < Vector3.kEpsilon) return;
                
                unit.animator.Speed = direction.value.localPosition.z;
                unit.animator.HorizontalSpeed = direction.value.localPosition.x;
            }
        }
    }
}