using Code.Data.Dungeon;
using Code.Fight.EcsFight.Timer;
using Leopotam.Ecs;
using UnityEngine;


namespace Code.Fight.EcsFight.Settings{
    public class UnitBehaviourSettingsS : IEcsInitSystem, IEcsRunSystem{
        private EcsWorld _world = null;
        private DungeonParams _dungeonParams;
        private EcsFilter<PlayerTag, ClickEventC> _clickFilter;
        private EcsFilter<PlayerTag, SwipeEventC> _swipeFilter;

        private EcsFilter<EnemyTag, Timer<CheckVisionTag>> _enemiesWithTimer;
        private EcsFilter<EnemyTag>.Exclude<Timer<CheckVisionTag>> _enemiesBattleWithoutTimer;

        public void Init(){
            foreach (var i in _enemiesBattleWithoutTimer){
                ref var entity = ref _enemiesBattleWithoutTimer.GetEntity(i);
                entity.Get<Timer<CheckVisionTag>>().TimeLeftSec = 3 + Random.Range(-2f, 1f);
            }
        }

        public void Run(){
            foreach (var i in _clickFilter){
                ref var entity = ref _clickFilter.GetEntity(i);
                entity.Get<Timer<BattleTag>>().TimeLeftSec = _dungeonParams.DurationOfAggreState;
                entity.Get<NeedFindTargetCommand>();
                entity.Get<NeedAttackTargetCommand>();
                entity.Del<ClickEventC>();
            }

            foreach (var i in _swipeFilter){
                ref var entity = ref _swipeFilter.GetEntity(i);
                entity.Del<SwipeEventC>();
            }

            foreach (var i in _enemiesBattleWithoutTimer){
                ref var entity = ref _enemiesBattleWithoutTimer.GetEntity(i);
                entity.Get<NeedFindTargetCommand>();
                entity.Get<NeedAttackTargetCommand>();
                entity.Get<Timer<CheckVisionTag>>().TimeLeftSec = 3 + Random.Range(-2f, 1f);
            }
        }
    }

    public struct CheckVisionTag{
    }

    public struct NeedAttackTargetCommand{
    }
    
    public struct NeedFindTargetCommand{
    }
}