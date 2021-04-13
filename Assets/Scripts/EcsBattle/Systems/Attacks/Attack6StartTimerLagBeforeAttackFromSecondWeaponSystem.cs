using EcsBattle.Components;
using Leopotam.Ecs;
using UnityEngine;


namespace EcsBattle.Systems.Attacks
{
    public sealed class Attack6StartTimerLagBeforeAttackFromSecondWeaponSystem : IEcsRunSystem
    {
        private EcsFilter<NeedAttackFromSecondWeaponComponent,BattleInfoSecondWeaponComponent> _filter;
        public void Run()
        {
            foreach (var i in _filter)
            {
                ref var entity = ref _filter.GetEntity(i);
                ref var battleInfo = ref _filter.Get2(i);

                if (!entity.Has<AwaitTimerForOneStrikeFromSecondWeaponComponent>())
                {
                    ref var timer = ref entity.Get<AwaitTimerForOneStrikeFromSecondWeaponComponent>();
                    timer._currentTime = 0.0f;
                    timer._maxTime = battleInfo._attackValue.GetTimeLag();
                }
                else
                {
                    //increment timer
                    ref var timer = ref entity.Get<AwaitTimerForOneStrikeFromSecondWeaponComponent>();
                    timer._currentTime += Time.deltaTime;
                    if (timer._currentTime > timer._maxTime)
                    {
                        entity.Del<AwaitTimerForOneStrikeFromSecondWeaponComponent>();
                        entity.Del<NeedAttackFromSecondWeaponComponent>();
                        entity.Get<FinalAttackFromSecondWeaponComponent>();
                    }
                }
            }
        }
    }
}