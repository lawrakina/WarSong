using Leopotam.Ecs;
using UnityEngine;


namespace Code.Fight.EcsFight.Timer{
    public class UniversalTimerS : IEcsRunSystem{
        private EcsFilter<TimerForAdd> _timers;
        private EcsFilter<TimerForAdd, ClearTimer> _timerForRemove;

        public void Run(){
            foreach (var i in _timers){
                ref var entity = ref _timers.GetEntity(i);
                ref var timer = ref _timers.Get1(i);
                timer.TimeLeftSec -= Time.deltaTime;
                if (timer.TimeLeftSec < 0f){
                    // var newEntity = timer.TargetEntity;
                    // entity.Del<TimerForAdd>();
                    timer.TargetEntity.Get<ClearTimer>();
                    entity.MoveTo(timer.TargetEntity);
                }
            }

            foreach (var i in _timerForRemove){
                ref var entity = ref _timerForRemove.GetEntity(i);
                entity.Del<ClearTimer>();
                entity.Del<TimerForAdd>();
            }
        }
    }
}