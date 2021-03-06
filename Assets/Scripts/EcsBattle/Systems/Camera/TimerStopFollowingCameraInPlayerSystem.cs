﻿using EcsBattle.Components;
using EcsBattle.Systems.Input;
using Leopotam.Ecs;
using UnityEngine;


namespace EcsBattle.Systems.Camera
{
    public sealed class TimerStopFollowingCameraInPlayerSystem : IEcsRunSystem
    {
        private EcsFilter<TimerStopFollowingInPlayerComponent> _filter;
        private EcsFilter<FightCameraComponent> _settings;

        public void Run()
        {
            foreach (var i in _filter)
            {
                ref var entity = ref _filter.GetEntity(i);
                ref var timer = ref _filter.Get1(i);
                ref var timerLerp = ref entity.Get<NeedLerpPositionCameraFollowingToTargetComponent>();

                timer._currentTime += Time.deltaTime;
                if (timer._currentTime > timer._maxTime)
                {
                    timerLerp._currentTime = 0.0f;
                    foreach (var s in _settings)
                    {
                        ref var setting = ref _settings.Get1(s);
                        timerLerp._maxTime = 1.0f;
                    }
                    entity.Del<TimerStopFollowingInPlayerComponent>();
                }
            }
        }
    }
}