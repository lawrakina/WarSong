using System;
using Code.Fight.EcsFight.Settings;
using Leopotam.Ecs;
using UnityEngine;


namespace Code.Fight.EcsFight.Camera{
    public class CameraUpdateS : IEcsRunSystem{
        private EcsFilter<CameraC, UnitC> _positionCamera;
        private EcsFilter<CameraC, UnitC, ManualMoveEventC> _manualInputPositionCamera;
        private EcsFilter<CameraC, UnitC, AutoMoveEventC> _autoInputPositionCamera;
        private EcsFilter<CameraC, UnitC, TargetListC> _alignToCamera;

        public void Run(){
            var isInputActive = false;
            foreach (var i in _manualInputPositionCamera){
                isInputActive = true;
                ref var camera = ref _manualInputPositionCamera.Get1(i);
                ref var moveEvent = ref _manualInputPositionCamera.Get3(i);

                camera.Value.UpdateWithInput(Time.deltaTime, 0f, moveEvent.Vector);
            }

            foreach (var i in _autoInputPositionCamera){
                isInputActive = true;
                ref var camera = ref _autoInputPositionCamera.Get1(i);
                ref var moveEvent = ref _autoInputPositionCamera.Get3(i);

                var axisRight = Vector3.SignedAngle(
                    camera.Value.transform.forward,
                    moveEvent.Vector,
                    Vector3.up) / 180;
                camera.Value.UpdateWithInput(
                    Time.deltaTime,
                    0f,
                    new Vector3(axisRight, 0f, 0f));
            }

            if (!isInputActive){
                foreach (var i in _positionCamera){
                    ref var camera = ref _positionCamera.Get1(i);
                    camera.Value.UpdateWithInput(Time.deltaTime, 0f, new Vector3());
                }
            }

            foreach (var i in _alignToCamera){
                ref var unit = ref _alignToCamera.Get2(i);
                foreach (var enemyView in unit.UnitVision.List){
                    enemyView.HealthBar.AlignCamera();
                }
            }
        }
    }
}