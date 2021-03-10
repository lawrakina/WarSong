using Battle;
using EcsBattle.Components;
using Extension;
using Leopotam.Ecs;
using UnityEngine;


namespace EcsBattle.Systems.Attacks
{
    public sealed class TimerForStartAnimationFromWeaponSystem : IEcsRunSystem
    {
        //If needAttack and have permissionForAttack to start animation and start timerForAttack
        private EcsFilter<NeedAttackComponent, 
                PermissionForAttackAllowedComponent,
                BattleInfoComponent,
                BaseUnitComponent> _filter;

        public void Run()
        {
            foreach (var i in _filter)
            {
                ref var entity = ref _filter.GetEntity(i);
                ref var unit = ref _filter.Get4(i);
                ref var weapon = ref _filter.Get3(i);

                //if Timer not founded => start animation and create timer
                if (!entity.Has<AwaitTimerForOneStrikeComponent>())
                {
                    //start animation
                    unit.animator.AttackType = Random.Range(0, 3);
                    unit.animator.SetTriggerAttack();
                    //start timer lag attack
                    ref var timer = ref entity.Get<AwaitTimerForOneStrikeComponent>();
                    timer.CurrentTime = 0.0f;
                    timer.MaxTime = weapon.AttackValue.GetTimeLag();
                }
                else
                {
                    //increment timer
                    ref var timer = ref entity.Get<AwaitTimerForOneStrikeComponent>();
                    timer.CurrentTime += Time.deltaTime;
                    if (timer.CurrentTime > timer.MaxTime)
                    {
                        //if timer complete => Attack
                        var attackPositionCenter =
                            unit.transform.position + unit.transform.forward + unit.unitVision.OffsetHead;
                        var maxColliders = 10;
                        var hitColliders = new Collider[maxColliders];
                        var numColliders = Physics.OverlapSphereNonAlloc(attackPositionCenter,
                            1.0f, hitColliders, 1 << unit.unitReputation.EnemyLayer);

                        DebugExtension.DebugWireSphere(attackPositionCenter, Color.magenta, 1.0f, 2.0f);
                        Dbg.Log($"numColliders:{numColliders}");

                        for (int index = 0; index < numColliders; index++)
                        {
                            var tempObj = hitColliders[index].gameObject.GetComponent<ICollision>();
                            if (tempObj != null)
                            {
                                var collision = new InfoCollision(weapon.AttackValue.GetAttack(), entity);
                                tempObj.OnCollision(collision);
                            }
                        }

                        //del command NeedAttack
                        entity.Del<NeedAttackComponent>();
                        //del timer
                        entity.Del<AwaitTimerForOneStrikeComponent>();
                        //del permission for attack
                        entity.Del<PermissionForAttackAllowedComponent>();
                    }
                }
            }
        }
    }
}