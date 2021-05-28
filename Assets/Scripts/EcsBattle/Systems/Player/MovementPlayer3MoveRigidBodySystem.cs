using EcsBattle.Components;
using Leopotam.Ecs;
using UnityEngine;


namespace EcsBattle.Systems.Player
{
    public sealed class MovementPlayer3MoveRigidBodySystem : IEcsRunSystem
    {
        private EcsFilter<NeedStepComponent, PlayerComponent, UnitComponent> _filter;
        public void Run()
        {
            foreach (var i in _filter)
            {
                ref var entity = ref _filter.GetEntity(i);
                ref var needStep = ref _filter.Get1(i);
                ref var transform = ref _filter.Get3(i)._rootTransform;
                ref var rigidBody = ref _filter.Get3(i)._rigidBody;
                ref var moveSpeed = ref _filter.Get3(i)._characteristics.Speed;
                
                rigidBody.MovePosition(transform.position - (needStep._value * (moveSpeed * Time.fixedDeltaTime)));

                entity.Del<NeedStepComponent>();
            }
        }
    }
}