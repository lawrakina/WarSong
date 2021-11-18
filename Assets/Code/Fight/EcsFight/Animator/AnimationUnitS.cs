using Code.Fight.EcsFight.Settings;
using Leopotam.Ecs;
using UnityEngine;


namespace Code.Fight.EcsFight.Animator{
    public class AnimationUnitS : IEcsRunSystem{
        private EcsFilter<UnitC, AnimatorTag> _filter;

        public void Run(){
            foreach (var i in _filter){
                ref var unit = ref _filter.Get1(i);

                var cross = Vector3.Cross(unit.UnitMovement.Motor.Velocity.normalized,
                    unit.UnitMovement.Motor.CharacterForward);
                var dot = Vector3.Dot(unit.UnitMovement.Motor.Velocity,
                    unit.UnitMovement.Motor.CharacterForward);
                // Dbg.Log($"Speed: {dot / 10}  |  Rotate: {-cross.y}");
                unit.Animator.Speed = dot;
                unit.Animator.HorizontalSpeed = -cross.y;
            }
        }
    }
}