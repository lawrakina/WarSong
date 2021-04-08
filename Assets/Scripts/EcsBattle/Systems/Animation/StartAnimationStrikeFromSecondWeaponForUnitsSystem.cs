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
                ref var animator = ref _filter.Get2(i).animator;
                ref var battle = ref _filter.Get3(i);
                
                timer.currentTimeForLag += Time.deltaTime;
                if (timer.currentTimeForLag > timer.maxTimeForLag)
                {
                    animator.WeaponType = battle.WeaponTypeAnimation;
                    animator.AttackType = Random.Range(0, battle.AttackMaxValueAnimation);
                    animator.SetTriggerAttack();
                    entity.Del<NeedStartAnimationAttackFromSecondWeaponComponent>();
                    entity.Get<NeedAttackFromSecondWeaponComponent>();
                }
            }
        }
    }
}