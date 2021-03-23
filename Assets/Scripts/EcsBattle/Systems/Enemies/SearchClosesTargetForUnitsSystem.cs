using System.Collections.Generic;
using EcsBattle.Components;
using Extension;
using Leopotam.Ecs;
using UnityEngine;


namespace EcsBattle.Systems.Enemies
{
    public class SearchClosesTargetForUnitsSystem : IEcsRunSystem
    {
        private EcsFilter<EnemyComponent, UnitComponent, TimerTickedForCheckVisionComponent> _filter;

        public void Run()
        {
            foreach (var i in _filter)
            {
                ref var entity = ref _filter.GetEntity(i);
                ref var unit = ref _filter.Get2(i);
                ref var transform = ref _filter.Get2(i).rootTransform;
                ref var vision = ref _filter.Get2(i).vision;
                ref var distanceDetection = ref _filter.Get2(i).vision.distanceDetection;
                ref var reputation = ref _filter.Get2(i).reputation;
                var position = transform.position;

                if (entity.Has<CurrentTargetComponent>())
                {
                    ref var target = ref entity.Get<CurrentTargetComponent>();
                    target.sqrDistance = (target.Target.position - transform.position).sqrMagnitude;
                    if (target.sqrDistance > Mathf.Pow(vision.distanceDetection, 2))
                        entity.Del<CurrentTargetComponent>();
                }
                
                //поиск всех целей
                var colliders = new Collider[unit.vision.maxCountTargets];
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
                        listEnemies.Add(colliders[j].gameObject);
                    }
                }

                //находим ближайшего врага
                if (listEnemies.Count >= 1)
                {
                    var targetGo = listEnemies[0];
                    var distance = Mathf.Infinity;
                    foreach (var target in listEnemies)
                    {
                        var diff = target.transform.position - transform.position;
                        var curDistance = diff.sqrMagnitude;
                        if (curDistance < distance)
                        {
                            targetGo = target;
                            distance = curDistance;
                        }
                    }

                    entity.Get<NeedMoveToTargetAndAttackComponent>();
                    entity.Get<CurrentTargetComponent>().Target = targetGo.transform;
                    entity.Get<CurrentTargetComponent>().sqrDistance = distance;
                }
                else
                {
                    if (entity.Has<CurrentTargetComponent>())
                        entity.Del<CurrentTargetComponent>();
                    entity.Del<NeedMoveToTargetAndAttackComponent>();
                }

                //restart timer
                entity.Del<TimerTickedForCheckVisionComponent>();
            }
        }
    }
}