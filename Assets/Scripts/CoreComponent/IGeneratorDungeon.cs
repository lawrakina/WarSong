using UniRx;
using UnityEngine;


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
    }
}