using Code.Data;
using Code.Extension;
using UnityEngine;


namespace Code.GameCamera
{
    public class CameraController : BaseController
    {
        private UnityEngine.Camera _camera;

        public CameraController(CameraSettings cameraSettings)
        {
            _camera = UnityEngine.Camera.main;

            if (_camera == null)
            {
                Dbg.Log($"cameraSettings - {cameraSettings}");
                _camera = Object.Instantiate(cameraSettings.CameraPrefab, cameraSettings.CameraStartPosition,
                    Quaternion.Euler(cameraSettings.CameraStartRotation));
            }
            else
            {
                _camera.transform.position = cameraSettings.CameraStartPosition;
                _camera.transform.rotation = Quaternion.Euler(cameraSettings.CameraStartRotation);
            }
        }
    }
}