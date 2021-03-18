using EcsBattle.Components;
using Leopotam.Ecs;


namespace EcsBattle.Systems.Attacks
{
    public sealed class Attack5StartAnimationStrikeSystem : IEcsRunSystem
    {
        private EcsFilter<NeedStartAnimationComponent, PlayerComponent> _filter;
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