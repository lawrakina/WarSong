using System.Collections.Generic;
using System.Linq;
using Code.Extension;
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
                ref var playerVision = ref _playerFilter.Get2(i).VisionData;
                ref var playerTransform = ref _playerFilter.Get2(i)._rootTransform;
                // ref var distanceDetection = ref _playerFilter.Get2(i).VisionData.distanceDetection;
                ref var reputation = ref _playerFilter.Get2(i)._reputation;

                var colliders = new Collider[5];
                // var colliders = new Collider[playerVision.maxCountTargets];
                Physics.OverlapSphereNonAlloc(playerTransform.position,15, colliders,
                // Physics.OverlapSphereNonAlloc(playerTransform.position, distanceDetection, colliders,
                    1 << reputation.EnemyLayer);

                foreach (var j in _enemyFilter)
                {
                    ref var enemyEntity = ref _enemyFilter.GetEntity(j);
                    ref var enemyCollider = ref _enemyFilter.Get1(j)._collider;
                    if (colliders.Contains(enemyCollider))
                    {
                        enemyEntity.Get<EnemyInSight>();
                    }
                    else
                    {
                        if (enemyEntity.Has<EnemyInSight>())
                        {
                            enemyEntity.Del<EnemyInSight>();
                        }
                    }
                }
            }
        }
    }
}