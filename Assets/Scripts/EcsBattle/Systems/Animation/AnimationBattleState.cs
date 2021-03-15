using EcsBattle.Components;
using Leopotam.Ecs;


namespace EcsBattle
{
    public sealed class AnimationBattleState : IEcsRunSystem
    {
        private EcsFilter<AnimatorComponent> _filter;
        public void Run()
        {
            foreach (var i in _filter)
            {
                ref var entity = ref _filter.GetEntity(i);
                ref var animator = ref _filter.Get1(i);

                animator.value.Battle = entity.Has<CurrentTargetComponent>();
            }
        }
    }
}