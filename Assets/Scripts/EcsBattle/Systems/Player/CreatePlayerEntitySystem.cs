using System;
using Controller.Model;
using DungeonArchitect.Samples.GridFlow;
using EcsBattle.Components;
using EcsBattle.CustomEntities;
using Enums;
using Extension;
using Leopotam.Ecs;
using Models;
using Unit.Player;
using UnityEngine;
using Object = UnityEngine.Object;


namespace EcsBattle.Systems.Player
{
    public sealed class CreatePlayerEntitySystem : IEcsInitSystem
    {
        private EcsWorld _world;
        private IPlayerView _view;
        private BattlePlayerModel _playerModel;

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
            entity.Get<UnitComponent>()._attributes = _view.Attributes;
            entity.Get<UnitComponent>()._animator = _view.AnimatorParameters;
            entity.Get<UnitComponent>()._health = _view.UnitHealth;
            entity.Get<UnitComponent>()._characteristics = _view.UnitCharacteristics;
            //ui
            _playerModel.MaxHp = Mathf.RoundToInt(_view.UnitHealth.CurrentHp);
            _playerModel.CurrentHp = Mathf.RoundToInt(_view.UnitHealth.CurrentHp);

            //battle
            ref var mainWeapon = ref entity.Get<BattleInfoMainWeaponComponent>();

            void SetBattleInfoForMainWeapon(ref BattleInfoMainWeaponComponent weapon)
            {
                weapon._value = _view.UnitPlayerBattle.MainWeapon;
                weapon._bullet = _view.UnitPlayerBattle.MainWeapon.StandardBullet;
                weapon._attackValue = _view.UnitPlayerBattle.MainWeapon.AttackValue;
                weapon._weaponTypeAnimation = _view.AnimatorParameters.WeaponType;
                //todo сделать отсылку в локальную БД и вытаскивать кол-во доступных анимаций по типу оружия
                weapon._attackMaxValueAnimation = 3;
            }

            Dbg.Log($"_view.UnitPlayerBattle.ActiveWeapons:{_view.UnitPlayerBattle.ActiveWeapons}");
            switch (_view.UnitPlayerBattle.ActiveWeapons)
            {
                case ActiveWeapons.RightHand:
                    SetBattleInfoForMainWeapon(ref mainWeapon);
                    break;

                case ActiveWeapons.RightAndShield:
                    SetBattleInfoForMainWeapon(ref mainWeapon);
                    break;


                case ActiveWeapons.RightAndLeft:
                    //right
                    mainWeapon._value = _view.UnitPlayerBattle.MainWeapon;
                    mainWeapon._bullet = _view.UnitPlayerBattle.MainWeapon.StandardBullet;
                    mainWeapon._attackValue = _view.UnitPlayerBattle.MainWeapon.AttackValue;
                    //todo убрать магические числа, но это не обязательно  //_view.AnimatorParameters.WeaponType; // (int) _view.UnitPlayerBattle.Weapon.Type;
                    mainWeapon._weaponTypeAnimation = 1;
                    //todo сделать отсылку в локальную БД и вытаскивать кол-во доступных анимаций по типу оружия
                    mainWeapon._attackMaxValueAnimation = 4;
                    //left
                    ref var secondWeapon = ref entity.Get<BattleInfoSecondWeaponComponent>();
                    secondWeapon._value = _view.UnitPlayerBattle.SecondWeapon;
                    secondWeapon._bullet = _view.UnitPlayerBattle.SecondWeapon.StandardBullet;
                    secondWeapon._attackValue = _view.UnitPlayerBattle.SecondWeapon.AttackValue;
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

            var inventory = Object.Instantiate(new GameObject("KeyChain"), _view.Transform);
            var inventoryForKeys = inventory.AddCode<Inventory>();
            inventoryForKeys.slots = new InventorySlot[4];
            inventoryForKeys.slots[0] = Object.Instantiate(new GameObject("slot1"), inventory.transform)
                                              .AddCode<InventorySlot>();
            inventoryForKeys.slots[0].item = new InventoryItem();
            inventoryForKeys.slots[1] = Object.Instantiate(new GameObject("slot2"), inventory.transform)
                                              .AddCode<InventorySlot>();
            inventoryForKeys.slots[1].item = new InventoryItem();
            inventoryForKeys.slots[2] = Object.Instantiate(new GameObject("slot3"), inventory.transform)
                                              .AddCode<InventorySlot>();
            inventoryForKeys.slots[2].item = new InventoryItem();
            inventoryForKeys.slots[3] = Object.Instantiate(new GameObject("slot4"), inventory.transform)
                                              .AddCode<InventorySlot>();
            inventoryForKeys.slots[3].item = new InventoryItem();
        }
    }
}