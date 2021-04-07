using EcsBattle.Components;
using Extension;
using Leopotam.Ecs;
using UnityEngine;


namespace EcsBattle.Systems.Animation
{
    public sealed class StartAnimationStrikeForUnitsSystem : IEcsRunSystem
    {
        private EcsFilter<NeedStartAnimationAttackComponent, UnitComponent, BattleInfoComponent> _filter;

        public void Run()
        {
            foreach (var i in _filter)
            {
                ref var entity = ref _filter.GetEntity(i);
                ref var animator = ref _filter.Get2(i).animator;
                ref var battle = ref _filter.Get3(i);

                animator.WeaponType = battle.WeaponTypeAnimation;
                animator.AttackType = Random.Range(0, battle.AttackMaxValueAnimation);
                animator.SetTriggerAttack();
                entity.Del<NeedStartAnimationAttackComponent>();
            }
        }
    }
}