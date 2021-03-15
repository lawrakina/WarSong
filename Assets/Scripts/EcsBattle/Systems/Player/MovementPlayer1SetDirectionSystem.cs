using EcsBattle.Components;
using EcsBattle.Systems.Player;
using Leopotam.Ecs;
using UnityEngine;


namespace EcsBattle.Systems.PlayerMove
{
    public class MovementPlayer1SetDirectionSystem : IEcsRunSystem
    {
        private EcsFilter<MovementEventComponent, DirectionMovementComponent> _filter;

        public void Run()
        {
            foreach (var i in _filter)
            {
                ref var entity = ref _filter.GetEntity(i);
                ref var eventVector = ref _filter.Get1(i);
                ref var direction = ref _filter.Get2(i);

                direction.value.localPosition =  Vector3.ClampMagnitude(eventVector.value, 1f);

                entity.Del<MovementEventComponent>();
            }
        }
    }
}