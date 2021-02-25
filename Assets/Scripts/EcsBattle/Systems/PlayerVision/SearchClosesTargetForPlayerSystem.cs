using System.Collections.Generic;
using EcsBattle.Components;
using Extension;
using Google.Apis.Util;
using Leopotam.Ecs;
using Unit.Player;
using UnityEngine;


namespace EcsBattle.Systems.PlayerVision
{
    public class SearchClosesTargetForPlayerSystem : IEcsRunSystem
    {
        private EcsFilter<PlayerComponent,
            TransformComponent,
            TimerTickedForVisionComponent,
            DetectionDistanceEnemyComponent,
            LayerMaskEnemiesComponent> _filter;

        private IPlayerView _player;

        public void Run()
        {
            foreach (var index in _filter)
            {
                ref var transform = ref _filter.Get2(index).Value;
                ref var distanceDetection = ref _filter.Get4(index).Value;
                ref var layerMaskEnemies = ref _filter.Get5(index).Value;

                //поиск всех целей
                Collider[] colliders = new Collider[_player.UnitVision.MaxCountTaggets];
                var countColliders =
                    Physics.OverlapSphereNonAlloc(transform.position, distanceDetection, colliders, layerMaskEnemies);
                // DebugExtension.DebugWireSphere(transform.position, Color.green, distanceDetection);
                var listEnemies = new List<GameObject>();
                // Dbg.Log($"countColliders:{countColliders}");

                //проверка прямой видимости
                for (var i = 0; i <= countColliders; i++)
                {
                    if (colliders[i] == null) continue;
                    Dbg.Log($"target:{colliders[i]}, colliders:{colliders.Length}");
                    var hit = new RaycastHit[1];
                    var rayDirection = colliders[i].transform.position - transform.position;
                    var countHit = Physics.RaycastNonAlloc(
                        transform.position + new Vector3(0f, 1.0f, 0f), rayDirection, hit,
                        distanceDetection, layerMaskEnemies);
                    // DebugExtension.DebugArrow(transform.position + new Vector3(0f, 1.0f, 0f),
                        // colliders[i].transform.position - transform.position, Color.red, distanceDetection);
                    if (countHit > 0)
                    {
                        // Dbg.Log($"Visible Target:colliders[i].gameObject");
                        listEnemies.Add(colliders[i].gameObject);
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

                    _filter.GetEntity(index).Get<CurrentTargetComponent>().Target = targetGo.transform;
                    _filter.GetEntity(index).Get<CurrentTargetComponent>().Distance = distance;
                }

                //restart timer
                _filter.GetEntity(index).Del<TimerTickedForVisionComponent>();
                _filter.GetEntity(index).Get<AwaitTimerForVisionComponent>().Value = 0.0f;
            }
        }
    }
}