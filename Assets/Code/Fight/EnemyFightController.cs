using Code.Profile.Models;
using Code.Unit.Factories.EnemyFactories;
using Code.Data;
using UnityEngine;


namespace Code.Fight
{
    public sealed class EnemyFightController: BaseController
    {
        private readonly DungeonGeneratorModel _dungeonGeneratorModel;
        private readonly EnemiesLevelModel _enemiesLevelModel;
        private readonly EnemiesData _enemySettings;

        private EnemyFactory_temp _enemyFactory;


        public EnemyFightController(DungeonGeneratorModel dungeonGeneratorModel, EnemiesLevelModel enemiesLevelModel,
            EnemiesData settings)
        {
            _dungeonGeneratorModel = dungeonGeneratorModel;
            _enemiesLevelModel = enemiesLevelModel;

            _enemySettings = settings;
            _enemyFactory = new EnemyFactory_temp(_enemySettings);
            //цифра временная для теста
            // for (int i = 0; i < 20; i++)
            // {
            //     
            //     Debug.LogError(_enemiesLevelModel.Enemies[i]);
            // }

            Debug.LogError("EnemyFightController");
            
            AddController(_enemyFactory);
        }

        public void SpawnEnemies()
        {
            //ToDo создавай врагов тут, бери настройки уровня в _dungeonGeneratorModel и после создания записывай их в _enemiesLevelModel.
            // тебе понадобятся фабрики по созданию врагов.
            //кокога черта здесь вылезает null reference когда я юзаю _enemiesLevelModel?????
            for (int i = 0; i < 20; i++)
            {
                _enemiesLevelModel.Enemies.Add(_enemyFactory.CreateEnemy());
                Debug.LogWarning(_enemiesLevelModel.Enemies[i]);
            }
            
        }
    }
}