using Code.Extension;
using Data;
using Interface;
using UnityEngine;


namespace Unit.Cameras
{
    public sealed class CameraFactory : ICameraFactory
    {
        private readonly CameraSettingsInBattle _cameraSettings;

        public CameraFactory(CameraSettingsInBattle cameraSettings)
        {
            _cameraSettings = cameraSettings;
        }

        public IFightCamera CreateCamera(Camera baseCamera)
        {
            var component = baseCamera.gameObject.AddCode<FightCamera>();
            var camera = component.GetComponent<IFightCamera>();
            camera.Camera = baseCamera;
            
            camera.UiTextManager = Object.Instantiate(_cameraSettings._textDamageManager, camera.Transform);
            camera.UiTextManager.canvas.worldCamera = camera.Transform.GetComponent<Camera>();
            camera.UiTextManager.theCamera = camera.Transform.GetComponent<Camera>();
            
            camera.Settings = _cameraSettings;
            return camera;
        }
    }
}