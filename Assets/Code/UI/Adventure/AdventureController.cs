using System;
using Code.Data;
using Code.Extension;
using Code.Profile;
using DungeonArchitect;
using DungeonArchitect.Builders.GridFlow;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;


namespace Code.UI.Adventure
{
    public sealed class AdventureController : BaseController
    {
        private readonly Transform _placeForUi;
        private readonly ProfilePlayer _profilePlayer;
        private AdventureView _view;

        public AdventureController(Transform placeForUi, ProfilePlayer profilePlayer)
        {
            _placeForUi = placeForUi;
            _profilePlayer = profilePlayer;

           // перенести в FightController if (!LevelGenerator.IsOn)
               // перенести в FightController     _profilePlayer.LevelGenerator = new LevelGenerator(_profilePlayer.Settings.DungeonGeneratorData);

            _view = ResourceLoader.InstantiateObject(_profilePlayer.Settings.UiViews.AdventureView, _placeForUi,false);
            AddGameObjects(_view.gameObject);
            _view.Init(StartBattle);
        }

        private void StartBattle()
        {
            // _profilePlayer.Settings.DungeonGeneratorData.ModelGungeon.ActiveLevel = 
                // _profilePlayer.Settings.DungeonGeneratorData.ModelGungeon.DemoLevel;
            _profilePlayer.CurrentState.Value = GameState.Fight;
        }
    }

    public class LevelGenerator
    {
        private readonly DungeonGeneratorData _settings;
        private GameObject _dungeon;
        private GridFlowDungeonBuilder _builder;
        private readonly GridFlowDungeonConfig _config;
        private readonly Dungeon _generator;
        private readonly Transform _parent;
        private readonly PooledDungeonSceneProvider _pooledSceneProvider;
        private static bool _isOn = false;
        private bool isEnableDungeon;

        public static bool IsOn
        {
            get => _isOn;
            set => _isOn = value;
        }
        
        public LevelGenerator(DungeonGeneratorData settings)
        {
            _isOn = true;
            _settings = settings;
            
            _dungeon = Object.Instantiate(new GameObject(), new RectTransform());
            _dungeon.name = "---Dungeon";
            _dungeon.isStatic = true;

            var gameObjectGenerator = Object.Instantiate(_settings.StorageGenerator, _dungeon.transform, false);
            _generator = gameObjectGenerator.GetComponent<Dungeon>();
            _config = gameObjectGenerator.GetComponent<GridFlowDungeonConfig>();
            _builder = gameObjectGenerator.GetComponent<GridFlowDungeonBuilder>();
            _pooledSceneProvider = gameObjectGenerator.GetComponent<PooledDungeonSceneProvider>();
            _pooledSceneProvider.itemParent = _dungeon;
            _builder.asyncBuild = true;
        }

        public void GenerateDemoLevel()
        {
            DestroyDungeon();
            _config.Seed = (uint) Random.Range(0, int.MaxValue);
        }
        
        public void DestroyDungeon()
        {
            _generator.DestroyDungeon();
            isEnableDungeon = false;
        }
    }
}