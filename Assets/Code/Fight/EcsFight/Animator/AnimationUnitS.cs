using Leopotam.Ecs;


namespace Code.Fight.EcsFight.Animator{
    public class AnimationUnitS : IEcsRunSystem{
        private EcsFilter<UnitC, DirectionMovementC> _animateStep;

        public void Run(){
            foreach (var i in _animateStep){
                ref var animator = ref _animateStep.Get1(i).Animator;
                ref var direction = ref _animateStep.Get2(i).Value;
                animator.Speed = direction.localPosition.z;
                animator.HorizontalSpeed = direction.localPosition.x;
            }
        }
    }
}