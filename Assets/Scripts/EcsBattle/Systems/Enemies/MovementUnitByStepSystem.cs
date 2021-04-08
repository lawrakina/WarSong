using EcsBattle.Components;
using Extension;
using Leopotam.Ecs;
using UnityEngine;


namespace EcsBattle.Systems.Enemies
{
    public sealed class MovementUnitByStepSystem : IEcsRunSystem
    {
        private EcsFilter<NeedStepComponent, UnitComponent,EnemyComponent> _filter;
        public void Run()
        {
            foreach (var i in _filter)
            {
                ref var entity = ref _filter.GetEntity(i);
                ref var step = ref _filter.Get1(i);
                ref var transform = ref _filter.Get2(i)._rootTransform;
                ref var rigidBody = ref _filter.Get2(i)._rigidBody;
                ref var moveSpeed = ref _filter.Get2(i)._attributes.Speed;
                
                rigidBody.MovePosition(transform.position + (step._value * (moveSpeed * Time.fixedDeltaTime)));
                
                entity.Del<NeedStepComponent>();
            }
        }
    }
}