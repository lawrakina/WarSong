using System.Collections.Generic;
using System.Linq;
using Code.Extension;
using Code.Fight.EcsBattle.TargetEnemySystems;
using Leopotam.Ecs;
using UnityEditor.UIElements;
using UnityEngine;

namespace Code.Fight.EcsBattle.Unit.EnemyHealthBars
{
    public class CheckForEnemyInSightSystem : IEcsRunSystem
    {
        private EcsFilter<PlayerComponent, UnitComponent> _playerFilter;
        private EcsFilter<UnitComponent, EnemyComponent> _enemyFilter;
        
        public void Run()
        {
            foreach (var i in _playerFilter)
            {
                ref var playerEntity = ref _playerFilter.GetEntity(i);
                ref var playerVision = ref _playerFilter.Get2(i)._vision;
                ref var playerTransform = ref _playerFilter.Get2(i)._rootTransform;
                ref var distanceDetection = ref _playerFilter.Get2(i)._vision.distanceDetection;
                ref var reputation = ref _playerFilter.Get2(i)._reputation;

                var colliders = new Collider[playerVision.maxCountTargets];
                Physics.OverlapSphereNonAlloc(playerTransform.position, distanceDetection, colliders,
                    1 << reputation.EnemyLayer);

                foreach (var j in _enemyFilter)
                {
                    ref var enemyEntity = ref _enemyFilter.GetEntity(j);
                    ref var enemyCollider = ref _enemyFilter.Get1(j)._collider;
                    ref var enemyUnitComponent = ref _enemyFilter.Get1(j);
                    if (colliders.Contains(enemyCollider))
                    {
                        enemyEntity.Get<EnemyInSight>();
                        ref var enemyList = ref playerEntity.Get<PreTargetEnemyListComponent>();

                        var sqrEnemyDistance =
                            (enemyUnitComponent._rootTransform.position - playerTransform.position)
                            .sqrMagnitude;
                        
                        enemyList.preTargetsUnitComponents[j] = enemyUnitComponent;
                        enemyList.preTargetsSqrDistances[j] = sqrEnemyDistance;
                        
                        Dbg.Error($"enemy component {enemyList.preTargetsUnitComponents[j]}\n" +
                                  $"enemy distance {enemyList.preTargetsSqrDistances[j]}");
                    }
                    else
                    {
                        if (enemyEntity.Has<EnemyInSight>())
                        {
                            enemyEntity.Del<EnemyInSight>();
                        }
                    }
                }

                // for (int j = 0; j < 5; j++)
                // {
                //     ref var enemyEntity = ref _enemyFilter.GetEntity(j);
                //     ref var enemyUnitComponent = ref _enemyFilter.Get1(j);
                //     ref var enemyCollider = ref _enemyFilter.Get1(j)._collider;
                //     
                //     if (colliders.Contains(enemyCollider))
                //     {
                //         ref var enemyList = ref playerEntity.Get<PreTargetEnemyListComponent>();
                //         Dbg.Log("Here");
                //         var sqrEnemyDistance = (enemyUnitComponent._rootTransform.position - playerTransform.position)
                //             .sqrMagnitude;
                //         enemyList.preTargetsUnitComponents[j] = enemyUnitComponent;
                //         enemyList.preTargetsSqrDistances[j] = sqrEnemyDistance;
                //         Dbg.Error($"enemy component {enemyList.preTargetsUnitComponents[j]}\n" +
                //                   $"enemy distance {enemyList.preTargetsSqrDistances[j]}");
                //     }
                // }
            }
        }
    }
}