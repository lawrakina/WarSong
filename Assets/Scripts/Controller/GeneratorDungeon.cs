using System;
using System.Collections.Generic;
using System.Linq;
using Controller.Model;
using Data;
using DungeonArchitect;
using DungeonArchitect.Builders.GridFlow;
using DungeonArchitect.Navigation;
using Interface;
using UniRx;
using Unit;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;


namespace Controller
{
    public sealed class GeneratorDungeon : IGeneratorDungeon, IController, IExecute
    {
        private readonly GeneratorDungeonModel _model;
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

        public GeneratorDungeon(GeneratorDungeonModel model)
        {
            _model = model;
            _dungeonGeneratorData = _model.GeneratorData;
            _parent = _model.Windows.RootDungeonTransform;

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
            _generator.Build();
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

        public SpawnMarkerGoalInDungeon GetGoalLevelMarker()
        {
            var result = _parent.GetComponentInChildren<SpawnMarkerGoalInDungeon>();
            return result;
        }

        public void Execute(float deltaTime)
        {
            if (_buildingProcess)
            {
                if (_generator.IsLayoutBuilt)
                {
                    _buildingProcess = false;
                    _buildingComplete = true;
                }
            }
            if (_buildingComplete)
            {
                _buildingComplete = false;
                isEnableDungeon = true;
            }
        }
    }
}