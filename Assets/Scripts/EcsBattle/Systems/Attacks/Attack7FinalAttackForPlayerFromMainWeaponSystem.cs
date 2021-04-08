using Battle;
using EcsBattle.Components;
using Extension;
using Leopotam.Ecs;
using UnityEngine;


namespace EcsBattle.Systems.Attacks
{
    public sealed class Attack7FinalAttackForPlayerFromMainWeaponSystem : IEcsRunSystem
    {
        private EcsFilter<
            FinalAttackFromMainWeaponComponent, 
            PlayerComponent, 
            UnitComponent,
            CurrentTargetComponent, 
            BattleInfoMainWeaponComponent> _filter;
        public void Run()
        {
            foreach (var i in _filter)
            {
                ref var entity = ref _filter.GetEntity(i);
                ref var modelTransform = ref _filter.Get3(i)._modelTransform;
                ref var reputation = ref _filter.Get3(i)._reputation;
                ref var vision = ref _filter.Get3(i)._vision;
                ref var battleInfo = ref _filter.Get5(i);

                var attackPositionCenter =
                    modelTransform.position + modelTransform.forward + vision.offsetHead;
                var maxColliders = 10;
                var hitColliders = new Collider[maxColliders];
                var numColliders = Physics.OverlapSphereNonAlloc(attackPositionCenter,
                    1.0f, hitColliders, 1 << reputation.EnemyLayer);
            
                // DebugExtension.DebugWireSphere(attackPositionCenter, Color.magenta, 1.0f, 2.0f);
                // Dbg.Log($"Attack6Final.Targets:{numColliders}");
            
                for (int index = 0; index < numColliders; index++)
                {
                    var tempObj = hitColliders[index].gameObject.GetComponent<ICollision>();
                    if (tempObj != null)
                    {
                        var collision = new InfoCollision(battleInfo._attackValue.GetAttack(), entity);
                        tempObj.OnCollision(collision);
                    }
                }
                
                entity.Del<FinalAttackFromMainWeaponComponent>();
            }
        }
    }
}