using System.Collections.Generic;
using UniRx;
using Unit.Enemies;
using UnityEngine;
using VIew;


namespace CoreComponent
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