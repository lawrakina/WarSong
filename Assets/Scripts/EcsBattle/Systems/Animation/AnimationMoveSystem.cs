using EcsBattle.Components;
using EcsBattle.Systems.Player;
using Extension;
using Leopotam.Ecs;


namespace EcsBattle.Systems.Animation
{
    public sealed class AnimationMoveSystem : IEcsRunSystem
    {
        private EcsFilter<PlayerComponent, DirectionMovementComponent> _filter;
        public void Run()
        {
            foreach (var index in _filter)
            {
                ref var player = ref _filter.Get1(index);
                ref var direction = ref _filter.Get2(index).value;

                player.animator.Speed = direction.localPosition.z;
                player.animator.HorizontalSpeed = direction.localPosition.x;
            }
        }
    }
}