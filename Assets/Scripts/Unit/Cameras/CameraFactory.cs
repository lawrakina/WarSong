using Data;
using Extension;
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

        public FightCamera CreateCamera(Camera baseCamera)
        {
            var component = baseCamera.gameObject.AddCode<FightCamera>();
            var camera = component.GetComponent<FightCamera>();
            camera.Settings = _cameraSettings;
            return camera;
        }
    }
}