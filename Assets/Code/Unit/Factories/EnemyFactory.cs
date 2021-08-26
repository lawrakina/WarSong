using System.Collections.Generic;
using System.Linq;
using Code.Data.Marker;
using Code.Data.Unit;
using Code.Data.Unit.Enemy;
using Code.Extension;
using UnityEngine;


namespace Code.Unit.Factories
{
    public class EnemyFactory : BaseController
    {
        private readonly EnemiesData _settings;
        private List<EnemyView> listOfEnemies = new List<EnemyView>();
        public EnemyFactory(EnemiesData settings)
        {
            _settings = settings;
          
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
            
            //test
            enemyView.UnitHealth = new UnitHealth();
            enemyView.UnitHealth.MaxHp = 100;
            enemyView.UnitHealth.CurrentHp = 100;
            
            
            //test
            
            // var equipmentPoints = new EquipmentPoints(enemyView.Transform.gameObject, item);
            // equipmentPoints.GenerateAllPoints();
            // enemyView.UnitEquipment = new UnitEquipment(equipmentPoints,item.unitEquipment);

            // var characteristics = new UnitCharacteristics();
            // characteristics.Speed = item.MoveSpeed;
            // characteristics.MinAttack = item.AttackValue..MoveSpeed;
            // characteristics.MaxAttack = item.MoveSpeed;

            var healthBarSettings = _settings.uiElement.First(x => (x.EnemyType == marker._type));
            enemyView.HealthBar = Object.Instantiate(healthBarSettings.UiView, enemyView.Transform, false);
            enemyView.HealthBar.transform.localPosition = healthBarSettings.Offset;
            
            //test name and level
            enemyView.HealthBar.SetEnemyName("Рандомный бомжик");

            var enemyLvl = Random.Range(1, 4);
            enemyView.HealthBar.SetEnemyLvl(enemyLvl);
            //test

            enemyView.Transform.SetParent(marker.Transform);
            enemyView.Transform.localPosition = Vector3.zero;
            enemyView.Transform.rotation = Quaternion.identity;
             
            return enemyView;
        }
    }
}