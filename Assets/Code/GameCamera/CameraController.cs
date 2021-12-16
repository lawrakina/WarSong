using System;
using System.Collections.Generic;
using System.Linq;
using Code.Data;
using Code.Extension;
using Code.Profile;
using Code.UI.CharacterList;
using UnityEngine;
using Object = UnityEngine.Object;


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
            var camera = baseCamera.GetComponent<BattleCamera>();

            var hider = Object.Instantiate(_profilePlayer.Settings.CameraSettings.FaderManager);
            hider.Camera = _camera;

            var manager = Object.Instantiate(_profilePlayer.Settings.CameraSettings.TextDamageManager, camera.Transform);
            manager.canvas.worldCamera = camera.Transform.GetComponent<Camera>();
            manager.theCamera = camera.Transform.GetComponent<Camera>();
            
            camera.UiTextManager = new UiTextManager{
                Manager = manager,
                Offset = _profilePlayer.Settings.CameraSettings.OffsetForDamageText
            };
            
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

        public void UpdatePosition(CameraAngles position){
            var angle= _profilePlayer.Settings.CameraSettings.CameraAngles.FirstOrDefault(x => x.Angle == position);
            if (angle != null)
                _camera.transform.SetPositionAndRotation(angle.Position, Quaternion.Euler(angle.Rotation));


            return;
            switch (position){
                case CameraAngles.BeforePlayer:
                    _battleCamera.enabled = false;
                    _camera.transform.position = _profilePlayer.CurrentPlayer.Transform.position - new Vector3(0,0,2f);
                    _camera.transform.RotateAround(
                        _profilePlayer.CurrentPlayer.Transform.position,
                        Vector3.up, 
                        180f);
                    break;
                case CameraAngles.AfterPlayer:
                    _battleCamera.enabled = true;
                    break;
                case CameraAngles.InTheFace:
                    _battleCamera.enabled = false;
                    _camera.transform.position = _profilePlayer.CurrentPlayer.Transform.position + new Vector3(0,0,2f);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(position), position, null);
            }
        }
    }
}