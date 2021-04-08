using EcsBattle.Components;
using Leopotam.Ecs;
using UnityEngine;


namespace EcsBattle.Systems.Enemies
{
    public sealed class CalculateStepForUnitsToTargetSystem : IEcsRunSystem
    {
        private EcsFilter<UnitComponent,
            NeedMoveToTargetAndAttackComponent,
            CurrentTargetComponent,
            BattleInfoMainWeaponComponent,
            EnemyComponent> _filter;

        public void Run()
        {
            foreach (var i in _filter)
            {
                ref var entity = ref _filter.GetEntity(i);
                ref var unit = ref _filter.Get1(i);
                ref var target = ref _filter.Get3(i);
                ref var battleInfo = ref _filter.Get4(i);
                var position = unit.rootTransform.position;

                target.sqrDistance = (target.Target.position - position).sqrMagnitude;
                unit.rootTransform.LookAt(target.Target.position, Vector3.up);

                if (target.sqrDistance > Mathf.Pow(battleInfo.Value.AttackDistance, 2))
                {
                    var step = Vector3.ClampMagnitude(target.Target.position - position, 1.0f);
                    entity.Get<NeedStepComponent>().value = step;
                }
                else
                {
                    entity.Get<NeedStartAnimationAttackFromMainWeaponComponent>();
                    entity.Get<NeedAttackFromMainWeaponComponent>();
                    entity.Del<NeedMoveToTargetAndAttackComponent>();
                }
            }
        }
    }
}