using Code.Data.Marker;
using Code.Data.Unit.Enemy;
using Code.Extension;
using Code.Profile;
using Code.Profile.Models;
using Code.Unit.Factories;


namespace Code.Fight
{
    public sealed class EnemyFightController : BaseController
    {
        private readonly FightDungeonModel _generatorModel;
        private readonly DungeonGeneratorModel _dungeonGeneratorModel;
        private readonly EnemiesLevelModel _enemiesLevelModel;
        private readonly EnemiesData _enemySettings;

        private EnemyFactory _enemyFactory;

        public EnemyFightController(FightDungeonModel generatorModel, DungeonGeneratorModel dungeonGeneratorModel,
            EnemiesLevelModel enemiesLevelModel, EnemiesData settings)
        {
            _generatorModel = generatorModel;
            _dungeonGeneratorModel = dungeonGeneratorModel;
            _enemiesLevelModel = enemiesLevelModel;

            _enemySettings = settings;

            _generatorModel.OnChangeEnemiesPositions += SpawnEnemies;
            _enemyFactory = new EnemyFactory(_enemySettings);
            AddController(_enemyFactory);
        }

        private void SpawnEnemies(SpawnMarkerEnemyInDungeon[] listEnemies)
        {
            Dbg.Log($"listEnemies.Length:{listEnemies.Length}");
            foreach (var marker in listEnemies)
            {
                _enemiesLevelModel.Enemies.Add(_enemyFactory.CreateEnemy(marker));
            }
        }
        
        protected override void OnDispose()
        {
            _generatorModel.OnChangeEnemiesPositions -= SpawnEnemies;
            base.OnDispose();
        }
    }
}