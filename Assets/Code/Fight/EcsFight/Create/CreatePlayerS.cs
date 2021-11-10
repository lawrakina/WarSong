using System.Collections.Generic;
using Code.Data;
using Code.Data.Unit;
using Code.Fight.EcsFight.Battle;
using Code.GameCamera;
using Code.Profile.Models;
using Code.Unit;
using Leopotam.Ecs;
using SensorToolkit;
using UnityEngine;
using UnityEngine.Events;


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
            // unit.RootTransform = _player.Transform;
            // unit.ModelTransform = _player.TransformModel;
            unit.Animator = _player.AnimatorParameters;
            unit.Characteristics = _player.UnitCharacteristics;
            unit.Health = _player.UnitHealth;
            unit.Resource = _player.UnitResource;
            unit.UnitVision = _player.UnitVision;
            unit.Reputation = _player.UnitReputation;
            unit.UnitLevel = _player.UnitLevel;
            unit.Weapons = new ListWeapons();
            //Weapons
            foreach (var unitBattleWeapon in _player.UnitBattle.Weapons){
                if(unitBattleWeapon.EquipType == EquipCellType.MainHand)
                    unit.Weapons.AddMain(unitBattleWeapon);
                if (unitBattleWeapon.EquipType == EquipCellType.SecondHand)
                    unit.Weapons.AddSecond(unitBattleWeapon);
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


    public class ListWeapons : List<Weapon>{
        private Weapon _main;
        private Weapon _second;
        public float SqrDistance{
            get{
                if (!Equals(_main, null)){
                    return _main.Distance * _main.Distance;
                }
                if (!Equals(_second, null)){
                    return _second.Distance * _second.Distance;
                }

                return 1.0f;
            }
        }

        public void AddMain(Weapon weapon){
            _main = weapon;
            Add(weapon);
        }

        public void AddSecond(Weapon weapon){
            _second = weapon;
            Add(weapon);
        }
    }
}