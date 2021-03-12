using EcsBattle.Components;
using EcsBattle.Systems.Player;
using Leopotam.Ecs;


namespace EcsBattle.Systems.Animation
{
    public sealed class AnimationMoveSystem : IEcsRunSystem
    {
        private EcsFilter<DirectionMovementComponent, AnimatorComponent> _filter;
        public void Run()
        {
            foreach (var index in _filter)
            {
                ref var animator = ref _filter.Get2(index);
                ref var direction = ref _filter.Get1(index);

                animator.value.Speed = direction.value.localPosition.z;
                animator.value.HorizontalSpeed = direction.value.localPosition.x;
            }
        }
    }
}