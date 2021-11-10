using Code.Unit;
using Leopotam.Ecs;
using UnityEngine;


namespace Code.Fight.EcsFight.Movement{
    public class MovementUnitS : IEcsRunSystem{
        private EcsFilter<UnitC, ManualMoveEventC> _moveEvent;
        private EcsFilter<UnitC, AutoMoveEventC> _moveInput;

        public void Run(){
            foreach (var i in _moveEvent){
                ref var entity = ref _moveEvent.GetEntity(i);
                ref var unit = ref _moveEvent.Get1(i);
                ref var inputs = ref _moveEvent.Get2(i);

                var move = new MovementInputsC{
                    MoveVector = new Vector3(
                        inputs.Vector.x,
                        inputs.Vector.y,
                        inputs.Vector.z
                    ),
                    CameraRotation = inputs.CameraRotation
                };
                unit.UnitMovement.SetInputs(ref move);
                entity.Del<ManualMoveEventC>();
            }

            foreach (var i in _moveInput){
                ref var entity = ref _moveInput.GetEntity(i);
                ref var unit = ref _moveInput.Get1(i);
                ref var inputs = ref _moveInput.Get2(i);

                var move = new MovementInputsC{
                    MoveVector = new Vector3(
                        inputs.Vector.x,
                        inputs.Vector.y,
                        inputs.Vector.z
                    ),
                    CameraRotation = inputs.CameraRotation
                };
                unit.UnitMovement.SetInputs(ref move);
                entity.Del<AutoMoveEventC>();
            }
        }
    }
}