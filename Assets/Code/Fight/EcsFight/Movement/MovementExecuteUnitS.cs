using Leopotam.Ecs;
using UnityEngine;


namespace Code.Fight.EcsFight.Movement{
    public class MovementExecuteUnitS : IEcsRunSystem{
        private EcsWorld _world;
        private EcsFilter<UnitC, DirectionMovementC, MovementEventC> _filter;

        public void Run(){
            foreach (var i in _filter){
                ref var entity = ref _filter.GetEntity(i);
                ref var unitRootTransform = ref _filter.Get1(i).RootTransform;
                ref var unitModelTransform = ref _filter.Get1(i).ModelTransform;
                ref var directionMoving = ref _filter.Get2(i);
                ref var eventVector = ref _filter.Get3(i);

                //точка назначения перемещения
                directionMoving.Value.localPosition = Vector3.ClampMagnitude(eventVector.Value, 1f);
                entity.Del<MovementEventC>();
                //если не в точке то надо подвинутся и повернуться
                if (directionMoving.Value.localPosition.sqrMagnitude > Vector3.kEpsilon){
                    entity.Get<NeedStepC>().Value = unitRootTransform.position - directionMoving.Value.position;
                    entity.Get<NeedRotateC>().Value = directionMoving.Value;
                }
            }
        }
    }
}