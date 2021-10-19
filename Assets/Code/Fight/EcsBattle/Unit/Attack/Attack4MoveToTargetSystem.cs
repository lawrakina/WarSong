using Code.Fight.EcsBattle.Input;
using Leopotam.Ecs;
using UnityEngine;


namespace Code.Fight.EcsBattle.Unit.Attack
{
    public sealed class Attack4MoveToTargetSystem : IEcsRunSystem
    {
        private EcsFilter<PlayerComponent,
            DirectionMovementComponent,
            NeedMoveToTargetAndAttackComponent,
            CurrentTargetComponent,
            BattleInfoMainWeaponComponent,
            UnitComponent> _player;

        public void Run()
        {
            foreach (var i in _player)
            {
                ref var entity = ref _player.GetEntity(i);
                ref var unit = ref _player.Get1(i);
                ref var target = ref _player.Get4(i);
                ref var direction = ref _player.Get2(i);
                ref var battleInfo = ref _player.Get5(i);
                var rootPosition = _player.Get6(i)._rootTransform.position;

                target._sqrDistance = (target._baseUnitView.Transform.position - rootPosition).sqrMagnitude;

                if (target._sqrDistance > Mathf.Pow(battleInfo._attackDistance, 2))
                {
                    var offset = target._baseUnitView.Transform.position - rootPosition;
                    direction._value.position = rootPosition + Vector3.ClampMagnitude(offset,1.0f);
                    entity.Get<NeedStepComponent>()._value = rootPosition - direction._value.position;
                    //for animator
                    direction._value.localPosition = new Vector3(0.0f, 0.0f, 1.0f);
                }
                else
                {
                    entity.Get<NeedStartAnimationAttackFromMainWeaponComponent>();
                    entity.Get<NeedAttackFromMainWeaponComponent>();
                    if (entity.Has<PermissionForAttackFromSecondWeaponAllowedComponent>())
                    {
                        entity.Del<PermissionForAttackFromSecondWeaponAllowedComponent>();
                        entity.Get<NeedStartAnimationAttackFromSecondWeaponComponent>()._currentTimeForLag = 0.0f;
                        entity.Get<NeedStartAnimationAttackFromSecondWeaponComponent>()._maxTimeForLag = entity.Get<BattleInfoSecondWeaponComponent>()._lagBeforeAttack;
                    }
                    if (entity.Has<StartKnockbackAbilityComponent>() && entity.Has<PermissionForKnockbackAbilityAllowedComponent>()) {
                        entity.Del<PermissionForKnockbackAbilityAllowedComponent>();
                        entity.Del<StartKnockbackAbilityComponent>();
                        entity.Get<NeedKnockbackAbilityComponent>();
                    }
                    entity.Del<NeedMoveToTargetAndAttackComponent>();
                }
            }
        }
    }

}