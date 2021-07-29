using System.Collections.Generic;
using UniRx;
using Unit;
using UnityEngine;


namespace Controller
{
    public interface IGeneratorDungeon
    {
        IReactiveProperty<uint> Seed { get; set; }
        void BuildDungeon();
        void DestroyDungeon();
        Transform GetPlayerPosition();
        void SetRandomSeed();
        GameObject Dungeon();
        List<SpawnMarkerEnemyInDungeon> GetEnemiesMarkers();
        SpawnMarkerGoalInDungeon GetGoalLevelMarker();
    }
}