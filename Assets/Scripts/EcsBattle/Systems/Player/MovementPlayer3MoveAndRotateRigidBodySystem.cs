using EcsBattle.Components;
using EcsBattle.Systems.Player;
using Leopotam.Ecs;
using UnityEngine;


namespace EcsBattle
{
    public sealed class MovementPlayer3MoveRigidBodySystem : IEcsRunSystem
    {
        private EcsFilter<NeedStepComponent, TransformComponent, RigidbodyComponent, MovementSpeed> _filter;
        public void Run()
        {
            foreach (var index in _filter)
            {
                ref var needStep = ref _filter.Get1(index);
                ref var transform = ref _filter.Get2(index).value;
                ref var rigidBody = ref _filter.Get3(index).value;
                ref var moveSpeed = ref _filter.Get4(index).value;
                
                rigidBody.MovePosition(transform.position - (needStep.value * (moveSpeed * Time.fixedDeltaTime)));

                needStep.needMove = false;
            }
        }
    }
    
    public sealed class MovementPlayer4RotateTransformSystem : IEcsRunSystem
    {
        private EcsFilter<NeedStepComponent, TransformComponent, RotateSpeed> _filter;
        public void Run()
        {
            foreach (var index in _filter)
            {
                ref var needStep = ref _filter.Get1(index);
                ref var transform = ref _filter.Get2(index).value;
                ref var rotateSpeed = ref _filter.Get3(index).value;
                
                transform.RotateAround(
                    transform.position, 
                    Vector3.up, 
                    rotateSpeed * Time.fixedDeltaTime * needStep.value.x
                );
            
                needStep.needRotate = false;
            }
        }
    }


    public sealed class MovementPlayer5EndSystem : IEcsRunSystem
    {
        private EcsFilter<NeedStepComponent> _filter;
        public void Run()
        {
            foreach (var i in _filter)
            {
                ref var completeStep = ref _filter.Get1(i);
                if(!completeStep.needMove && !completeStep.needRotate)
                    _filter.GetEntity(i).Del<NeedStepComponent>();
            }
        }
    }
    
}