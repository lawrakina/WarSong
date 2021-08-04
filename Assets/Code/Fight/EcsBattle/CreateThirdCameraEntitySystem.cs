using Code.GameCamera;
using Code.Unit;
using Leopotam.Ecs;
using UnityEngine;


namespace Code.Fight.EcsBattle
{
    public sealed class CreateThirdCameraEntitySystem : IEcsInitSystem
    {
        private EcsWorld _world;
        private IFightCamera _camera;
        private IPlayerView _player;

        public void Init()
        {
            _camera.ThirdTarget = 
                Object.Instantiate(new GameObject("ThirdPersonTargetCamera"), _player.Transform).transform;
            _camera.ThirdTarget.localPosition = _camera.OffsetThirdPosition();

            _camera.Transform.SetParent(_camera.ThirdTarget, false);
            _camera.Transform.LookAt(_player.Transform);
            _camera.Camera.enabled = true;

            //if us CameraPositioningOnMarkerPlayerSystem && CameraRotateOnPlayerSystem than decomment =>
            var camera = _world.NewEntity();
            camera.Get<FightCameraComponent>().uiTextManager = _camera.UiTextManager;
            // camera.Get<FightCameraComponent>().positionThirdTarget = _camera.ThirdTarget;
            // camera.Get<FightCameraComponent>().positionPlayerTransform = _player.Transform;
            // camera.Get<TransformComponent>().value = _camera.Transform;
        }
    }
}