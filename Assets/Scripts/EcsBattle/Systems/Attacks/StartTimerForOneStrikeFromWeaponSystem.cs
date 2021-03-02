using EcsBattle.Components;
using Leopotam.Ecs;
using UnityEngine;


namespace EcsBattle.Systems.Attacks
{
    public sealed class StartTimerForOneStrikeFromWeaponSystem : IEcsRunSystem
    {
        private EcsFilter<NeedAttackComponent, BaseUnitComponent, BattleInfoComponent>
            .Exclude<CurrentTargetComponent> _filter;

        public void Run()
        {
            foreach (var i in _filter)
            {
                ref var unit = ref _filter.Get2(i);
                ref var weapon = ref _filter.Get3(i);
                ref var entity = ref _filter.GetEntity(i);

                unit.animator.AttackType = Random.Range(0, 3);
                unit.animator.SetTriggerAttack();

                entity.Get<AwaitTimerForOneStrikeComponent>().CurrentTime = 0.0f;
                entity.Get<AwaitTimerForOneStrikeComponent>().MaxTime = weapon.AttackValue.GetTimeLag();

                entity.Del<NeedAttackComponent>();
            }
        }
    }
}