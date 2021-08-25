using System.Collections;
using System.Collections.Generic;
using Code.Extension;
using Code.Fight.EcsBattle;
using Leopotam.Ecs;
using UnityEngine;

public class UpdateEnemiesCurrentHealthPointsSystem : IEcsRunSystem
{
    private EcsFilter<UnitComponent, UiEnemyHealthBarComponent> _filter;
    
    public void Run()
    {
        foreach (var i in _filter)
        {
            ref var enemyEntity = ref _filter.GetEntity(i);
            var maxHp = enemyEntity.Get<UnitComponent>()._health.MaxHp;
            var currentHp = enemyEntity.Get<UnitComponent>()._health.CurrentHp;
            var enemyHealthBar = enemyEntity.Get<UiEnemyHealthBarComponent>()._value;
            enemyHealthBar.ChangeValue(currentHp, maxHp);
            // Dbg.Log($"{currentHp/maxHp}");
        }
    }
}
