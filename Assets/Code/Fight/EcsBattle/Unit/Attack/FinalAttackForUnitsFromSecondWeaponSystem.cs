using Code.Unit;
using Leopotam.Ecs;
using UnityEngine;


namespace Code.Fight.EcsBattle.Unit.Attack
{
    public class FinalAttackForUnitsFromSecondWeaponSystem : IEcsRunSystem
    {
        private EcsFilter<
            FinalAttackFromSecondWeaponComponent,
            UnitComponent,
            CurrentTargetComponent,
            BattleInfoSecondWeaponComponent> _filter;

        public void Run()
        {
            foreach (var i in _filter)
            {
                ref var entity = ref _filter.GetEntity(i);
                ref var transform = ref _filter.Get2(i)._rootTransform;
                ref var reputation = ref _filter.Get2(i)._reputation;
                ref var vision = ref _filter.Get2(i)._vision;
                ref var battleInfo = ref _filter.Get4(i);

                var attackPositionCenter = transform.position + transform.forward + vision.offsetHead;
                var maxColliders = 10;
                var hitColliders = new Collider[maxColliders];
                var numColliders = Physics.OverlapSphereNonAlloc(attackPositionCenter,
                    1.0f, hitColliders, 1 << reputation.EnemyLayer);

                // DebugExtension.DebugWireSphere(attackPositionCenter, Color.black, 1.0f, 2.0f);
                // Dbg.Log($"Attack6Final.Targets:{numColliders}");

                for (int index = 0; index < numColliders; index++)
                {
                    var tempObj = hitColliders[index].gameObject.GetComponent<ICollision>();
                    if (tempObj != null)
                    {
                        var collision =
                            new InfoCollision(battleInfo._attackValue.GetAttackAvarage() * battleInfo._powerFactor, entity);
                        tempObj.OnCollision(collision);
                    }
                }

                entity.Del<FinalAttackFromMainWeaponComponent>();
            }
        }
    }
}