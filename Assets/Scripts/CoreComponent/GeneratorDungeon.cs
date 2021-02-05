using System;
using Controller;
using Data;
using DungeonArchitect;
using DungeonArchitect.Builders.GridFlow;
using UniRx;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;


namespace CoreComponent
{
    public sealed class GeneratorDungeon : IGeneratorDungeon
    {
        private GridFlowDungeonBuilder _builder;
        private readonly GridFlowDungeonConfig _config;
        private readonly GameObject _dungeon;
        private readonly DungeonGeneratorData _dungeonGeneratorData;
        private readonly Dungeon _generator;
        private readonly Transform _parent;
        private readonly PooledDungeonSceneProvider _pooledSceneProvider;
        private Type _typeSpawnPlayer;
        private bool isEnableDungeon;

        public GeneratorDungeon(DungeonGeneratorData dungeonGeneratorData, Transform parent)
        {
            _dungeonGeneratorData = dungeonGeneratorData;
            _parent = parent;

            _dungeon = Object.Instantiate(new GameObject(), _parent);
            _dungeon.name = "root-Dungeon.Static";
            _dungeon.isStatic = true;

            var gO = Object.Instantiate(_dungeonGeneratorData.StorageGenerator, _parent);
            _generator = gO.GetComponent<Dungeon>();
            _config = gO.GetComponent<GridFlowDungeonConfig>();
            _builder = gO.GetComponent<GridFlowDungeonBuilder>();
            _pooledSceneProvider = gO.GetComponent<PooledDungeonSceneProvider>();
            _pooledSceneProvider.itemParent = _dungeon;

            Seed = new ReactiveProperty<uint>(_config.Seed);
            Seed.Subscribe(x => { _config.Seed = x; });
        }

        public IReactiveProperty<uint> Seed { get; set; }

        public void BuildDungeon()
        {
            _generator.Build();
            isEnableDungeon = true;
        }

        public void DestroyDungeon()
        {
            _generator.DestroyDungeon();
            isEnableDungeon = false;
        }

        public Transform GetPlayerPosition()
        {
            if (!isEnableDungeon) return null;

            var result = _parent.GetComponentInChildren<SpawnMarkerCharacterInDungeon>();
            if (result != null)
                return result.transform;
            return null;
        }

        public void SetRandomSeed()
        {
            DestroyDungeon();
            Seed.Value = (uint) Random.Range(0, int.MaxValue);
        }

        public GameObject Dungeon()
        {
            return _dungeon;
        }
    }
}