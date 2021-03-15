using EcsBattle.Components;
using Leopotam.Ecs;
using UnityEngine;


namespace EcsBattle.Systems.Attacks
{
    public sealed class Attack5StartTimerLagBeforeAttack : IEcsRunSystem
    {
        private EcsFilter<NeedAttackComponent,BattleInfoComponent> _filter;
        public void Run()
        {
            foreach (var i in _filter)
            {
                ref var entity = ref _filter.GetEntity(i);
                ref var battleInfo = ref _filter.Get2(i);

                if (!entity.Has<AwaitTimerForOneStrikeComponent>())
                {
                    ref var timer = ref entity.Get<AwaitTimerForOneStrikeComponent>();
                    timer.CurrentTime = 0.0f;
                    timer.MaxTime = battleInfo.AttackValue.GetTimeLag();
                }
                else
                {
                    //increment timer
                    ref var timer = ref entity.Get<AwaitTimerForOneStrikeComponent>();
                    timer.CurrentTime += Time.deltaTime;
                    if (timer.CurrentTime > timer.MaxTime)
                    {
                        entity.Del<AwaitTimerForOneStrikeComponent>();
                        entity.Del<NeedAttackComponent>();
                        entity.Get<FinalAttackComponent>();
                    }
                }
            }
        }
    }
}