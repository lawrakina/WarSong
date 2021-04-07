using System.Collections.Generic;
using EcsBattle.Components;
using Extension;
using Leopotam.Ecs;
using UnityEngine;


namespace EcsBattle.Systems.Attacks
{
    public class Attack2StartGetTargetSystem : IEcsRunSystem
    {
        private EcsFilter<
            NeedFindTargetComponent,
            PlayerComponent,
            UnitComponent
            // ,BattleInfoComponent
        > _filter;

        public void Run()
        {
            foreach (var i in _filter)
            {
                ref var entity = ref _filter.GetEntity(i);
                ref var transform = ref _filter.Get3(i).rootTransform;
                ref var vision = ref _filter.Get3(i).vision;
                ref var reputation = ref _filter.Get3(i).reputation;
                // ref var battleInfo = ref _filter.Get3(i);

                //поиск всех целей
                var colliders = new Collider[vision.maxCountTargets];
                var countColliders =
                    Physics.OverlapSphereNonAlloc(transform.position, vision.distanceDetection, colliders,
                        1 << reputation.EnemyLayer);
                // DebugExtension.DebugWireSphere(transform.position, Color.green, distanceDetection, 1.0f);
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
                    // Dbg.Log($"listEnemies.Count:{listEnemies.Count}");
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
                            // Dbg.Log($"target:{target},distance:{distance}");
                        }
                    }

                    entity.Get<CurrentTargetComponent>().Target = targetGo.transform;
                    entity.Get<CurrentTargetComponent>().sqrDistance = distance;

                    entity.Get<NeedLookAtTargetComponent>();
                    entity.Get<NeedMoveToTargetAndAttackComponent>();
                }
                else
                {
                    entity.Get<NeedStartAnimationAttackComponent>();
                    entity.Del<CurrentTargetComponent>();
                }

                entity.Del<NeedFindTargetComponent>();
            }
        }
    }
}