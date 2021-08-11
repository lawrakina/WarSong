using System;
using Code.Data.Marker;
using Code.Data.Unit.Enemy;
using Code.Extension;
using Code.Profile.Models;
using Code.Unit.Factories;


namespace Code.Fight.BuildingDungeon
{
    public sealed class EnemyFightController : BaseController, IVerifiable
    {
        private readonly FightDungeonModel _generatorModel;
        private readonly DungeonGeneratorModel _dungeonGeneratorModel;
        private readonly EnemiesLevelModel _enemiesLevelModel;
        private readonly EnemiesData _enemySettings;

        private EnemyFactory _enemyFactory;
        private BuildStatus _status;

        public BuildStatus Status
        {
            get => _status;
            set => _status = value;
        }

        public event Action<IVerifiable> Complete = verifiable => { verifiable.Status = BuildStatus.Complete;};

        public EnemyFightController(FightDungeonModel generatorModel, DungeonGeneratorModel dungeonGeneratorModel,
            EnemiesLevelModel enemiesLevelModel, EnemiesData settings)
        {
            _generatorModel = generatorModel;
            _dungeonGeneratorModel = dungeonGeneratorModel;
            _enemiesLevelModel = enemiesLevelModel;
            _enemySettings = settings;

            _status = BuildStatus.Passive;
            _generatorModel.OnChangeEnemiesPositions += SpawnEnemies;
            _enemyFactory = new EnemyFactory(_enemySettings);
            AddController(_enemyFactory);
        }

        private void SpawnEnemies(SpawnMarkerEnemyInDungeon[] listEnemies)
        {
            _status = BuildStatus.Process;
            // Dbg.Log($"listEnemies.Length:{listEnemies.Length}");
            foreach (var marker in listEnemies)
            {
                var enemy = _enemyFactory.CreateEnemy(marker);
                _enemiesLevelModel.Enemies.Add(enemy);
                // Dbg.Log($"markers:{listEnemies.Length}, enemies:{_enemiesLevelModel.Enemies.Count}. {enemy.name}");
            }
            Complete?.Invoke(this);
        }
        
        protected override void OnDispose()
        {
            _generatorModel.OnChangeEnemiesPositions -= SpawnEnemies;
            base.OnDispose();
        }

    }
}