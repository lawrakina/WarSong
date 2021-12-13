using System.Collections.Generic;
using System.Linq;
using Code.Data.Marker;
using Code.Data.Unit.Enemy;
using Code.Equipment;
using Code.Extension;
using KinematicCharacterController;
using UnityEngine;


namespace Code.Unit.Factories{
    public class EnemyFactory : BaseController{
        private readonly EnemiesData _data;
        private int _baseLevel;
        private List<EnemyView> listOfEnemies = new List<EnemyView>();
        private ReputationFactory _reputationFactory;
        private NpcCharacteristicsFactory _chatacteristicFactory;
        private NpsHealthFactory _healthFactory;
        private NpsVisionFactory _visionFactory;
        private NpsBattleFactory _battleFactory;
        private NpsLevelFactory _levelFactory;

        public EnemyFactory(EnemiesData data, int baseLevel){
            _data = data;
            _baseLevel = baseLevel;
            _reputationFactory = new ReputationFactory();
            _chatacteristicFactory = new NpcCharacteristicsFactory(_data);
            _healthFactory = new NpsHealthFactory(_data);
            _visionFactory = new NpsVisionFactory(_data);
            _battleFactory = new NpsBattleFactory(_data);
            _levelFactory = new NpsLevelFactory(_data);
        }

        public EnemyView CreateEnemy(SpawnMarkerEnemyInDungeon marker){
            var enemySettings = _data.Enemies.FirstOrDefault(x => x.EnemyType == marker._type);
            var root = Object.Instantiate(_data.StorageRootPrefab);
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

            _baseLevel += Random.Range(0, enemySettings.LevelOffset);

            unit.UnitCharacteristics = _chatacteristicFactory.GenerateCharacteristics(enemySettings, _baseLevel);

            unit.UnitHealth = _healthFactory.GenerateHealth(enemySettings, _baseLevel);
            unit.UnitVision = _visionFactory.GenerateVision(enemySettings, unit.TransformModel);
            unit.UnitBattle = _battleFactory.GenerateBattle(enemySettings, unit.UnitCharacteristics);
            unit.UnitLevel = _levelFactory.GenerateLevel(enemySettings, _baseLevel);

            var healthBarSettings = _data.uiElement.First(x => (x.EnemyType == marker._type));
            unit.HealthBar = Object.Instantiate(healthBarSettings.UiView, unit.Transform, false);
            unit.HealthBar.transform.localPosition = healthBarSettings.Offset;

            unit.HealthBar.SetEnemyName(enemySettings.DisplayName);
            unit.HealthBar.SetEnemyLvl(_baseLevel);

            unit.Motor.SetPosition(marker.Transform.position, false);

            return unit;
        }
    }
}