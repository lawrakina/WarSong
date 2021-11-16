using System;
using Leopotam.Ecs;
using UnityEngine;


namespace Code.Fight.EcsFight.Camera{
    public class CameraUpdateS : IEcsRunSystem{
        private EcsFilter<CameraC, UnitC, ManualMoveEventC> _positionCamera;
        private EcsFilter<CameraC, UnitC, TargetListC> _alignToCamera;
        public void Run(){
            foreach (var i in _positionCamera){
                ref var camera = ref _positionCamera.Get1(i);
                ref var moveEvent = ref _positionCamera.Get3(i);

                switch (moveEvent.ControlType){
                    case ControlType.Manual:
                        camera.Value.UpdateWithInput(Time.deltaTime, 0f, moveEvent.Vector);
                        break;
                    case ControlType.AutoAttack:
                        var axisRight = Vector3.SignedAngle(
                            camera.Value.transform.forward,
                            moveEvent.Vector,
                            Vector3.up) / 180;
                        camera.Value.UpdateWithInput(
                            Time.deltaTime, 
                            0f, 
                            new Vector3(axisRight, 0f, 0f));
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
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