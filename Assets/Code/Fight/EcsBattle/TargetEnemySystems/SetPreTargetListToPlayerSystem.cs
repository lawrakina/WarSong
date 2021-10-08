using System;
using System.Collections.Generic;
using Code.Extension;
using Code.Fight.EcsBattle.CustomEntities;
using Code.Fight.EcsBattle.Unit.EnemyHealthBars;
using Leopotam.Ecs;

namespace Code.Fight.EcsBattle.TargetEnemySystems
{
    public class SetPreTargetListToPlayerSystem : IEcsRunSystem
    {
        private EcsFilter<PlayerComponent, UnitComponent, NeedFindTargetComponent> _playerFilter;
        private EcsFilter<UnitComponent, EnemyComponent, EnemyInSight> _enemyFilter;

        public void Run()
        {
            //     foreach (var i in _enemyFilter)
            //     {
            //         ref var enemyUnitComponent = ref _enemyFilter.Get1(i);
            //         
            //         foreach (var j in _playerFilter)
            //         {
            //             ref var playerEntity = ref _playerFilter.GetEntity(j);
            //             ref var playerUnitComponent = ref _playerFilter.Get2(j);
            //             
            //             if (!playerEntity.Has<PreTargetEnemyListComponent>())
            //             {
            //                 playerEntity.Get<PreTargetEnemyListComponent>();
            //                 ref var playerPreTargetComponent = ref playerEntity.Get<PreTargetEnemyListComponent>();
            //                 playerPreTargetComponent.preTargets = new Dictionary<UnitComponent, float>();
            //                 var sqrDistanceToEnemy =
            //                     (enemyUnitComponent._rootTransform.position - playerUnitComponent._rootTransform.position)
            //                     .sqrMagnitude;
            //                 playerPreTargetComponent.preTargets.Add(enemyUnitComponent, sqrDistanceToEnemy);
            //             }
            //             else
            //             {
            //                 ref var playerPreTargetComponent = ref playerEntity.Get<PreTargetEnemyListComponent>();
            //                 
            //                 //TODO Под вопросом???
            //                 playerPreTargetComponent.preTargets.Clear();
            //                 var sqrDistanceToEnemy =
            //                     (enemyUnitComponent._rootTransform.position - playerUnitComponent._rootTransform.position)
            //                     .sqrMagnitude;
            //                 playerPreTargetComponent.preTargets.Add(enemyUnitComponent, sqrDistanceToEnemy);
            //             }
        }
    }
}
