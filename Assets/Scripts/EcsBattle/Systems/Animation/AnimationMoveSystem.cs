using EcsBattle.Components;
using EcsBattle.Systems.Player;
using Extension;
using Leopotam.Ecs;
using UnityEngine;


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
                ref var direction = ref _filter.Get2(index);
                // if(direction.value.localPosition.x < Vector3.kEpsilon && 
                //    direction.value.localPosition.z < Vector3.kEpsilon) return;
                
                player.animator.Speed = direction.value.localPosition.z;
                player.animator.HorizontalSpeed = direction.value.localPosition.x;
            }
        }
    }
}