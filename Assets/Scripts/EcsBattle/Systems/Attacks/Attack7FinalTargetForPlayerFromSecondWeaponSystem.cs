using Battle;
using EcsBattle.Components;
using Leopotam.Ecs;
using UnityEngine;


namespace EcsBattle.Systems.Attacks
{
    public sealed class Attack7FinalTargetForPlayerFromSecondWeaponSystem : IEcsRunSystem
    {
        private EcsFilter<
            FinalAttackFromSecondWeaponComponent, 
            PlayerComponent, 
            CurrentTargetComponent, 
            BattleInfoSecondWeaponComponent> _filter;
        public void Run()
        {
            foreach (var i in _filter)
            {
                ref var entity = ref _filter.GetEntity(i);
                ref var target = ref _filter.Get3(i);
                ref var battleInfo = ref _filter.Get4(i);

                var targetCollision = target._baseUnitView.Transform.GetComponent<ICollision>();
                var collision =
                    new InfoCollision(battleInfo._attackValue.GetAttack() * battleInfo._powerFactor, entity);
                targetCollision?.OnCollision(collision);

                entity.Del<FinalAttackFromSecondWeaponComponent>();
            }
        }
    }
}