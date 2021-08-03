using Code.Data;
using Code.Extension;
using UnityEngine;


namespace Code.GameCamera
{
    public class CameraController : BaseController
    {
        private readonly DataSettings _settings;
        private Camera _camera;
        private IFightCamera _fightCamera;
        public IFightCamera FightCamera => _fightCamera;

        public CameraController(DataSettings settings)
        {
            _settings = settings;
            _camera = Camera.main;

            if (_camera == null)
            {
                Dbg.Log($"cameraSettings - {settings}");
                _camera = Object.Instantiate(settings.CameraSettings.CameraPrefab, settings.CameraSettings.CameraStartPosition,
                    Quaternion.Euler(settings.CameraSettings.CameraStartRotation));
                _fightCamera = CreateCamera(_camera);
            }
            else
            {
                _camera.transform.position = settings.CameraSettings.CameraStartPosition;
                _camera.transform.rotation = Quaternion.Euler(settings.CameraSettings.CameraStartRotation);
            }
        }

        private IFightCamera CreateCamera(Camera baseCamera)
        {
            var component = baseCamera.gameObject.AddCode<FightCamera>();
            var camera = component.GetComponent<IFightCamera>();
            camera.Camera = baseCamera;
            
            // camera.UiTextManager = Object.Instantiate(_settings.CameraSettings._textDamageManager, camera.Transform);
            // camera.UiTextManager.canvas.worldCamera = camera.Transform.GetComponent<Camera>();
            // camera.UiTextManager.theCamera = camera.Transform.GetComponent<Camera>();
            
            camera.Settings = _settings.CameraSettings;
            return camera;
        }
    }
}