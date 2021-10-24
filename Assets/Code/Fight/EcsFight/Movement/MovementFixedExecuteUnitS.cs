using Leopotam.Ecs;
using UnityEngine;


namespace Code.Fight.EcsFight.Movement{
    public class MovementFixedExecuteUnitS : IEcsRunSystem{
        private EcsFilter<NeedStepC, UnitC> _stepUnits;
        private EcsFilter<NeedRotateC, UnitC> _rotateUnits;

        public void Run(){
            foreach (var i in _stepUnits){
                ref var entity = ref _stepUnits.GetEntity(i);
                ref var needStep = ref _stepUnits.Get1(i);
                ref var unit = ref _stepUnits.Get2(i);

                unit.Rigidbody.MovePosition(
                    unit.RootTransform.position -
                    (needStep.Value * (unit.Characteristics.Speed * Time.fixedDeltaTime)));
                entity.Del<NeedStepC>();
            }

            foreach (var i in _rotateUnits){
                ref var entity = ref _rotateUnits.GetEntity(i);
                ref var needRotate = ref _rotateUnits.Get1(i);
                ref var unit = ref _stepUnits.Get2(i);

                unit.RootTransform.RotateAround(
                    unit.RootTransform.position,
                    Vector3.up,
                    unit.Characteristics.RotateSpeedPlayer * Time.fixedDeltaTime * needRotate.Value.localPosition.x
                );

                if (!Equals(unit.ModelTransform, null))
                    unit.ModelTransform.localRotation = Quaternion.identity;

                entity.Del<NeedRotateC>();
            }
        }
    }

}