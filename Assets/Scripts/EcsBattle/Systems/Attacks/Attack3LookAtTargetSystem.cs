﻿using EcsBattle.Components;
using Leopotam.Ecs;
using UnityEngine;


namespace EcsBattle.Systems.Attacks
{
    public sealed class Attack3LookAtTargetSystem : IEcsRunSystem
    {
        private EcsFilter<NeedLookAtTargetComponent, CurrentTargetComponent, PlayerComponent, UnitComponent> _filter;
        public void Run()
        {
            foreach (var i in _filter)
            {
                ref var entity = ref _filter.GetEntity(i);
                // ref var rootTransform = ref _filter.Get3(i).rootTransform;
                ref var modelTransform = ref _filter.Get4(i)._modelTransform;
                ref var target = ref _filter.Get2(i);
                
                if(target._baseUnitView != null)
                    modelTransform.LookAt(target._baseUnitView.Transform);
                else
                    modelTransform.localRotation = Quaternion.identity;
                
                entity.Del<NeedLookAtTargetComponent>();
            }
        }
    }
}