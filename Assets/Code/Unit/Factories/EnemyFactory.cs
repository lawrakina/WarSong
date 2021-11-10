using System.Collections.Generic;
using System.Linq;
using Code.Data.Marker;
using Code.Data.Unit;
using Code.Data.Unit.Enemy;
using Code.Extension;
using KinematicCharacterController;
using UnityEngine;


namespace Code.Unit.Factories
{
    public class EnemyFactory : BaseController
    {
        private readonly EnemiesData _settings;
        private List<EnemyView> listOfEnemies = new List<EnemyView>();
        private ReputationFactory _reputationFactory;

        public EnemyFactory(EnemiesData settings)
        {
            _settings = settings;
            _reputationFactory = new ReputationFactory();
        }

        public EnemyView CreateEnemy(SpawnMarkerEnemyInDungeon marker)
        {
            var enemySettings = _settings.Enemies.FirstOrDefault(x => x.EnemyType == marker._type);
            var root = Object.Instantiate(_settings.StorageRootPrefab);
            root.name = $"Enemy.{enemySettings.EnemyType.ToString()}.{enemySettings.EnemyView.name}";
            var unit = root.AddCode<EnemyView>();
            unit.Transform = root.transform;
            unit.Motor = root.GetComponent<KinematicCharacterMotor>();
            unit.UnitMovement = root.GetComponent<UnitMovement>();
            unit.Collider = root.GetComponent<CapsuleCollider>();

            var unitModel = Object.Instantiate(enemySettings.EnemyView, unit.UnitMovement.MeshRoot, true);
            unitModel.name = $"Prefab.Model";

            unit.TransformModel = unitModel.transform;
            unit.MeshRenderer = unitModel.GetComponent<MeshRenderer>();
            unit.Animator = unitModel.GetComponent<Animator>();
            unit.Animator.enabled = true;
            unit.AnimatorParameters = new AnimatorParameters(unit.Animator);

            unit.UnitReputation = _reputationFactory.GenerateEnemyReputation();
            unit.gameObject.layer = unit.UnitReputation.FriendLayer;
            unit.tag = TagManager.TAG_ENEMY;
            
            unit.UnitHealth = new UnitHealth();
            unit.UnitHealth.MaxHp = 100;
            unit.UnitHealth.CurrentHp = 100;

            // enemyView.CharacterVisionData = new CharacterVisionData();
            // enemyView.CharacterVisionData.distanceDetection = 15.0f;
            
            //test
            
            // var equipmentPoints = new EquipmentPoints(enemyView.Transform.gameObject, item);
            // equipmentPoints.GenerateAllPoints();
            // enemyView.UnitEquipment = new UnitEquipment(equipmentPoints,item.unitEquipment);

            // var characteristics = new UnitCharacteristics();
            // characteristics.Speed = item.MoveSpeed;
            // characteristics.MinAttack = item.AttackValue..MoveSpeed;
            // characteristics.MaxAttack = item.MoveSpeed;

            var healthBarSettings = _settings.uiElement.First(x => (x.EnemyType == marker._type));
            unit.HealthBar = Object.Instantiate(healthBarSettings.UiView, unit.Transform, false);
            unit.HealthBar.transform.localPosition = healthBarSettings.Offset;
            
            //test name and level
            unit.HealthBar.SetEnemyName("Рандомный бомжик");

            var enemyLvl = Random.Range(1, 4);
            unit.HealthBar.SetEnemyLvl(enemyLvl);
            //test

            unit.Motor.SetPosition(marker.Transform.position, false);
             
            return unit;
        }
    }
}