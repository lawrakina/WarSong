using System.Collections.Generic;
using Code.Extension;
using Leopotam.Ecs;
using UnityEngine;

namespace Code.Fight.EcsBattle.Unit.EnemyHealthBars
{
    public class CheckForEnemyInSightSystem : IEcsRunSystem
    {
        private EcsFilter<PlayerComponent, UnitComponent > _playerFilter;
        private EcsFilter<UnitComponent, EnemyComponent> _enemyFilter;

        private List<GameObject> _enemiesInSight;
        
        public void Run()
        {
            foreach (var i in _playerFilter)
            {
                ref var playerEntity = ref _playerFilter.GetEntity(i);
                ref var playerVision = ref _playerFilter.Get2(i)._vision;
                ref var playerTransform = ref _playerFilter.Get2(i)._rootTransform;
                var playerPosition = playerTransform.position;
                ref var distanceDetection = ref _playerFilter.Get2(i)._vision.distanceDetection;
                ref var reputation = ref _playerFilter.Get2(i)._reputation;

                Dbg.Log($"{distanceDetection}");
                var colliders = new Collider[playerVision.maxCountTargets];
                var countColliders =
                    Physics.OverlapSphereNonAlloc(playerPosition, distanceDetection, colliders, 1 << reputation.EnemyLayer);
                DebugExtension.DebugWireSphere(playerPosition, Color.red, distanceDetection, 1.0f);
                var listEnemies = new List<GameObject>();
            }

            foreach (var i in _enemyFilter)
            {
                
            }
            
            
            // foreach (var i in _filter)
            // {
            //      ref var entity = ref _filter.GetEntity(i);
            //      ref var unit = ref _filter.Get1(i);
            //      ref var transform = ref _filter.Get1(i)._rootTransform;
            //      ref var vision = ref _filter.Get1(i)._vision;
            //      ref var distanceDetection = ref _filter.Get1(i)._vision.distanceDetection;
            //      ref var reputation = ref _filter.Get1(i)._reputation;
            //      var position = transform.position;
            //     
            //      var colliders = new Collider[unit._vision.maxCountTargets];
            //      var countColliders =
            //          Physics.OverlapSphereNonAlloc(position, distanceDetection, colliders, 1 << reputation.EnemyLayer);
            //      // DebugExtension.DebugWireSphere(position, Color.red, distanceDetection, 1.0f);
            //      var listEnemies = new List<GameObject>();
            //      // Dbg.Log($"countColliders:{countColliders}");
            //
            //     //проверка прямой видимости
            //      for (var j = 0; j < countColliders; j++)
            //      {
            //          if (!VectorExtension.CheckBlocked(transform, colliders[j].transform, offset: vision.offsetHead))
            //          {
            //              entity.Get<EnemyInSight>();
            //          }
            //          else
            //          {
            //              if (entity.Has<EnemyInSight>())
            //              {
            //                  entity.Del<EnemyInSight>();
            //              }
            //          }
            //      }
            }
        }
    }

