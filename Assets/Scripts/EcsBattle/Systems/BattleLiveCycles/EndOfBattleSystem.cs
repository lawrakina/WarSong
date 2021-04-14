using EcsBattle.Components;
using Enums;
using Leopotam.Ecs;
using UniRx;
using UnityEngine;


namespace EcsBattle.Systems.BattleLiveCycles
{
    public sealed class EndOfBattleSystem : IEcsRunSystem
    {
        private EcsFilter<GoalLevelComponent, GoalLevelAchievedComponent> _filter;
        public void Run()
        {
            foreach (var i in _filter)
            {
                ref var entity = ref _filter.GetEntity(i);

                Time.timeScale = 0;
                // _battleState.Value = EnumBattleWindow.Victory;
            }
        }
    }
}