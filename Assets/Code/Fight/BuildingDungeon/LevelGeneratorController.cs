﻿using System;
using Code.Data.Dungeon;
using Code.Data.Marker;
using Code.Extension;
using Code.Profile.Models;
using Code.TimeRemaining;
using DungeonArchitect;
using DungeonArchitect.Builders.GridFlow;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;


namespace Code.Fight.BuildingDungeon
{
    public class LevelGeneratorController : BaseController, IVerifiable
    {
        private readonly Controllers _controllers;
        private readonly DungeonGeneratorData _settings;
        private readonly FightDungeonModel _fightModel;
        private GameObject _dungeon;
        private GridFlowDungeonBuilder _builder;
        private readonly GridFlowDungeonConfig _config;
        private readonly Dungeon _generator;
        private readonly PooledDungeonSceneProvider _pooledSceneProvider;

        private static bool _isOn = false;
        private TimeRemaining.TimeRemaining _timerCheckBuildState;

        public BuildStatus Status { get; set; }

        public event Action<IVerifiable> Complete = verifiable => { verifiable.Status = BuildStatus.Complete;};

        public static bool IsOn
        {
            get => _isOn;
            set => _isOn = value;
        }
        

        private void CheckBuildDungeon()
        {
            Dbg.Log($"CheckBuildDungeon");
            if (GetPlayerPosition() == null)
            {
                Dbg.Log($"CheckStatus Dungeon: {GetPlayerPosition()} ?");
                _fightModel.InfoState.Value = StringManager.INFO_BULDING_STATE_BUILDING_PROCESS;
                _fightModel.FightState.Value = FightState.BuildingProcess;
            }
            else
            {
                Dbg.Log($"Start BATTLE: {GetPlayerPosition()} ?");
                _fightModel.FightState.Value = FightState.BuildingComplete;
                _fightModel.InfoState.Value = StringManager.INFO_BUILDING_STATE_BUILDING_COMPLETE_START_SPAWN_OBJECTS;
                _fightModel.OnChangePlayerPosition?.Invoke(GetPlayerPosition());
                _fightModel.OnChangeEnemiesPositions?.Invoke(GetEnemiesPositions());
                _timerCheckBuildState.RemoveTimeRemaining();
            }
        }
        
        private SpawnMarkerEnemyInDungeon[] GetEnemiesPositions()
        {
            var result = _dungeon.GetComponentsInChildren<SpawnMarkerEnemyInDungeon>();
            return result ?? null;
        }

        private Transform GetPlayerPosition()
        {
            var result = _dungeon.GetComponentInChildren<SpawnMarkerCharacterInDungeon>();
            if (result != null)
                return result.transform;
            return null;
        }

        public LevelGeneratorController(Controllers controllers, DungeonGeneratorData settings,
            FightDungeonModel fightModel)
        {
            _isOn = true;
            Status = BuildStatus.Passive;
            _controllers = controllers;
            _settings = settings;
            _fightModel = fightModel;

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
            Status = BuildStatus.Process;
            GenerateDemoLevel();
            _generator.Build();
            _fightModel.FightState.Value = FightState.BuildingProcess;
            _fightModel.InfoState.Value = StringManager.INFO_BULDING_STATE_START_PROCESS;
            _timerCheckBuildState = new TimeRemaining.TimeRemaining(CheckBuildDungeon, 1f, true);
            _timerCheckBuildState.AddTimeRemaining();
            
            Complete?.Invoke(this);
        }

        private void GenerateDemoLevel()
        {
            _config.Seed = (uint) Random.Range(0, int.MaxValue);
        }

        private void DestroyDungeon()
        {
            _generator.DestroyDungeon();
            Status = BuildStatus.Passive;
        }

        public override void Dispose()
        {
            // DestroyDungeon();
            base.Dispose();
        }
    }
}