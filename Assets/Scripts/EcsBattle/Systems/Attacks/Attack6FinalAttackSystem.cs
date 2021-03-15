using Battle;
using EcsBattle.Components;
using Extension;
using Leopotam.Ecs;
using UnityEngine;


namespace EcsBattle.Systems.Attacks
{
    public sealed class Attack6FinalAttackSystem : IEcsRunSystem
    {
        private EcsFilter<
            FinalAttackComponent, 
            PlayerComponent, 
            CurrentTargetComponent, 
            BattleInfoComponent> _filter;
        public void Run()
        {
            foreach (var i in _filter)
            {
                ref var entity = ref _filter.GetEntity(i);
                // ref var rootTransform = ref _filter.Get3(i).rootTransform;
                ref var modelTransform = ref _filter.Get2(i).modelTransform;
                // ref var target = ref _filter.Get4(i);
                ref var reputation = ref _filter.Get2(i).unitReputation;
                ref var vision = ref _filter.Get2(i).unitVision;
                ref var battleInfo = ref _filter.Get4(i);

                var attackPositionCenter =
                    modelTransform.position + modelTransform.forward + vision.OffsetHead;
                var maxColliders = 10;
                var hitColliders = new Collider[maxColliders];
                var numColliders = Physics.OverlapSphereNonAlloc(attackPositionCenter,
                    1.0f, hitColliders, 1 << reputation.EnemyLayer);
            
                DebugExtension.DebugWireSphere(attackPositionCenter, Color.magenta, 1.0f, 2.0f);
                Dbg.Log($"numColliders:{numColliders}");
            
                for (int index = 0; index < numColliders; index++)
                {
                    var tempObj = hitColliders[index].gameObject.GetComponent<ICollision>();
                    if (tempObj != null)
                    {
                        var collision = new InfoCollision(battleInfo.AttackValue.GetAttack(), entity);
                        tempObj.OnCollision(collision);
                    }
                }
                
                entity.Del<FinalAttackComponent>();
            }
        }
    }
}