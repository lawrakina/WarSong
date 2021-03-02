﻿using EcsBattle.Components;
using Leopotam.Ecs;


namespace EcsBattle.Systems.Attacks
{
    public sealed class ApplyDamageInUnitSystem : IEcsRunSystem
    {
        private EcsFilter<UnitHpComponent, AttackCollisionComponent> _filter;
    
        public void Run()
        {
            foreach (var i in _filter)
            {
                ref var unitHp = ref _filter.Get1(i);
                ref var infoCollision = ref _filter.Get2(i);
                
                unitHp.CurrentValue -= infoCollision.Value.Damage;
                _filter.GetEntity(i).Del<AttackCollisionComponent>();
            }
        }
    }
}