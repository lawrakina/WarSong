using System.Collections;
using System.Collections.Generic;
using Code.Fight.EcsBattle;
using Leopotam.Ecs;
using UnityEngine;

public class UpdateEnemiesCurrentHealthPointsSystem : IEcsRunSystem
{
    private EcsFilter<EnemyComponent, UnitComponent, UiEnemyHealthBarComponent> _filter;
    
    public void Run()
    {
        foreach (var i in _filter)
        {
            ref var enemyHealthBar = ref _filter.GetEntity(i);
            var maxHp = enemyHealthBar.Get<UnitComponent>()._health.MaxHp;
            var currentHp = enemyHealthBar.Get<UnitComponent>()._health.CurrentHp;
        }
    }
}
