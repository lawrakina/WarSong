using Code.GameCamera;
using Code.Unit;
using Leopotam.Ecs;
using ThirdPersonCameraWithLockOn;
using UnityEngine;


namespace Code.Fight.EcsBattle
{
    public sealed class CreateThirdCameraEntitySystem : IEcsInitSystem
    {
        private EcsWorld _world;
        private ThirdPersonCamera _camera;
        private IPlayerView _player;

        public void Init()
        {
            // _camera.ThirdTarget = 
                // Object.Instantiate(new GameObject("ThirdPersonTargetCamera"), _player.Transform).transform;
            // _camera.ThirdTarget.localPosition = _camera.OffsetThirdPosition;

            // _camera.Transform.SetParent(_camera.ThirdTarget, false);
            // _camera.Transform.LookAt(_player.Transform);
            // _camera.Camera.enabled = true;

            // var camera = _world.NewEntity();
            // camera.Get<FightCameraComponent>().uiTextManager = _camera.UiTextManager;
        }
    }
}