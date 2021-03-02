using Battle;
using EcsBattle.Components;
using Extension;
using Leopotam.Ecs;
using UnityEngine;


namespace EcsBattle.Systems.Attacks
{
    public sealed class OneStrikeFromWeaponExcludeTargetSystem : IEcsRunSystem
    {
        private EcsFilter<AwaitTimerForOneStrikeComponent, BaseUnitComponent, BattleInfoComponent>
            .Exclude<CurrentTargetComponent> _filter;

        public void Run()
        {
            foreach (var i in _filter)
            {
                ref var timer = ref _filter.Get1(i);
                ref var unit = ref _filter.Get2(i);
                ref var weapon = ref _filter.Get3(i);

                timer.CurrentTime += Time.deltaTime;
                if (timer.CurrentTime < timer.MaxTime) continue;

                var attackPositionCenter = unit.transform.position + unit.transform.forward + unit.offsetHead;
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
                        var collision = new InfoCollision(weapon.AttackValue.GetAttack());
                        tempObj.OnCollision(collision);
                    }
                }

                _filter.GetEntity(i).Del<AwaitTimerForOneStrikeComponent>();
            }
        }
    }
}