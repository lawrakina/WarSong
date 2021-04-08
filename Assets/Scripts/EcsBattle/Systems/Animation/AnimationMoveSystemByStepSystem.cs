using EcsBattle.Components;
using Leopotam.Ecs;


namespace EcsBattle.Systems.Animation
{
    public sealed class AnimationMoveSystemByStepSystem : IEcsRunSystem
    {
        private EcsFilter<UnitComponent, EnemyComponent> _filter;
        public void Run()
        {
            foreach (var i in _filter)
            {
                ref var entity = ref _filter.GetEntity(i);
                ref var animator = ref _filter.Get1(i)._animator;
                ref var step = ref _filter.Get1(i);

                if (entity.Has<NeedStepComponent>())
                {
                    animator.Speed = entity.Get<NeedStepComponent>()._value.magnitude;
                }
                else
                {
                    animator.Speed = 0.0f;
                }
            }
        }
    }
}