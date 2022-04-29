using Leopotam.Ecs;


namespace Code.Fight.EcsBattle.Unit.Animation
{
    public sealed class AnimationMoveForPlayerSystem : IEcsRunSystem
    {
        private EcsFilter<PlayerComponent, UnitComponent, DirectionMovementComponent> _filter;
        public void Run()
        {
            foreach (var index in _filter)
            {
                ref var unit = ref _filter.Get2(index);
                ref var direction = ref _filter.Get3(index);
                // if(direction.value.localPosition.x < Vector3.kEpsilon && 
                //    direction.value.localPosition.z < Vector3.kEpsilon) return;
                
                unit._animator.Speed = direction._value.localPosition.z;
                unit._animator.HorizontalSpeed = direction._value.localPosition.x;
            }
        }
    }
}