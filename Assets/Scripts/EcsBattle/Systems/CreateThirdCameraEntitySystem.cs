﻿using EcsBattle.Components;
using Interface;
using Leopotam.Ecs;
using Unit.Player;
using UnityEngine;


namespace EcsBattle
{
    public sealed class CreateThirdCameraEntitySystem : IEcsInitSystem
    {
        private EcsWorld _world;
        private IFightCamera _camera;
        private IPlayerView _player;

        public void Init()
        {
            _camera.ThirdTarget = Object.Instantiate(
                new GameObject("ThirdPersonTargetCamera"),
                _player.Transform
            ).transform;
            _camera.ThirdTarget.localPosition = _camera.OffsetThirdPosition();
            
            var camera = _world.NewEntity();
            camera.Get<FightCameraComponent>();
            camera.Get<TransformComponent>().Value = _camera.Transform;
            camera.Get<TargetCameraComponent>().Position = _camera.ThirdTarget;
            camera.Get<TargetCameraComponent>().Rotate = _player.Transform;
        }
    }
}