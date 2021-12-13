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
        private BattleCamera _battleCamera;
        public BattleCamera BattleCamera => _battleCamera;
        
        public CameraController(ProfilePlayer profilePlayer)
        {
            _profilePlayer = profilePlayer;
            _camera = Camera.main;

            if (_camera == null)
            {
                _camera = Object.Instantiate(profilePlayer.Settings.CameraSettings.CameraPrefab);
                _battleCamera = CreateCamera(_camera);
            }
        }


        private BattleCamera CreateCamera(Camera baseCamera)
        {
            baseCamera.fieldOfView = 60.0f;
            // var component = baseCamera.gameObject.AddCode<ThirdPersonCamera>();
            var camera = baseCamera.GetComponent<BattleCamera>();

            var hider = Object.Instantiate(_profilePlayer.Settings.CameraSettings.FaderManager);
            hider.Camera = _camera;

            // camera.UiTextManager = Object.Instantiate(_settings.CameraSettings._textDamageManager, camera.Transform);
            // camera.UiTextManager.canvas.worldCamera = camera.Transform.GetComponent<Camera>();
            // camera.UiTextManager.theCamera = camera.Transform.GetComponent<Camera>();
            
            return camera;
        }

        public void StartFight(){
            var player = _profilePlayer.CurrentPlayer;
            if (!player.Transform.TryGetComponent<FadeToMe>(out var fide)){
                player.Transform.gameObject.AddCode<FadeToMe>();
            } else{
                fide.enabled = true;
            }
        }
    }
}