using EcsBattle.Components;
using Extension;
using Leopotam.Ecs;
using UnityEngine;


namespace EcsBattle.Systems.Attacks
{
    public sealed class TimerForGettingPermissionAttackFromMainWeaponSystem : IEcsRunSystem
    {
        //If Permission not founded => Start Timer, create new Permission, stop Timer
        private EcsFilter<BattleInfoMainWeaponComponent>.Exclude<PermissionForAttackFromMainWeaponAllowedComponent,
            AwaitTimerForOneStrikeFromMainWeaponComponent> _filter;

        public void Run()
        {
            foreach (var i in _filter)
            {
                ref var entity = ref _filter.GetEntity(i);
                ref var weapon = ref _filter.Get1(i);

                if (!entity.Has<TimerTickForGetPermissionToAttackFromMainWeaponComponent>())
                {
                    //timer not found, start timer
                    ref var timer = ref entity.Get<TimerTickForGetPermissionToAttackFromMainWeaponComponent>();
                    timer._currentTime = 0.0f;
                    timer._maxTime = weapon._attackValue.GetAttackSpeed();
                }
                else
                {
                    //timer founded, increment ValueTimer
                    ref var timer = ref entity.Get<TimerTickForGetPermissionToAttackFromMainWeaponComponent>();
                    timer._currentTime += Time.deltaTime;
                    if (timer._currentTime > timer._maxTime)
                    {
                        entity.Del<TimerTickForGetPermissionToAttackFromMainWeaponComponent>();
                        entity.Get<PermissionForAttackFromMainWeaponAllowedComponent>();
                    }
                }
            }
        }
    }
}