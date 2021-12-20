using Code.Extension;
using Code.Fight.EcsFight.Battle;
using Code.Fight.EcsFight.Settings;
using Code.Unit;
using Leopotam.Ecs;
using UnityEngine;


namespace Code.Fight.EcsFight.Movement{
    public class MovementUnitS : IEcsRunSystem{
        private EcsFilter<UnitC, ManualMoveEventC>.Exclude<DeathTag> _manualMoveEvent;
        private EcsFilter<UnitC, AutoMoveEventC>.Exclude<DeathTag> _autoMoveEvent;

        public void Run(){
            foreach (var i in _manualMoveEvent){
                ref var entity = ref _manualMoveEvent.GetEntity(i);
                ref var unit = ref _manualMoveEvent.Get1(i);
                ref var inputs = ref _manualMoveEvent.Get2(i);

                var move = new MovementInputsC{
                    MoveVector = new Vector3(
                        inputs.Vector.x,
                        inputs.Vector.y,
                        inputs.Vector.z
                    ),
                    CameraRotation = inputs.CameraRotation
                };
                unit.UnitMovement.SetInputs( move);
                entity.Del<ManualMoveEventC>();
            }

            foreach (var i in _autoMoveEvent){
                ref var entity = ref _autoMoveEvent.GetEntity(i);
                ref var unit = ref _autoMoveEvent.Get1(i);
                ref var inputs = ref _autoMoveEvent.Get2(i);

                var move = new MovementInputsC{
                    MoveVector = new Vector3(
                        inputs.Vector.x,
                        inputs.Vector.y,
                        inputs.Vector.z
                    ),
                    CameraRotation = inputs.CameraRotation
                };
                unit.UnitMovement.SetInputs( move);
                entity.Del<AutoMoveEventC>();
            }
        }
    }
}