using Code.Profile.Models;
using Leopotam.Ecs;


namespace Code.Fight.EcsBattle.Statistics
{
    public sealed class UpdateStatisticsOfBattleGroundSystem: IEcsRunSystem
    {
        private InOutControlFightModel _model;
        private EcsFilter<NeedToAddToStatisticsComponent, UnitComponent, EnemyComponent> _filter;
        
        public void Run()
        {
            foreach (var i in _filter)
            {
                ref var entity = ref _filter.GetEntity(i);
                ref var killerEntity = ref _filter.Get1(i);
                ref var unit = ref _filter.Get2(i);
                

                _model.BattleProgress.CurrentEnemy++;
                // killerEntity.killer.Get<RewardForKillingComponent>().value = unit.level.RewardForKilling;
                entity.Del<UnitComponent>();
                entity.Del<EnemyComponent>();
                entity.Del<NeedToAddToStatisticsComponent>();
            }
        }
    }
}