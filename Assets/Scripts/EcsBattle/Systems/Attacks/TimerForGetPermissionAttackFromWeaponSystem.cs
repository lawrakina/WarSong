using EcsBattle.Components;
using Leopotam.Ecs;
using UnityEngine;


namespace EcsBattle.Systems.Attacks
{
    public sealed class TimerForGetPermissionAttackFromWeaponSystem : IEcsRunSystem
    {
        //If Permission not founded => Start Timer, create new Permission, stop Timer
        private EcsFilter<BattleInfoComponent>.Exclude<PermissionForAttackAllowedComponent> _filter;
        public void Run()
        {
            foreach (var i in _filter)
            {
                ref var entity = ref _filter.GetEntity(i);
                ref var weapon = ref _filter.Get1(i);

                if (!entity.Has<TimerTickForGetPermissionToAttackComponent>())
                {//timer not found, start timer
                    ref var timer = ref entity.Get<TimerTickForGetPermissionToAttackComponent>();
                    timer.CurrentTime = 0.0f;
                    timer.MaxTime = weapon.AttackValue.GetAttackSpeed();
                }
                else
                {//timer founded, increment ValueTimer
                    ref var timer = ref entity.Get<TimerTickForGetPermissionToAttackComponent>();
                    timer.CurrentTime += Time.deltaTime;
                    if (timer.CurrentTime > timer.MaxTime)
                    {
                        entity.Del<TimerTickForGetPermissionToAttackComponent>();
                        entity.Get<PermissionForAttackAllowedComponent>();
                    }
                }
            }
        }
    }
}