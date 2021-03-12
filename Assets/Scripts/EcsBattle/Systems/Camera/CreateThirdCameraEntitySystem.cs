using EcsBattle.Components;
using Interface;
using Leopotam.Ecs;
using Unit.Player;
using UnityEngine;


namespace EcsBattle.Systems.Camera
{
    public sealed class CreateThirdCameraEntitySystem : IEcsInitSystem
    {
        private EcsWorld _world;
        private IFightCamera _camera;
        private IPlayerView _player;

        public void Init()
        {
            _camera.ThirdTarget = Object.Instantiate(new GameObject("ThirdPersonTargetCamera"), _player.Transform).transform;
            _camera.ThirdTarget.localPosition = _camera.OffsetThirdPosition();
            
            var camera = _world.NewEntity();
            // camera.Get<FightCameraComponent>().maxTimeToStopFollowingInPlayer = _camera.Settings.maxTimeToStopFollowingInPlayer;
            // camera.Get<FightCameraComponent>().maxTimeToLerpInPlayer = _camera.Settings.maxTimeToLerpInPlayer;
            camera.Get<FightCameraComponent>().positionThirdTarget = _camera.ThirdTarget;
            camera.Get<FightCameraComponent>().positionPlayerTransform = _player.Transform;
            camera.Get<TransformComponent>().value = _camera.Transform;
        }
    }
}