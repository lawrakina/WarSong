using System;
using System.Linq;
using Code.Extension;
using Leopotam.Ecs;

namespace Code.Fight.EcsBattle.TargetEnemySystems
{
    public class SelectCurrentOrNearestTargetSystem : IEcsRunSystem
    {
        private EcsFilter<PreTargetEnemyListComponent, PlayerComponent, UnitComponent> _playerFilter;

        public void Run()
        {
            foreach (var i in _playerFilter)
            {
                ref var playerPreTargetList = ref _playerFilter.Get1(i);


                if (playerPreTargetList.preTargetsSqrDistances.Length > 0)
                {
                    var nearestTargetDistance = playerPreTargetList.preTargetsSqrDistances.Min();
                    for (int j = 0; j < playerPreTargetList.preTargetsSqrDistances.Length; j++)
                    {
                        if (Math.Abs(playerPreTargetList.preTargetsSqrDistances[j] - nearestTargetDistance) < 0.0f)
                        {
                            playerPreTargetList.currentTarget = playerPreTargetList.preTargetsUnitComponents[j];
                        }
                    }

                    if (playerPreTargetList.currentTarget._rootTransform != null)
                    {
                        Dbg.Error($"{playerPreTargetList.currentTarget._rootTransform.position}");
                    }
                }
            }
        }
    }
}