﻿using EcsBattle.Components;
using Leopotam.Ecs;


namespace EcsBattle
{
    public sealed class AnimationBattleState : IEcsRunSystem
    {
        private EcsFilter<PlayerComponent> _filter;
        public void Run()
        {
            foreach (var i in _filter)
            {
                ref var entity = ref _filter.GetEntity(i);
                ref var player = ref _filter.Get1(i);

                player.animator.Battle = entity.Has<CurrentTargetComponent>();
            }
        }
    }
}