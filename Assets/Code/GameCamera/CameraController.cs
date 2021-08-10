using Code.Data;
using Code.Extension;
using Code.Profile;
using ThirdPersonCameraWithLockOn;
using UnityEngine;


namespace Code.GameCamera
{
    public class CameraController : BaseController
    {
        private readonly ProfilePlayer _profilePlayer;
        private Camera _camera;
        private ThirdPersonCamera _fightCamera;
        public ThirdPersonCamera FightCamera => _fightCamera;
        
        public CameraController(ProfilePlayer profilePlayer)
        {
            _profilePlayer = profilePlayer;
            _camera = Camera.main;

            if (_camera == null)
            {
                _camera = Object.Instantiate(profilePlayer.Settings.CameraSettings.CameraPrefab);
                _fightCamera = CreateCamera(_camera);
            }
        }


        private ThirdPersonCamera CreateCamera(Camera baseCamera)
        {
            baseCamera.fieldOfView = 60.0f;
            // var component = baseCamera.gameObject.AddCode<ThirdPersonCamera>();
            var camera = baseCamera.GetComponent<ThirdPersonCamera>();
            camera.Follow = _profilePlayer.CurrentPlayer.Transform;
            camera.UsingMouse = false;
            camera.DefaultYAngle = 12.0f;
            camera.Distance = 15.0f;
            // camera.UiTextManager = Object.Instantiate(_settings.CameraSettings._textDamageManager, camera.Transform);
            // camera.UiTextManager.canvas.worldCamera = camera.Transform.GetComponent<Camera>();
            // camera.UiTextManager.theCamera = camera.Transform.GetComponent<Camera>();
            
            return camera;
        }
    }
}