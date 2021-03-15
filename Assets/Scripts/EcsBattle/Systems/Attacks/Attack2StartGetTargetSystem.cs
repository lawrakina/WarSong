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
            PlayerComponent> _filter;

        public void Run()
        {
            foreach (var i in _filter)
            {
                ref var entity = ref _filter.GetEntity(i);
                ref var transform = ref _filter.Get2(i).rootTransform;
                ref var vision = ref _filter.Get2(i).unitVision;
                ref var reputation = ref _filter.Get2(i).unitReputation;

                //поиск всех целей
                var colliders = new Collider[vision.MaxCountTargets];
                var countColliders =
                    Physics.OverlapSphereNonAlloc(transform.position, vision.BattleDistance, colliders,
                        1 << reputation.EnemyLayer);
                // DebugExtension.DebugWireSphere(transform.position, Color.green, distanceDetection, 1.0f);
                var listEnemies = new List<GameObject>();
                // Dbg.Log($"countColliders:{countColliders}");

                //проверка прямой видимости
                for (var j = 0; j < countColliders; j++)
                {
                    if (colliders[j] == null) continue;
                    Dbg.Log($"Player.target:{colliders[j]}, colliders:{colliders.Length}");
                    var hit = new RaycastHit[1];
                    var rayDirection = colliders[j].transform.position - transform.position;
                    var countHit = Physics.RaycastNonAlloc(
                        transform.position + new Vector3(0f, 1.0f, 0f), rayDirection, hit,
                        vision.BattleDistance, 1 << reputation.EnemyLayer);
                    // DebugExtension.DebugArrow(transform.position + new Vector3(0f, 1.0f, 0f),
                    // colliders[i].transform.position - transform.position, Color.red, distanceDetection);
                    if (countHit > 0)
                    {
                        // Dbg.Log($"Visible Target:colliders[i].gameObject");
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

                    entity.Get<CurrentTargetComponent>().Target = targetGo.transform;
                    entity.Get<CurrentTargetComponent>().sqrDistance = distance;
                }
                else
                {
                    entity.Get<CurrentTargetComponent>().Target = null;
                }
                
                entity.Del<NeedFindTargetComponent>();
            }
        }
    }
}