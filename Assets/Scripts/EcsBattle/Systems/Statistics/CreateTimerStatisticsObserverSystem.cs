using System.Collections.Generic;
using Data;
using EcsBattle.Components;
using Leopotam.Ecs;
using Models;
using Unit.Enemies;
using UnityEngine;


namespace EcsBattle.Systems.Statistics
{
    public sealed class CreateTimerStatisticsObserverSystem : IEcsInitSystem
    {
        private EcsWorld _world;
        private BattleSettingsData _battleSettings;
        private BattleProgressModel _battleModel;
        private List<IEnemyView> _enemyViews;
        
        public void Init()
        {
            var entity = _world.NewEntity();
            entity.Get<TimerStatisticsObserverComponent>();
            ref var timer = ref entity.Get<TimerStatisticsObserverComponent>();
            timer.maxTime = _battleSettings._maxTimeForReward;
            timer.currentTime = 0.0f;
            timer.observer = _battleModel;
            //Battle time
            timer.observer.MaxTimer = Mathf.RoundToInt(_battleSettings._maxTimeForReward);
            timer.observer.CurrentTimer = 0.0f;
            //EnemyCount
            timer.observer.MaxEnemy = _enemyViews.Count;
            timer.observer.CurrentEnemy = 0;
        }
    }
}