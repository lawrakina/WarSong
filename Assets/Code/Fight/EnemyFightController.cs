using Code.Profile.Models;
using Code.Unit.Factories.EnemyFactories;
using Code.Data;
using Code.Data.Marker;
using Code.Extension;
using UnityEngine;


namespace Code.Fight
{
    public sealed class EnemyFightController: BaseController
    {
        private readonly FightDungeonModel _generatorModel;
        private readonly DungeonGeneratorModel _dungeonGeneratorModel;
        private readonly EnemiesLevelModel _enemiesLevelModel;
        private readonly EnemiesData _enemySettings;

        private EnemyFactory_temp _enemyFactory;


        public EnemyFightController(FightDungeonModel generatorModel, DungeonGeneratorModel dungeonGeneratorModel,
            EnemiesLevelModel enemiesLevelModel,
            EnemiesData settings)
        {
            _generatorModel = generatorModel;
            _dungeonGeneratorModel = dungeonGeneratorModel;
            _enemiesLevelModel = enemiesLevelModel;
            _enemySettings = settings;
            
            _generatorModel.OnChangeEnemiesPositions += SpawnEnemies;
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

        private void SpawnEnemies(SpawnMarkerEnemyInDungeon[] listEnemies)
        {
            Dbg.Log($"listEnemies.Length:{listEnemies.Length}");
            foreach (var marker in listEnemies)
            {
                _enemiesLevelModel.Enemies.Add(_enemyFactory.CreateEnemy(marker));
            }
        }

        // public void SpawnEnemies()
        // {
        //     //ToDo создавай врагов тут, бери настройки уровня в _dungeonGeneratorModel и после создания записывай их в _enemiesLevelModel.
        //     // тебе понадобятся фабрики по созданию врагов.
        //     //кокога черта здесь вылезает null reference когда я юзаю _enemiesLevelModel?????
        //     for (int i = 0; i < 20; i++)
        //     {
        //         _enemiesLevelModel.Enemies.Add(_enemyFactory.CreateEnemy());
        //         Debug.LogWarning(_enemiesLevelModel.Enemies[i]);
        //     }
        //     
        // }

        protected override void OnDispose()
        {
            _generatorModel.OnChangeEnemiesPositions -= SpawnEnemies;
            base.OnDispose();
        }
    }
}