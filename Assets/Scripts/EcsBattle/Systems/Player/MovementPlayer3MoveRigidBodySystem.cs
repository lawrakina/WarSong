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
                ref var transform = ref _filter.Get3(i).rootTransform;
                ref var rigidBody = ref _filter.Get3(i).rigidbody;
                ref var moveSpeed = ref _filter.Get3(i).attributes.Speed;
                
                rigidBody.MovePosition(transform.position - (needStep.value * (moveSpeed * Time.fixedDeltaTime)));

                entity.Del<NeedStepComponent>();
            }
        }
    }
}