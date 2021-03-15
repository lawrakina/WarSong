using System;
using System.Collections.Generic;
using System.Linq;
using Controller;
using Data;
using DungeonArchitect;
using DungeonArchitect.Builders.Grid.SpatialConstraints;
using DungeonArchitect.Builders.GridFlow;
using DungeonArchitect.Navigation;
using Extension;
using Interface;
using UniRx;
using Unit.Enemies;
using UnityEngine;
using VIew;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;


namespace CoreComponent
{
    public sealed class GeneratorDungeon : IGeneratorDungeon, IController, IExecute
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
        private AstarPath _navMash;
        private CityLayoutTriangleProvider _layetProviderNavMesh;
        private bool _buildingProcess;
        private bool _buildingComplete;

        public GeneratorDungeon(DungeonGeneratorData dungeonGeneratorData, Transform parent)
        {
            _dungeonGeneratorData = dungeonGeneratorData;
            _parent = parent;

            _dungeon = Object.Instantiate(new GameObject(), _parent);
            _dungeon.name = "root-Dungeon.Static";
            _dungeon.isStatic = true;

            var gameObjectGenerator = Object.Instantiate(_dungeonGeneratorData.StorageGenerator, _parent);
            _generator = gameObjectGenerator.GetComponent<Dungeon>();
            _config = gameObjectGenerator.GetComponent<GridFlowDungeonConfig>();
            _builder = gameObjectGenerator.GetComponent<GridFlowDungeonBuilder>();
            _pooledSceneProvider = gameObjectGenerator.GetComponent<PooledDungeonSceneProvider>();
            _pooledSceneProvider.itemParent = _dungeon;
            _builder.asyncBuild = true;

            // _builder.IsLayoutBuilt;
            // _pooledSceneProvider.OnDungeonBuildStop+= (x =>x){};
            // var proc = new MyDungeonProcessor();
            // _generator.

            var goNavMesh = Object.Instantiate(_dungeonGeneratorData.StorageNavMash, _parent);
            goNavMesh.transform.position = new Vector3(17.0f,0.32f,20.27f);//it`s laziness
            _navMash = goNavMesh.GetComponent<AstarPath>();
            _navMash.scanOnStartup = false;

            Seed = new ReactiveProperty<uint>(_config.Seed);
            Seed.Subscribe(x => { _config.Seed = x; });
        }

        public IReactiveProperty<uint> Seed { get; set; }

        public void BuildDungeon()
        {
            Dbg.Log($"Before BuildDungeon._generator.IsLayoutBuilt:{_generator.IsLayoutBuilt}");
            _generator.Build();
            Dbg.Log($"After BuildDungeon._generator.IsLayoutBuilt:{_generator.IsLayoutBuilt}");
            _buildingProcess = true;
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

        public List<SpawnMarkerEnemyInDungeon> GetEnemiesMarkers()
        {
            if (!isEnableDungeon) return null;

            var result = _parent.GetComponentsInChildren<SpawnMarkerEnemyInDungeon>();
            return result.ToList();
        }

        public void Execute(float deltaTime)
        {
            if (_buildingProcess)
            {
                Debug.Log($"Before _builder.IsLayoutBuilt:{_builder.IsLayoutBuilt}");
                if (_generator.IsLayoutBuilt)
                {
                    Debug.Log($"After _builder.IsLayoutBuilt:{_builder.IsLayoutBuilt}");
                    _buildingProcess = false;
                    _buildingComplete = true;
                }
                // Debug.Log($"Before _generator.IsLayoutBuilt:{_generator.IsLayoutBuilt}");
                // if (_generator.IsLayoutBuilt)
                // {
                //     Debug.Log($"After _generator.IsLayoutBuilt:{_generator.IsLayoutBuilt}");
                //     _buildingProcess = false;
                //     _buildingComplete = true;
                // }
            }

            if (_buildingComplete)
            {
                Dbg.Log($"BuildNavMash");
                // _navMash.Scan();
                foreach (var progress in _navMash.ScanAsync())
                {
                    Dbg.Log($"progress:{progress},{progress.progress},{progress.description}");
                }

                _buildingComplete = false;
                Dbg.Log($"EndBuildNavmash");
                isEnableDungeon = true;
            }
            // Dbg.Log($"_generator.IsLayoutBuilt:{_generator.IsLayoutBuilt}");
        }

        public void On()
        {
            
        }

        public void Off()
        {
            
        }
    }
    
    public class MyDungeonProcessor : DungeonEventListener {
        ///<summary>
        ///Called after the dungeon is completely built
        ///</summary>
        public virtual void OnPostDungeonBuild(Dungeon dungeon, DungeonModel model) {
            var myObjs = GameObject.FindObjectsOfType<DungeonSceneProviderData>();
            foreach (var obj in myObjs) {
                // your processing here
                Dbg.Log($"111111111 {obj}");
            }
        }
    }
}