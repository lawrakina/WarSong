using Battle;
using EcsBattle.Components;
using Extension;
using Leopotam.Ecs;
using UnityEngine;


namespace EcsBattle.Systems.Attacks
{
    public sealed class TimerForStartAnimationFromWeaponSystem : IEcsRunSystem
    {
        // //If needAttack and have permissionForAttack to start animation and start timerForAttack
        // private EcsFilter<StartAttackComponent,
        //     PermissionForAttackAllowedComponent,
        //     BattleInfoComponent> _filter;

        public void Run()
        {
            // foreach (var i in _filter)
            // {
            //     ref var entity = ref _filter.GetEntity(i);
            //     ref var unit = ref _filter.Get4(i);
            //     ref var weapon = ref _filter.Get3(i);
            //
            //     //if Timer not founded => start animation and create timer
            //     if (!entity.Has<AwaitTimerForOneStrikeComponent>())
            //     {
            //         //LookAt target
            //         if (entity.Has<CurrentTargetComponent>())
            //         {
            //             //save default rotation
            //             if (!entity.Has<SavedRotationValueComponent>())
            //                 entity.Get<SavedRotationValueComponent>().value = unit.transform.rotation;
            //
            //             ref var target = ref entity.Get<CurrentTargetComponent>();
            //             if(target.sqrDistance < Mathf.Pow(weapon.Value.AttackDistance,2) + weapon.Value.AttackDistanceOffset)
            //                 unit.transform.LookAt(target.Target, Vector3.up);
            //         }
            //
            //         //start animation
            //         unit.animator.AttackType = Random.Range(0, 3);
            //         unit.animator.SetTriggerAttack();
            //         //start timer lag attack
            //         ref var timer = ref entity.Get<AwaitTimerForOneStrikeComponent>();
            //         timer.CurrentTime = 0.0f;
            //         timer.MaxTime = weapon.AttackValue.GetTimeLag();
            //     }
            //     else
            //     {
            //         //increment timer
            //         ref var timer = ref entity.Get<AwaitTimerForOneStrikeComponent>();
            //         timer.CurrentTime += Time.deltaTime;
            //         if (timer.CurrentTime > timer.MaxTime)
            //         {
            //             //if timer complete => Attack
            //             var attackPositionCenter =
            //                 unit.transform.position + unit.transform.forward + unit.unitVision.OffsetHead;
            //             var maxColliders = 10;
            //             var hitColliders = new Collider[maxColliders];
            //             var numColliders = Physics.OverlapSphereNonAlloc(attackPositionCenter,
            //                 1.0f, hitColliders, 1 << unit.unitReputation.EnemyLayer);
            //
            //             DebugExtension.DebugWireSphere(attackPositionCenter, Color.magenta, 1.0f, 2.0f);
            //             Dbg.Log($"numColliders:{numColliders}");
            //
            //             for (int index = 0; index < numColliders; index++)
            //             {
            //                 var tempObj = hitColliders[index].gameObject.GetComponent<ICollision>();
            //                 if (tempObj != null)
            //                 {
            //                     var collision = new InfoCollision(weapon.AttackValue.GetAttack(), entity);
            //                     tempObj.OnCollision(collision);
            //                 }
            //             }
            //
            //             //del command NeedAttack
            //             entity.Del<StartAttackComponent>();
            //             //del timer
            //             entity.Del<AwaitTimerForOneStrikeComponent>();
            //             //del permission for attack
            //             entity.Del<PermissionForAttackAllowedComponent>();
            //         }
            //     }
            // }
        }
    }

    public sealed class RestoreSavedRotationInUnitSystem : IEcsRunSystem
    {
        // private EcsFilter<BaseUnitComponent, SavedRotationValueComponent>
            // .Exclude<CurrentTargetComponent> _units;
        // private EcsFilter<NeedLerpPositionCameraFollowingToTargetComponent> _lerp;

        public void Run()
        {
            // foreach (var u in _units)
            // {
            //     ref var entity = ref _units.GetEntity(u);
            //     ref var unit = ref _units.Get1(u);
            //     ref var rotation = ref _units.Get2(u);
            //     // foreach (var l in _lerp)
            //     {
            //         //if camera contains component _lerp => need recover old rotation in unit
            //         // ref var entityLerp = ref _lerp.GetEntity(l);
            //         unit.transform.rotation = rotation.value;
            //         entity.Del<SavedRotationValueComponent>();
            //     }
            // }
        }
    }

    public struct SavedRotationValueComponent
    {
        public Quaternion value;
    }
}