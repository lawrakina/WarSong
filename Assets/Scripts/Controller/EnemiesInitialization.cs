using System.Collections.Generic;
using System.Linq;
using Data;
using Unit.Enemies;
using UnityEngine;
using VIew;


namespace Controller
{
    public class EnemiesInitialization
    {
        #region fields

        private readonly EnemiesData _enemiesData;
        private readonly IEnemyFactory _enemyFactory;

        #endregion


        #region ClassLiveCycles

        public EnemiesInitialization(EnemiesData enemiesData, IEnemyFactory enemyFactory)
        {
            _enemiesData = enemiesData;
            _enemyFactory = enemyFactory;
        }

        #endregion


        #region Methods

        public List<IEnemyView> GetListEnemies(List<SpawnMarkerEnemyInDungeon> list)
        {
            var result = new List<IEnemyView>();
            foreach (var spawnPoint in list)
            {
                var itemSetting = _enemiesData._enemies.ListEnemies.FirstOrDefault(
                    x => x.EnemyType == spawnPoint._type
                );
                var enemy = _enemyFactory.CreateEnemy(itemSetting);
                enemy.Transform.SetParent(spawnPoint.transform);
                enemy.Transform.localPosition = Vector3.zero;
                
                result.Add(enemy);
            }
            return result;
        }

        #endregion
    }
}