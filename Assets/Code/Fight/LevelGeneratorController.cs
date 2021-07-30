using System;
using System.Collections.Generic;
using System.Threading;
using Code.Data.Dungeon;
using Code.Data.Marker;
using Code.Extension;
using DungeonArchitect;
using DungeonArchitect.Builders.GridFlow;
using UniRx;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;


namespace Code.Fight
{
    public class LevelGeneratorController : BaseController, IExecute
    {
        private readonly DungeonGeneratorData _settings;
        private GameObject _dungeon;
        private GridFlowDungeonBuilder _builder;
        private readonly GridFlowDungeonConfig _config;
        private readonly Dungeon _generator;
        private readonly PooledDungeonSceneProvider _pooledSceneProvider;

        private static bool _isOn = false;

        // private bool isEnableDungeon;
        // private bool _buildingProcess;
        // private bool _buildingComplete;
        public ReactiveProperty<FightState> FightState = new ReactiveProperty<FightState>();
        private Timer _timer;

        public static bool IsOn
        {
            get => _isOn;
            set => _isOn = value;
        }

        public void Execute(float deltaTime)
        {
            Dbg.Log($"GENERATOR EXECUTE");
            switch (FightState.Value)
            {
                case Fight.FightState.Default:
                    break;
                case Fight.FightState.BuildingStart:
                    Dbg.Log($"Build Start:");
                    _timer = new Timer(new TimerCallback(CheckBuildDungeon), null, 0, 1000);
                    break;
                case Fight.FightState.BuildingProcess:
                    Dbg.Log($"Check build state: {FightState.Value}");
                    break;
                case Fight.FightState.BuildingComplete:
                    Dbg.Log($"Build complete:");
                    break;
                case Fight.FightState.Fight:
                    break;
                case Fight.FightState.Fail:
                    break;
                case Fight.FightState.Win:
                    break;
                case Fight.FightState.Pause:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            // if (_buildingProcess)
            // {
            //     if (_generator.IsLayoutBuilt)
            //     {
            //         _buildingProcess = false;
            //         _buildingComplete = true;
            //     }
            // }
            // if (_buildingComplete)
            // {
            //     _buildingComplete = false;
            //     isEnableDungeon = true;
            //     Dbg.Log($"GeneratorLevel.Complete");
            //     FightState.Value = Fight.FightState.Fight;
            // }
        }

        private void CheckBuildDungeon(object state)
        {
            Dbg.Log($"CheckBuildDungeon");
            if (GetPlayerPosition() == null)
            {
                Dbg.Log($"CheckStatus Dungeon: {GetPlayerPosition()} ?");
                FightState.Value = Fight.FightState.BuildingProcess;
            }
            else
            {
                Dbg.Log($"Start BATTLE: {GetPlayerPosition()} ?");
                FightState.Value = Fight.FightState.BuildingComplete;
                _timer.Dispose();
            }
        }

        private Transform GetPlayerPosition()
        {
            var result = _dungeon.GetComponentInChildren<SpawnMarkerCharacterInDungeon>();
            Dbg.Log($"GetPlayerPosition:{result}");
            if (result != null)
                return result.transform;
            return null;
        }

        public LevelGeneratorController(DungeonGeneratorData settings)
        {
            _isOn = true;
            _settings = settings;

            _dungeon = Object.Instantiate(new GameObject(), new RectTransform());
            _dungeon.name = "---Dungeon";
            _dungeon.isStatic = true;
            AddGameObjects(_dungeon);

            var gameObjectGenerator = Object.Instantiate(_settings.StorageGenerator, _dungeon.transform, false);
            _generator = gameObjectGenerator.GetComponent<Dungeon>();
            _config = gameObjectGenerator.GetComponent<GridFlowDungeonConfig>();
            _builder = gameObjectGenerator.GetComponent<GridFlowDungeonBuilder>();
            _pooledSceneProvider = gameObjectGenerator.GetComponent<PooledDungeonSceneProvider>();
            _pooledSceneProvider.itemParent = _dungeon;
            _builder.asyncBuild = true;
        }

        public void BuildDungeon()
        {
            _generator.Build();
            // _buildingProcess = true;
            FightState.Value = Fight.FightState.BuildingProcess;
        }

        public void GenerateDemoLevel()
        {
            _config.Seed = (uint) Random.Range(0, int.MaxValue);
        }

        public void DestroyDungeon()
        {
            _generator.DestroyDungeon();
            // isEnableDungeon = false;
        }

        protected override void OnDispose()
        {
            DestroyDungeon();
            base.OnDispose();
        }
    }
}