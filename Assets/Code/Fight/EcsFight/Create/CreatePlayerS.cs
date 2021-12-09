using Code.Data;
using Code.Fight.EcsFight.Battle;
using Code.Fight.EcsFight.Settings;
using Code.GameCamera;
using Code.Profile.Models;
using Code.Unit;
using Leopotam.Ecs;
using SensorToolkit;
using UnityEngine;


namespace Code.Fight.EcsFight.Create{
    public class CreatePlayerS : IEcsInitSystem{
        private IPlayerView _player;
        private BattleCamera _camera;
        private readonly EcsWorld _world = null;
        private InOutControlFightModel _model;

        public void Init(){
            //player
            var entity = _world.NewEntity();
            entity.Get<PlayerTag>();
            entity.Get<AnimatorTag>();
            ref var unit = ref entity.Get<UnitC>();
            unit.UnitMovement = _player.UnitMovement;
            unit.Animator = _player.AnimatorParameters;
            unit.Characteristics = _player.UnitCharacteristics;
            unit.Health = _player.UnitHealth;
            unit.Resource = _player.UnitResource;
            unit.UnitVision = _player.UnitVision;
            unit.Reputation = _player.UnitReputation;
            unit.UnitLevel = _player.UnitLevel;

            //Weapon
            unit.InfoAboutWeapons = new ListWeapons();
            unit.InfoAboutWeapons.WeaponTypeAnimation = _player.UnitEquipment.GetWeaponType();
            // unit.InfoAboutWeapons.MeleeRangeSplash = _player.UnitBattle.MeleeRangeSplash();
            foreach (var unitBattleWeapon in _player.UnitBattle.Weapons){
                if (unitBattleWeapon.EquipType == EquipCellType.MainHand){
                    ref var mainWeapon = ref entity.Get<Weapon<MainHand>>();
                    mainWeapon.Value = unitBattleWeapon;
                    mainWeapon.Speed = unitBattleWeapon.Speed;
                    mainWeapon.Distance = unitBattleWeapon.Distance;
                    mainWeapon.LagBefAttack = Mathf.Abs(unitBattleWeapon.LagBeforeAttack);

                    unit.InfoAboutWeapons.AddMain(unitBattleWeapon);
                }

                if (unitBattleWeapon.EquipType == EquipCellType.SecondHand){
                    ref var secondWeapon = ref entity.Get<Weapon<SecondHand>>();
                    secondWeapon.Value = unitBattleWeapon;
                    secondWeapon.Speed = unitBattleWeapon.Speed;
                    secondWeapon.Distance = unitBattleWeapon.Distance;
                    secondWeapon.LagBefAttack = unitBattleWeapon.LagBeforeAttack;

                    unit.InfoAboutWeapons.AddSecond(unitBattleWeapon);
                }
            }

            //Ui
            _model.PlayerStats.MaxHp = Mathf.RoundToInt(_player.UnitHealth.CurrentHp);
            _model.PlayerStats.CurrentHp = Mathf.RoundToInt(_player.UnitHealth.CurrentHp);
            _model.PlayerStats.MaxResource = Mathf.RoundToInt(_player.UnitResource.ResourceBaseValue);
            _model.PlayerStats.CurrentResource = Mathf.RoundToInt(_player.UnitResource.ResourceBaseValue);

            //for collision 
            var playerEntity = new PlayerEntity(_player, entity);

            //SensorToolkit
            unit.UnitVision.Visor.enabled = true;
            ref var targets = ref entity.Get<TargetListC>();
            targets.Current = null;
            targets.List = unit.UnitVision.Visor.DetectedObjectsOrderedByDistance;
            unit.UnitVision.Visor.OnDetected.AddListener(OnDetected);
            unit.UnitVision.Visor.OnLostDetection.AddListener(OnLostDetection);
            //ToDo удалить привязку на событие

            //Camera
            entity.Get<CameraC>().Value = _camera;
            _camera.SetFollowTransform(_player.UnitMovement.CameraFollowPoint);
            _camera.IgnoredColliders.Clear();
            _camera.IgnoredColliders.AddRange(_player.Transform.GetComponentsInChildren<Collider>());
        }

        private void OnDetected(GameObject unit, Sensor sensor){
            if (unit.TryGetComponent<EnemyView>(out var enemy)){
                enemy.HealthBar.SetOnOff(true);
                _player.UnitVision.AddEnemy(enemy);
            }
        }

        private void OnLostDetection(GameObject unit, Sensor sensor){
            if (unit.TryGetComponent<EnemyView>(out var enemy)){
                enemy.HealthBar.SetOnOff(false);
                _player.UnitVision.DelEnemy(enemy);
            }
        }
    }
}