using Code.Data.Dungeon;
using DungeonArchitect;
using DungeonArchitect.Builders.GridFlow;
using UnityEngine;


namespace Code.Fight
{
    public class LevelGeneratorController : BaseController
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
        
        public LevelGeneratorController(DungeonGeneratorData settings)
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
            _config.Seed = (uint) Random.Range(0, int.MaxValue);
        }
        
        public void DestroyDungeon()
        {
            _generator.DestroyDungeon();
            isEnableDungeon = false;
        }

        protected override void OnDispose()
        {
            DestroyDungeon();
            base.OnDispose();
        }
    }
}