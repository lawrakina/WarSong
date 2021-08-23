using System;
using Code.Data;
using Code.Extension;
using Code.Fight.EcsBattle.CustomEntities;
using Code.Profile.Models;
using Code.Unit;
using Leopotam.Ecs;
using UnityEngine;
using Object = UnityEngine.Object;


namespace Code.Fight.EcsBattle.Unit.Create
{
    public sealed class CreatePlayerEntitySystem : IEcsInitSystem
    {
        private EcsWorld _world;
        private IPlayerView _view;
        private InOutControlFightModel _model;

        public void Init()
        {
            var entity = _world.NewEntity();
            //Base components
            entity.Get<PlayerComponent>()._unitClass = _view.CharacterClass.Class;
            // entity.Get<PlayerComponent>()._view = _view;
            entity.Get<UnitComponent>()._view = _view;
            entity.Get<UnitComponent>()._modelTransform = _view.TransformModel;
            entity.Get<UnitComponent>()._rootTransform = _view.Transform;
            entity.Get<UnitComponent>()._rigidBody = _view.Rigidbody;
            entity.Get<UnitComponent>()._reputation = _view.UnitReputation;
            entity.Get<UnitComponent>()._vision = _view.UnitVision;
            // entity.Get<UnitComponent>()._attributes = _view.Attributes;
            entity.Get<UnitComponent>()._animator = _view.AnimatorParameters;
            entity.Get<UnitComponent>()._health = _view.UnitHealth;
            entity.Get<UnitComponent>()._characteristics = _view.UnitCharacteristics;
            //ui
            _model.PlayerStats.MaxHp = Mathf.RoundToInt(_view.UnitHealth.CurrentHp);
            _model.PlayerStats.CurrentHp = Mathf.RoundToInt(_view.UnitHealth.CurrentHp);

            //battle
            ref var mainWeapon = ref entity.Get<BattleInfoMainWeaponComponent>();

            void SetBattleInfoForMainWeapon(ref BattleInfoMainWeaponComponent weapon)
            {
                weapon._attackDistance = _view.UnitEquipment.MainWeapon.AttackValue.GetAttackDistance();
                weapon._bullet = _view.UnitEquipment.MainWeapon.StandardBullet;
                weapon._attackValue = _view.UnitCharacteristics.GetAttackMainWeaponValue();
                weapon._weaponTypeAnimation = _view.AnimatorParameters.WeaponType;
                //todo сделать отсылку в локальную БД и вытаскивать кол-во доступных анимаций по типу оружия
                weapon._attackMaxValueAnimation = 3;
            }

            Dbg.Log($"_view.UnitPlayerBattle.ActiveWeapons:{_view.UnitPerson.ActiveWeapons}");
            switch (_view.UnitPerson.ActiveWeapons)
            {
                case ActiveWeapons.RightHand:
                    SetBattleInfoForMainWeapon(ref mainWeapon);
                    break;

                case ActiveWeapons.RightAndShield:
                    SetBattleInfoForMainWeapon(ref mainWeapon);
                    break;


                case ActiveWeapons.RightAndLeft:
                    //right
                    mainWeapon._attackDistance = _view.UnitEquipment.MainWeapon.AttackValue.GetAttackDistance();
                    mainWeapon._bullet = _view.UnitEquipment.MainWeapon.StandardBullet;
                    mainWeapon._attackValue = _view.UnitCharacteristics.GetAttackMainWeaponValue();
                    //todo убрать магические числа, но это не обязательно  //_view.AnimatorParameters.WeaponType; // (int) _view.UnitPlayerBattle.Weapon.Type;
                    mainWeapon._weaponTypeAnimation = 1;
                    //todo сделать отсылку в локальную БД и вытаскивать кол-во доступных анимаций по типу оружия
                    mainWeapon._attackMaxValueAnimation = 4;
                    //left
                    ref var secondWeapon = ref entity.Get<BattleInfoSecondWeaponComponent>();
                    secondWeapon._attackDistance = _view.UnitEquipment.SecondWeapon.AttackValue.GetAttackDistance();
                    secondWeapon._bullet = _view.UnitEquipment.SecondWeapon.StandardBullet;
                    secondWeapon._attackValue = _view.UnitCharacteristics.GetAttackSecondWeaponValue();
                    //todo убрать магические числа, но это не обязательно  //_view.AnimatorParameters.WeaponType; // (int) _view.UnitPlayerBattle.Weapon.Type;
                    secondWeapon._weaponTypeAnimation = 2;
                    //todo сделать отсылку в локальную БД и вытаскивать кол-во доступных анимаций по типу оружия
                    secondWeapon._attackMaxValueAnimation = 4;
                    //todo в глобальные настройки персонажа - лаг перед ударом левой рукой
                    secondWeapon._lagBeforeAttack = 0.8f;
                    //todo в глобальные настройки персонажа - коэффициент силы удара
                    secondWeapon._powerFactor = 0.5f;
                    break;

                case ActiveWeapons.TwoHand:
                    SetBattleInfoForMainWeapon(ref mainWeapon);
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }

            var directionMovement = Object.Instantiate(new GameObject(), _view.Transform, true);
            directionMovement.name = "->DirectionMoving<-";
            entity.Get<DirectionMovementComponent>()._value = directionMovement.transform;

            //for collision 
            var playerEntity = new PlayerEntity(_view, entity);
        }
    }
}