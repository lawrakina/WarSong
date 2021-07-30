using Code.Data.Dungeon;
using DungeonArchitect;
using DungeonArchitect.Builders.GridFlow;
using UnityEngine;


namespace Code.Fight
{
    public class LevelGeneratorController : BaseController, IExecute
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
        private bool _buildingProcess;
        private bool _buildingComplete;

        public static bool IsOn
        {
            get => _isOn;
            set => _isOn = value;
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
            
            GenerateDemoLevel();
            BuildDungeon();
        }

        private void BuildDungeon()
        {
            _generator.Build();
            _buildingProcess = true;
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