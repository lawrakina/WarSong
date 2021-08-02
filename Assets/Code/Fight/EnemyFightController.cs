using System.Linq;
using Code.Data.Dungeon;
using Code.Profile;
using Code.Profile.Models;
using Code.Unit.Factories.EnemyFactories;
using Data;
using UnityEngine;
using EnemiesData = Code.Data.Unit.Enemy.EnemiesData;


namespace Code.Fight
{
    public sealed class EnemyFightController: BaseController
    {
        private readonly DungeonGeneratorModel _dungeonGeneratorModel;
        private readonly EnemiesLevelModel _enemiesLevelModel;
        private readonly EnemiesData _enemySettings;

        private EnemyFactory_temp _enemyFactory;
        

        public EnemyFightController(DungeonGeneratorModel dungeonGeneratorModel, EnemiesLevelModel enemiesLevelModel, EnemiesData settings)
        {
            _dungeonGeneratorModel = dungeonGeneratorModel;
            _enemiesLevelModel = enemiesLevelModel;
            
            _enemySettings = settings;
            _enemyFactory = new EnemyFactory_temp(_enemySettings);
            AddController(_enemyFactory);
        }

        public void SpawnEnemies()
        {
            //ToDo создавай врагов тут, бери настройки уровня в _dungeonGeneratorModel и после создания записывай их в _enemiesLevelModel.
            // тебе понадобятся фабрики по созданию врагов.
            _enemiesLevelModel.Enemies = _enemyFactory.CreateEnemies();
            Debug.LogWarning(_enemyFactory.CreateEnemies());

        }
    }
}