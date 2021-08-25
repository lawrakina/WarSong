using System;
using System.Collections;
using System.Collections.Generic;
using Code.Fight.EcsBattle;
using Code.Fight.EcsBattle.CustomEntities;
using Code.Unit;
using UnityEngine;
using Leopotam.Ecs;
using UniRx;

public class RotateUiHeathBarsToCameraSystem : IEcsRunSystem
{
    private EcsFilter<UiEnemyHealthBarComponent> _filter;
    
    public void Run()
    {
        foreach (var i in _filter)
        {
            ref var enemyEntity = ref _filter.GetEntity(i);
            enemyEntity.Get<UiEnemyHealthBarComponent>()._value.AlignCamera();
        }
        
    }
}
