using EcsBattle.Components;
using Extension;
using Leopotam.Ecs;
using UnityEngine;


namespace EcsBattle.Systems.Animation
{
    public sealed class StartAnimationStrikeFromMainWeaponForUnitsSystem : IEcsRunSystem
    {
        private EcsFilter<NeedStartAnimationAttackFromMainWeaponComponent, UnitComponent, BattleInfoMainWeaponComponent>
            _filter;

        public void Run()
        {
            foreach (var i in _filter)
            {
                ref var entity = ref _filter.GetEntity(i);
                ref var animator = ref _filter.Get2(i)._animator;
                ref var battle = ref _filter.Get3(i);

                animator.WeaponType = battle._weaponTypeAnimation;
                animator.AttackType = Random.Range(0, battle._attackMaxValueAnimation);
                animator.SetTriggerAttack();
                entity.Del<NeedStartAnimationAttackFromMainWeaponComponent>();
            }
        }
    }
}