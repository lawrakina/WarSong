﻿using Code.Fight.EcsFight.Battle;
using Code.Fight.EcsFight.Output;
using Code.Fight.EcsFight.Settings;
using Code.GameCamera;
using Code.Profile.Models;
using Leopotam.Ecs;
using UnityEngine;


namespace Code.Fight.EcsFight.Create{
    public class CreateEnemiesS : IEcsInitSystem{
        private EcsWorld _world;
        private EnemiesLevelModel _enemiesModel;
        private BattleCamera _camera;
        private InOutControlFightModel _model;

        public void Init(){
            foreach (var view in _enemiesModel.Enemies){
                view.HealthBar.SetCamera(_camera.transform);

                var entity = _world.NewEntity();
                entity.Get<EnemyTag>();
                entity.Get<AnimatorTag>();
                ref var unit = ref entity.Get<UnitC>();
                unit.UnitMovement = view.UnitMovement;
                unit.Animator = view.AnimatorParameters;
                unit.Characteristics = view.UnitCharacteristics;
                unit.Health = view.UnitHealth;
                unit.Resource = view.UnitResource;
                unit.UnitVision = view.UnitVision;
                unit.Reputation = view.UnitReputation;
                unit.UnitLevel = view.UnitLevel;
                unit.InfoAboutWeapons = new ListWeapons();
                unit.InfoAboutWeapons.WeaponTypeAnimation = view.UnitBattle.GetMainWeaponType();
                
                ref var weapon = ref entity.Get<MainWeaponC>();
                var unitBattleWeapon = view.UnitBattle.Weapons[0]; 
                weapon.Value = unitBattleWeapon;
                weapon.Speed = unitBattleWeapon.Speed;
                weapon.Distance = unitBattleWeapon.Distance;
                weapon.LagBefAttack = Mathf.Abs(unitBattleWeapon.LagBeforeAttack);
                unit.InfoAboutWeapons.AddMain(unitBattleWeapon);
                
                entity.Get<UiEnemyHealthBarC>().value = view.HealthBar;
                
                view.HealthBar.SetOnOff(false);

                var enemyEntity = new EnemyEntity(view, entity);
            }
        }
    }
}