using EcsBattle.Components;
using Leopotam.Ecs;


namespace EcsBattle.Systems.Animation
{
    public sealed class StartAnimationStrikeForUnitsSystem : IEcsRunSystem
    {
        private EcsFilter<NeedStartAnimationComponent, UnitComponent> _filter;

        public void Run()
        {
            foreach (var i in _filter)
            {
                ref var entity = ref _filter.GetEntity(i);
                ref var animator = ref _filter.Get2(i).animator;

                animator.SetTriggerAttack();
                entity.Del<NeedStartAnimationComponent>();
            }
        }
    }
}