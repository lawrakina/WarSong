using EcsBattle.Components;
using Leopotam.Ecs;


namespace EcsBattle.Systems.Attacks
{
    public sealed class Attack5StartAnimationStrikeSystem : IEcsRunSystem
    {
        private EcsFilter<NeedStartAnimationComponent, UnitComponent> _player;
        
        public void Run()
        {
            foreach (var i in _player)
            {
                ref var entity = ref _player.GetEntity(i);
                ref var animator = ref _player.Get2(i).animator;

                animator.SetTriggerAttack();
                entity.Del<NeedStartAnimationComponent>();
            }
        }
    }
}