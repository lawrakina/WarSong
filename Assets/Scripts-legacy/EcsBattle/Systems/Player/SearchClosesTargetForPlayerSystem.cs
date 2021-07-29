using System.Collections.Generic;
using EcsBattle.Components;
using Leopotam.Ecs;
using Unit.Player;
using UnityEngine;


namespace EcsBattle.Systems.PlayerVision
{
    public class SearchClosesTargetForPlayerSystem : IEcsRunSystem
    {
        // private EcsFilter<PlayerComponent, BaseUnitComponent,
        //     TimerTickedForVisionComponent> _filter;
        //
        // private IPlayerView _player;

        public void Run()
        {
        //     foreach (var index in _filter)
        //     {
        //         ref var entity = ref _filter.GetEntity(index);
        //         ref var transform = ref _filter.Get2(index).transform;
        //         ref var distanceDetection = ref _filter.Get2(index).unitVision.BattleDistance;
        //         ref var reputation = ref _filter.Get2(index).unitReputation;
        //     
        //         //поиск всех целей
        //         Collider[] colliders = new Collider[_player.UnitVision.MaxCountTargets];
        //         var countColliders =
        //             Physics.OverlapSphereNonAlloc(transform.position, distanceDetection, colliders,
        //                 1 << reputation.EnemyLayer);
        //         // DebugExtension.DebugWireSphere(transform.position, Color.green, distanceDetection, 1.0f);
        //         var listEnemies = new List<GameObject>();
        //         // Dbg.Log($"countColliders:{countColliders}");
        //     
        //         //проверка прямой видимости
        //         for (var i = 0; i < countColliders; i++)
        //         {
        //             if (colliders[i] == null) continue;
        //             Dbg.Log($"Player.target:{colliders[i]}, colliders:{colliders.Length}");
        //             var hit = new RaycastHit[1];
        //             var rayDirection = colliders[i].transform.position - transform.position;
        //             var countHit = Physics.RaycastNonAlloc(
        //                 transform.position + new Vector3(0f, 1.0f, 0f), rayDirection, hit,
        //                 distanceDetection, 1 << reputation.EnemyLayer);
        //             // DebugExtension.DebugArrow(transform.position + new Vector3(0f, 1.0f, 0f),
        //             // colliders[i].transform.position - transform.position, Color.red, distanceDetection);
        //             if (countHit > 0)
        //             {
        //                 // Dbg.Log($"Visible Target:colliders[i].gameObject");
        //                 listEnemies.Add(colliders[i].gameObject);
        //             }
        //         }
        //     
        //         //находим ближайшего врага
        //         if (listEnemies.Count >= 1)
        //         {
        //             var targetGo = listEnemies[0];
        //             var distance = Mathf.Infinity;
        //             foreach (var target in listEnemies)
        //             {
        //                 var diff = target.transform.position - transform.position;
        //                 var curDistance = diff.sqrMagnitude;
        //                 if (curDistance < distance)
        //                 {
        //                     targetGo = target;
        //                     distance = curDistance;
        //                 }
        //             }
        //             
        //             entity.Get<CurrentTargetComponent>().Target = targetGo.transform;
        //             entity.Get<CurrentTargetComponent>().sqrDistance = distance;
        //         }
        //         else
        //         {
        //             entity.Del<CurrentTargetComponent>();
        //         }
        //     
        //         //restart timer
        //         entity.Del<TimerTickedForVisionComponent>();
        //         entity.Get<AwaitTimerForVisionComponent>().Value = 0.0f;
        //     }
        }
    }
}