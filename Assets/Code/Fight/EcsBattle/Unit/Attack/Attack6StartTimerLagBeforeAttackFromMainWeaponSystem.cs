using Leopotam.Ecs;
using UnityEngine;


namespace Code.Fight.EcsBattle.Unit.Attack
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
                    timer._currentTime = 0.0f;
                    timer._maxTime = battleInfo._attackValue.GetTimeLag();
                }
                else
                {
                    //increment timer
                    ref var timer = ref entity.Get<AwaitTimerForOneStrikeFromMainWeaponComponent>();
                    timer._currentTime += Time.deltaTime;
                    if (timer._currentTime > timer._maxTime)
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