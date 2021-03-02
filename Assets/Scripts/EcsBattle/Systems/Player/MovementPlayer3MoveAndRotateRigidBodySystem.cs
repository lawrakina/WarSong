﻿using EcsBattle.Components;
using Leopotam.Ecs;
using UnityEngine;


namespace EcsBattle
{
    public sealed class MovementPlayer3MoveAndRotateRigidBodySystem : IEcsRunSystem
    {
        private EcsFilter<NeedStepComponent, BaseUnitComponent, MovementSpeed, RotateSpeed, DirectionMoving> _filter;
        public void Run()
        {
            foreach (var index in _filter)
            {
                ref var moveSpeed = ref _filter.Get3(index).Value;
                ref var needStep = ref _filter.Get1(index).Value;
                ref var rigidBody = ref _filter.Get2(index).rigidbody;
                ref var playerTransform = ref _filter.Get2(index).transform;
                ref var rotateSpeed = ref _filter.Get4(index).Value;
                ref var directionMoving = ref _filter.Get5(index).Value;
                
                rigidBody.MovePosition(playerTransform.position - (needStep * (moveSpeed * Time.fixedDeltaTime)));
                playerTransform.RotateAround(
                    playerTransform.position, 
                    Vector3.up, 
                    rotateSpeed * Time.fixedDeltaTime * directionMoving.x
                );

                _filter.GetEntity(index).Del<NeedStepComponent>();
            }
        }
    }
}