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
            timer._maxTime = _battleSettings._maxTimeForReward;
            timer._currentTime = 0.0f;
            timer._observer = _battleModel;
            //Battle time
            timer._observer.MaxTimer = Mathf.RoundToInt(_battleSettings._maxTimeForReward);
            timer._observer.CurrentTimer = 0.0f;
            //EnemyCount
            timer._observer.MaxEnemy = _enemyViews.Count;
            timer._observer.CurrentEnemy = 0;
            //BagCount
            timer._observer.MaxBag = 0;//_bagViews.Count;
            timer._observer.CurrentBag = 0;
            //BossCount
            timer._observer.MaxRareEnemy = 0;//_bossViews.Count;
            timer._observer.CurrentRareEnemy = 0;
        }
    }
}