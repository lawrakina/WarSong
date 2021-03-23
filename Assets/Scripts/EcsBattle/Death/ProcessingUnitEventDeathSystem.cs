using EcsBattle.Components;
using EcsBattle.Systems.Attacks;
using EcsBattle.Systems.Enemies;
using Leopotam.Ecs;
using Models;


namespace EcsBattle
{
    public sealed class ProcessingUnitEventDeathSystem : IEcsRunSystem
    {    //ToDo optimisation Ragdoll for mobile =>
        //https://habr.com/ru/post/334922/
        private EcsFilter<UnitComponent, DeathEventComponent, ListRigidBAndCollidersComponent> _filter;
        
        
        public void Run()
        {
            foreach (var i in _filter)
            {
                ref var entity = ref _filter.GetEntity(i);
                ref var unit = ref _filter.Get1(i);
                ref var deathEvent = ref _filter.Get2(i);
                ref var listBbodies = ref _filter.Get3(i);
                // ref var listOfAwards = ref _filter.Get2(i);
            
                //Enable Ragdoll
                for (int j = 0; j < listBbodies.rigidbodies.Count; j++)
                {
                    listBbodies.rigidbodies[j].isKinematic = false;
                    listBbodies.colliders[j].enabled = true;
                }
                
                unit.animator.Off();
                unit.collider.enabled = false;
                unit.rigidbody.isKinematic = true;

                entity.Get<NeedToAddToStatisticsComponent>().killer = deathEvent._killer;
                // foreach (var rigidbody in listBbodies.rigidbodies)
                // {
                //     Dbg.Log($"Bone:{rigidbody.name}");
                //     rigidbody.isKinematic = false;
                // }
            }
        }
    }

    public struct NeedToAddToStatisticsComponent
    {
        public EcsEntity killer;
    }

    public sealed class UpdateStatisticsOfBattleGroundSystem: IEcsRunSystem
    {
        private BattleProgressModel _battleProgressModel;
        private EcsFilter<NeedToAddToStatisticsComponent, UnitComponent, EnemyComponent> _filter;
        
        public void Run()
        {
            foreach (var i in _filter)
            {
                ref var entity = ref _filter.GetEntity(i);
                ref var killerEntity = ref _filter.Get1(i);
                ref var unit = ref _filter.Get2(i);
                
                _battleProgressModel.CurrentEnemy++;
                // killerEntity.killer.Get<RewardForKillingComponent>().value = unit.level.RewardForKilling;
                entity.Del<UnitComponent>();
                entity.Del<EnemyComponent>();
                entity.Del<NeedToAddToStatisticsComponent>();
            }
        }
    }
}