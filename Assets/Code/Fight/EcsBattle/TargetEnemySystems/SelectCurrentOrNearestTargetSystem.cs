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
                ref var playerUnitComponent = ref _playerFilter.Get3(i);
                
                foreach (var enemy in playerPreTargetList.preTargets)
                {
                    //Dbg.Log($"{enemy._rootTransform.position - playerUnitComponent._rootTransform.position}");
                    var currentEnemyTarget = 
                        playerPreTargetList.preTargets.Aggregate((l, r)
                            => l.Value < r.Value ? l : r).Key;
                    playerPreTargetList.currentTarget = currentEnemyTarget;
                    //Dbg.Error($"{currentEnemyTarget._view.Transform.position}");
                }
            }
        }
    }
}