using EcsBattle.Components;
using Leopotam.Ecs;
using UnityEngine;


namespace EcsBattle.Systems.Attacks
{
    public sealed class Attack4MoveToTargetSystem : IEcsRunSystem
    {
        private EcsFilter<PlayerComponent,
            DirectionMovementComponent,
            NeedMoveToTargetAndAttackComponent,
            CurrentTargetComponent,
            BattleInfoComponent,
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
                var rootPosition = _player.Get6(i).rootTransform.position;

                target.sqrDistance = (target.Target.position - rootPosition).sqrMagnitude;

                if (target.sqrDistance > Mathf.Pow(battleInfo.Value.AttackDistance, 2))
                {
                    var offset = target.Target.position - rootPosition;
                    direction.value.position = rootPosition + Vector3.ClampMagnitude(offset,1.0f);
                    entity.Get<NeedStepComponent>().value = rootPosition - direction.value.position;
                    //for animator
                    direction.value.localPosition = new Vector3(0.0f, 0.0f, 1.0f);
                }
                else
                {
                    entity.Get<NeedStartAnimationComponent>();
                    entity.Get<NeedAttackComponent>();
                    entity.Del<NeedMoveToTargetAndAttackComponent>();
                }
            }
        }
    }
}