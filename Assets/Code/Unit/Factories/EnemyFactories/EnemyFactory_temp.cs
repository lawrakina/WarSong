using System.Collections.Generic;
using System.Linq;
using Code.Profile.Models;
using Code.Data;
using Code.Data.Marker;
using Code.Extension;
using JetBrains.Annotations;
using UnityEngine;
using EnemyType = Enums.EnemyType;


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
            Dbg.Log($"{_settings.Enemies}");
            //тут количество врагов на уровне???
            //вот это вот тоже хз
            
            //TODO: дебаги убрать нахер когда закончишь!!
            // Debug.LogError("Here");
            
            var enemyObject = GameObject.Instantiate(_settings.Enemies[0].EnemyView);
            var enemyObjectView = enemyObject.AddComponent<EnemyView>();

            enemyObjectView.Transform = enemyObject.transform;
            enemyObjectView.Animator = enemyObject.GetComponent<Animator>();
            enemyObjectView.AnimatorParameters = new AnimatorParameters(enemyObjectView.Animator);
            
            //Можно вынести в отдельный класс-фабрику
            CapsuleCollider EnemyCollider = enemyObject.AddComponent(typeof(CapsuleCollider)) as CapsuleCollider;
            EnemyCollider.height = 2.0f;
            EnemyCollider.radius = 0.33f;
            EnemyCollider.center = new Vector3(0.0f, 1.0f, 0.0f);
            enemyObjectView.Collider = EnemyCollider;

            enemyObjectView.Rigidbody = enemyObject.GetComponentInChildren<Rigidbody>();
            enemyObjectView.MeshRenderer = enemyObject.GetComponentInChildren<MeshRenderer>();
            enemyObjectView.HealthBar = _settings.Enemies[0].uiElement.UiView.GetComponent<HealthBarView>();
            //Не оч понятно что конкретно нужно заполнять во вью врага??
            //Не оч понятно где взять список точек спавна чтоб расставить их на уровне???
            
            //Как отсюда получить тот самый геймобжект Dungeon с его детьми??
            return enemyObjectView;
        }

        public EnemyView CreateEnemy(SpawnMarkerEnemyInDungeon marker)
        {
            var item = _settings.Enemies.FirstOrDefault(x => x.EnemyType == marker._type);
            var enemy = Object.Instantiate(item.EnemyView);
             enemy.name = $"Enemy.{item.EnemyType.ToString()}.{item.EnemyView.name}";
             var enemyView = enemy.AddCode<EnemyView>();
             enemyView.Animator = enemy.GetComponent<Animator>();
             enemyView.Transform = enemy.transform;
             enemyView.Rigidbody = enemy.AddRigidBody(80, CollisionDetectionMode.ContinuousSpeculative,
                 false, true,
                 RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY |
                 RigidbodyConstraints.FreezeRotationZ);
             enemyView.Collider = enemy.AddCapsuleCollider(0.5f, false,
                 new Vector3(0.0f, 0.9f, 0.0f),
                 1.8f);
             enemyView.MeshRenderer = enemy.GetComponent<MeshRenderer>();
             enemyView.AnimatorParameters = new AnimatorParameters(enemyView.Animator);
             
             enemyView.Transform.SetParent(marker.Transform);
             enemyView.Transform.localPosition = Vector3.zero;
             enemyView.Transform.rotation = Quaternion.identity;
             
             return enemyView;
        }
    }
}