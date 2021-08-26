using System.Collections.Generic;
using Code.Extension;
using Leopotam.Ecs;
using UnityEngine;

namespace Code.Fight.EcsBattle.Unit.EnemyHealthBars
{
    public class CheckForEnemyInSightSystem : IEcsRunSystem
    {
        private EcsFilter<EnemyComponent, UnitComponent> _filter;
        
        public void Run()
        {
            foreach (var i in _filter)
            {
                ref var entity = ref _filter.GetEntity(i);
                ref var unit = ref _filter.Get2(i);
                ref var transform = ref _filter.Get2(i)._rootTransform;
                ref var vision = ref _filter.Get2(i)._vision;
                ref var distanceDetection = ref _filter.Get2(i)._vision.distanceDetection;
                ref var reputation = ref _filter.Get2(i)._reputation;
                var position = transform.position;
                
                var colliders = new Collider[unit._vision.maxCountTargets];
                var countColliders =
                    Physics.OverlapSphereNonAlloc(position, distanceDetection, colliders, 1 << reputation.EnemyLayer);
                // DebugExtension.DebugWireSphere(position, Color.red, distanceDetection, 1.0f);
                var listEnemies = new List<GameObject>();
                // Dbg.Log($"countColliders:{countColliders}");

                //проверка прямой видимости
                for (var j = 0; j < countColliders; j++)
                {
                    if (!VectorExtension.CheckBlocked(transform, colliders[j].transform, offset: vision.offsetHead))
                    {
                        entity.Get<EnemyInSight>();
                    }
                    else
                    {
                        if (entity.Has<EnemyInSight>())
                        {
                            entity.Del<EnemyInSight>();
                        }
                    }
                }
            }
        }
    }
}
