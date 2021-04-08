using EcsBattle.Components;
using EcsBattle.Systems.Attacks;
using Leopotam.Ecs;
using UnityEngine;


namespace EcsBattle.Systems.Animation
{
    public sealed class StartAnimationStrikeFromSecondWeaponForUnitsSystem : IEcsRunSystem
    {
        private EcsFilter<NeedStartAnimationAttackFromSecondWeaponComponent, UnitComponent,
            BattleInfoSecondWeaponComponent> _filter;

        public void Run()
        {
            foreach (var i in _filter)
            {
                ref var entity = ref _filter.GetEntity(i);
                ref var timer = ref _filter.Get1(i);
                ref var animator = ref _filter.Get2(i)._animator;
                ref var battle = ref _filter.Get3(i);
                
                timer._currentTimeForLag += Time.deltaTime;
                if (timer._currentTimeForLag > timer._maxTimeForLag)
                {
                    animator.WeaponType = battle._weaponTypeAnimation;
                    animator.AttackType = Random.Range(0, battle._attackMaxValueAnimation);
                    animator.SetTriggerAttack();
                    entity.Del<NeedStartAnimationAttackFromSecondWeaponComponent>();
                    entity.Get<NeedAttackFromSecondWeaponComponent>();
                }
            }
        }
    }
}