using System.Collections.Generic;
using System.Linq;
using Data;
using DungeonArchitect;
using DungeonArchitect.Builders.GridFlow;
using Extension;
using Unit;
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
        private IHealthBarFactory _healthBarFactory;

        #endregion


        #region ClassLiveCycles

        public EnemiesInitialization(EnemiesData enemiesData, IEnemyFactory enemyFactory, IHealthBarFactory healthBarFactory)
        {
            _enemiesData = enemiesData;
            _enemyFactory = enemyFactory;
            _healthBarFactory = healthBarFactory;
        }

        #endregion


        #region Methods

        public List<IEnemyView> GetListEnemies(List<SpawnMarkerEnemyInDungeon> list, GameObject parent)
        {
            var result = new List<IEnemyView>();
            
            foreach (var spawnPoint in list)
            {
                var itemSetting = _enemiesData.Enemies.ListEnemies.FirstOrDefault(
                    x => x.EnemyType == spawnPoint._type
                );
                var enemy = _enemyFactory.CreateEnemy(itemSetting);
                enemy.Transform.SetParent(spawnPoint.transform);
                enemy.Transform.localPosition = Vector3.zero;
                enemy.Transform.SetParent(parent.transform);

                //DungeonArchitect навязывает свои скрипты на всех заспавненных объектах,
                //сейчас это костыль для копирования скриптов
                //todo перенести функционал скриптов от DungeonArchitect во вью врагов.
                GameObjectExtension.CopyComponent(
                    spawnPoint.GetComponent<DungeonSceneProviderData>(), enemy.Transform.gameObject);
                GameObjectExtension.CopyComponent(
                    spawnPoint.GetComponent<GridFlowItemMetadataComponent>(), enemy.Transform.gameObject);
                
                //UI HealthBar
                var itemUiEnemy = _enemiesData.Enemies.ListEnemies.FirstOrDefault(
                    x => x.EnemyType == spawnPoint._type
                );
                var enemyHealthBar = _healthBarFactory.CreateHealthBar(itemUiEnemy.uiElement, enemy);
                enemy.HealthBar = enemyHealthBar;

                // Dbg.Log($"GetListEnemy.OneEnemy.UnitReputation.EnemyLayer:{enemy.UnitReputation.EnemyLayer}");
                result.Add(enemy);
            }

            return result;
        }

        #endregion
    }
}