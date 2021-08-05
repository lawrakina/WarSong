using System.Collections.Generic;
using Code.Profile.Models;
using Code.Data;
using JetBrains.Annotations;
using Unit;
using UnityEngine;

namespace Code.Unit.Factories.EnemyFactories
{
    public class EnemyFactory_temp : BaseController
    {
        private readonly EnemiesData _settings;
        private List<EnemyView> listOfEnemies = new List<EnemyView>();
        public EnemyFactory_temp(EnemiesData settings)
        {
            _settings = settings;
          
        }

        [ItemCanBeNull]
        public EnemyView CreateEnemy()
        {
            Debug.LogError(_settings.Enemies[0].EnemyView);
            //тут количество врагов на уровне???
            //вот это вот тоже хз
            var enemyList = new List<EnemyView>();
            Debug.LogError("Here");
            var enemyObject = GameObject.Instantiate(_settings.Enemies[0].EnemyView);
            var enemyObjectView = enemyObject.AddComponent<EnemyView>();

            enemyObjectView.Transform = enemyObject.transform;
            enemyObjectView.Animator = enemyObject.GetComponent<Animator>();
            enemyObjectView.Collider = enemyObject.AddComponent<CapsuleCollider>();
            
            enemyObjectView.Rigidbody = enemyObject.GetComponentInChildren<Rigidbody>();
            enemyObjectView.MeshRenderer = enemyObject.GetComponentInChildren<MeshRenderer>();
            enemyObjectView.HealthBar = _settings.Enemies[0].uiElement.UiView.GetComponent<HealthBarView>();
            //Не оч понятно что конкретно нужно заполнять во вью врага??
            //Не оч понятно где взять список точек спавна чтоб расставить их на уровне???
            return enemyObjectView;
        }
    }
}