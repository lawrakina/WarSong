using EcsBattle.Components;
using Leopotam.Ecs;
using UnityEngine;


namespace EcsBattle.Systems.Attacks
{
    public sealed class Attack6StartTimerLagBeforeAttackFromMainWeaponSystem : IEcsRunSystem
    {
        private EcsFilter<NeedAttackFromMainWeaponComponent,BattleInfoMainWeaponComponent> _filter;
        public void Run()
        {
            foreach (var i in _filter)
            {
                ref var entity = ref _filter.GetEntity(i);
                ref var battleInfo = ref _filter.Get2(i);

                if (!entity.Has<AwaitTimerForOneStrikeFromMainWeaponComponent>())
                {
                    ref var timer = ref entity.Get<AwaitTimerForOneStrikeFromMainWeaponComponent>();
                    timer.CurrentTime = 0.0f;
                    timer.MaxTime = battleInfo.AttackValue.GetTimeLag();
                }
                else
                {
                    //increment timer
                    ref var timer = ref entity.Get<AwaitTimerForOneStrikeFromMainWeaponComponent>();
                    timer.CurrentTime += Time.deltaTime;
                    if (timer.CurrentTime > timer.MaxTime)
                    {
                        entity.Del<AwaitTimerForOneStrikeFromMainWeaponComponent>();
                        entity.Del<NeedAttackFromMainWeaponComponent>();
                        entity.Get<FinalAttackFromMainWeaponComponent>();
                    }
                }
            }
        }
    }

}