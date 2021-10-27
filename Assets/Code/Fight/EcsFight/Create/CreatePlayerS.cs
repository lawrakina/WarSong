using System.Collections.Generic;
using Code.Fight.EcsFight.Battle;
using Code.Profile.Models;
using Code.Unit;
using Leopotam.Ecs;
using UnityEngine;


namespace Code.Fight.EcsFight.Create{
    public class CreatePlayerS : IEcsInitSystem{
        private IPlayerView _player;
        private readonly EcsWorld _world = null;
        private InOutControlFightModel _model;

        public void Init(){
            //player
            var entity = _world.NewEntity();
            entity.Get<PlayerTag>();
            ref var unit = ref entity.Get<UnitC>();
            unit.RootTransform = _player.Transform;
            unit.ModelTransform = _player.TransformModel;
            unit.Rigidbody = _player.Rigidbody;
            unit.Animator = _player.AnimatorParameters;
            unit.Characteristics = _player.UnitCharacteristics;
            unit.Health = _player.UnitHealth;
            unit.Resource = _player.UnitResource;
            unit.UnitVision = _player.UnitVision;
            unit.Reputation = _player.UnitReputation;
            
            //weapons
            foreach (var unitBattleWeapon in _player.UnitBattle.Weapons){
                var entityWeapon = _world.NewEntity();
                ref var weapon = ref entityWeapon.Get<WeaponC>();
                weapon.Type = unitBattleWeapon.EquipType;
                weapon.Bullet = unitBattleWeapon.Bullet;
                weapon.Owner = unit;
                weapon.CanUse = false;
                weapon.CurrentCooldownTime = 0.0f;
                weapon.MaxCooldownTime = unitBattleWeapon.Speed;
            }

            //Ui
            _model.PlayerStats.MaxHp = Mathf.RoundToInt(_player.UnitHealth.CurrentHp);
            _model.PlayerStats.CurrentHp = Mathf.RoundToInt(_player.UnitHealth.CurrentHp);
            _model.PlayerStats.MaxResource = Mathf.RoundToInt(_player.UnitResource.ResourceBaseValue);
            _model.PlayerStats.CurrentResource = Mathf.RoundToInt(_player.UnitResource.ResourceBaseValue);

            //SensorToolkit
            unit.UnitVision.Sensor.enabled = true;
            ref var targets = ref entity.Get<TargetListC>();
            targets.Current = null;
            targets.List = unit.UnitVision.Sensor.DetectedObjectsOrderedByDistance;
            
            var directionMovement = new GameObject();
            directionMovement.transform.SetParent(_player.Transform, false);
            directionMovement.name = "->DirectionMoving<-";
            entity.Get<DirectionMovementC>().Value = directionMovement.transform;
        }
    }

    public struct TargetListC{
        public GameObject Current;
        public List<GameObject> List;
    }
}