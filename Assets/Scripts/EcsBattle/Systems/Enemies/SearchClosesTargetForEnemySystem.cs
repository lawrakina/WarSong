using System.Collections.Generic;
using EcsBattle.Components;
using Extension;
using Leopotam.Ecs;
using UnityEngine;


namespace EcsBattle.Systems.Enemies
{
    public class SearchClosesTargetForEnemySystem : IEcsRunSystem
    {
        private EcsFilter<EnemyComponent, BaseUnitComponent, TimerTickedForVisionComponent> _filter;

        public void Run()
        {
            foreach (var i in _filter)
            {
                ref var unit = ref _filter.Get2(i);
                ref var transform = ref _filter.Get2(i).transform;
                ref var distanceDetection = ref _filter.Get2(i).unitVision.BattleDistance;
                ref var reputation = ref _filter.Get2(i).unitReputation;
                var position = transform.position;

                //поиск всех целей
                var colliders = new Collider[unit.unitVision.MaxCountTargets];
                var countColliders =
                    Physics.OverlapSphereNonAlloc(position, distanceDetection, colliders,1<< reputation.EnemyLayer);
                DebugExtension.DebugWireSphere(position, Color.red, distanceDetection, 1.0f);
                var listEnemies = new List<GameObject>();
                Dbg.Log($"countColliders:{countColliders}");

                //проверка прямой видимости
                for (var j = 0; j < countColliders; j++)
                {
                    if (colliders[j] == null) continue;
                    Dbg.Log($"target:{colliders[j]}, colliders:{colliders.Length}");
                    var hit = new RaycastHit[1];
                    var rayDirection = colliders[j].transform.position - transform.position;
                    var countHit = Physics.RaycastNonAlloc(
                        transform.position + new Vector3(0f, 1.0f, 0f), rayDirection, hit,
                        distanceDetection, 1<<reputation.EnemyLayer);
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

                    _filter.GetEntity(i).Get<CurrentTargetComponent>().Target = targetGo.transform;
                    _filter.GetEntity(i).Get<CurrentTargetComponent>().Distance = distance;
                }
                else
                {
                    _filter.GetEntity(i).Del<CurrentTargetComponent>();
                }

                //restart timer
                _filter.GetEntity(i).Del<TimerTickedForVisionComponent>();
            }
        }
    }
}