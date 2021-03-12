using EcsBattle.Components;
using Leopotam.Ecs;


namespace EcsBattle.Systems.Animation
{
    public sealed class AnimationMoveSystem : IEcsRunSystem
    {
        private EcsFilter<DirectionMoving, AnimatorComponent> _filter;
        public void Run()
        {
            foreach (var index in _filter)
            {
                ref var animator = ref _filter.Get2(index);
                ref var direction = ref _filter.Get1(index);

                animator.Value.Speed = direction.Value.z;
                animator.Value.HorizontalSpeed = direction.Value.x;
            }
        }
    }
}