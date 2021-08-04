using Code.Data.Dungeon;
using Code.Profile.Models;
using Leopotam.Ecs;
using UnityEngine;


namespace Code.Fight.EcsBattle.Statistics
{
    public class CreateTimerStatisticsObserverSystem : IEcsInitSystem
    {
        private EcsWorld _world;
        private DungeonParams _dungeonParams;
        private InOutControlFightModel _model;
        // private List<IEnemyView> _enemyViews;
        
        public void Init()
        {
            var entity = _world.NewEntity();
            entity.Get<TimerStatisticsObserverComponent>();
            ref var timer = ref entity.Get<TimerStatisticsObserverComponent>();
            timer._maxTime = _dungeonParams.MaxTimeForReward;
            timer._currentTime = 0.0f;
            timer._observer = _model.BattleProgress;
            //Battle time
            timer._observer.MaxTimer = Mathf.RoundToInt(_dungeonParams.MaxTimeForReward);
            timer._observer.CurrentTimer = 0.0f;
            //EnemyCount
            timer._observer.MaxEnemy = 0;//ToDo need fix _enemyViews.Count;
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