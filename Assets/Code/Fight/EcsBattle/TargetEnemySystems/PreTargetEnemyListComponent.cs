using System.Collections.Generic;
using Code.Fight.EcsBattle.CustomEntities;
using Leopotam.Ecs;

namespace Code.Fight.EcsBattle.TargetEnemySystems
{
    public struct PreTargetEnemyListComponent
    {
        public Dictionary<UnitComponent, float> preTargets;
        public UnitComponent currentTarget;
    }
}