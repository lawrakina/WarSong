using System;
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
            entity.Get<UnitComponent>()._view =_view; 
            entity.Get<UnitComponent>()._modelTransform =_view.TransformModel; 
            entity.Get<UnitComponent>()._rootTransform =_view.Transform; 
            entity.Get<UnitComponent>()._rigidBody = _view.Rigidbody;
            entity.Get<UnitComponent>()._reputation = _view.UnitReputation;
            entity.Get<UnitComponent>()._vision = _view.UnitVision;
            entity.Get<UnitComponent>()._attributes = _view.Attributes;
            entity.Get<UnitComponent>()._animator = _view.AnimatorParameters;
            entity.Get<UnitComponent>()._health = _view.UnitHealth;
            //ui
            _playerModel.MaxHp = Mathf.RoundToInt(_view.UnitHealth.CurrentHp);
            _playerModel.CurrentHp = Mathf.RoundToInt(_view.UnitHealth.CurrentHp);
            
            // entity.Get<UnitComponent>()._currentHpValue = _view.CurrentHp;
            // entity.Get<UnitComponent>()._maxHpValue = _view.CurrentHp;
            //battle
            void SetBattleInfoForMainWeapon()
            {
                entity.Get<BattleInfoMainWeaponComponent>()._value = _view.UnitPlayerBattle.MainWeapon;
                entity.Get<BattleInfoMainWeaponComponent>()._bullet = _view.UnitPlayerBattle.MainWeapon.StandardBullet;
                entity.Get<BattleInfoMainWeaponComponent>()._attackValue = _view.UnitPlayerBattle.MainWeapon.AttackValue;
                entity.Get<BattleInfoMainWeaponComponent>()._weaponTypeAnimation = _view.AnimatorParameters.WeaponType; // (int) _view.UnitPlayerBattle.Weapon.Type;
                entity.Get<BattleInfoMainWeaponComponent>()._attackMaxValueAnimation = 3;//todo сделать отсылку в локальную БД и вытаскивать кол-во доступных анимаций по типу оружия
            }
            switch (_view.UnitPlayerBattle.ActiveWeapons)
            {
                case ActiveWeapons.RightHand:
                    SetBattleInfoForMainWeapon();
                    break;

                case ActiveWeapons.TwoHand:
                    SetBattleInfoForMainWeapon();
                    break;

                case ActiveWeapons.RightAndLeft:
                    //right
                    entity.Get<BattleInfoMainWeaponComponent>()._value = _view.UnitPlayerBattle.MainWeapon;
                    entity.Get<BattleInfoMainWeaponComponent>()._bullet = _view.UnitPlayerBattle.MainWeapon.StandardBullet;
                    entity.Get<BattleInfoMainWeaponComponent>()._attackValue = _view.UnitPlayerBattle.MainWeapon.AttackValue;
                    entity.Get<BattleInfoMainWeaponComponent>()._weaponTypeAnimation = 1;//todo убрать магические числа, но это не обязательно  //_view.AnimatorParameters.WeaponType; // (int) _view.UnitPlayerBattle.Weapon.Type;
                    entity.Get<BattleInfoMainWeaponComponent>()._attackMaxValueAnimation = 4;//todo сделать отсылку в локальную БД и вытаскивать кол-во доступных анимаций по типу оружия
                    //left
                    entity.Get<BattleInfoSecondWeaponComponent>()._value = _view.UnitPlayerBattle.SecondWeapon;
                    entity.Get<BattleInfoSecondWeaponComponent>()._bullet = _view.UnitPlayerBattle.SecondWeapon.StandardBullet;
                    entity.Get<BattleInfoSecondWeaponComponent>()._attackValue = _view.UnitPlayerBattle.SecondWeapon.AttackValue;
                    entity.Get<BattleInfoSecondWeaponComponent>()._weaponTypeAnimation = 2;//todo убрать магические числа, но это не обязательно  //_view.AnimatorParameters.WeaponType; // (int) _view.UnitPlayerBattle.Weapon.Type;
                    entity.Get<BattleInfoSecondWeaponComponent>()._attackMaxValueAnimation = 4;//todo сделать отсылку в локальную БД и вытаскивать кол-во доступных анимаций по типу оружия
                    entity.Get<BattleInfoSecondWeaponComponent>()._lagBeforeAttack = 0.5f; //todo в глобальные настройки персонажа - лаг перед ударом левой рукой
                    entity.Get<BattleInfoSecondWeaponComponent>()._powerFactor = 0.5f; //todo в глобальные настройки персонажа - коэффициент силы удара
                    break;

                case ActiveWeapons.RightAndShield:
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
            inventoryForKeys.slots[0] = Object.Instantiate(new GameObject("slot1"),inventory.transform).AddCode<InventorySlot>();
            inventoryForKeys.slots[0].item = new InventoryItem();
            inventoryForKeys.slots[1] = Object.Instantiate(new GameObject("slot2"),inventory.transform).AddCode<InventorySlot>();
            inventoryForKeys.slots[1].item = new InventoryItem();
            inventoryForKeys.slots[2] = Object.Instantiate(new GameObject("slot3"),inventory.transform).AddCode<InventorySlot>();
            inventoryForKeys.slots[2].item = new InventoryItem();
            inventoryForKeys.slots[3] = Object.Instantiate(new GameObject("slot4"),inventory.transform).AddCode<InventorySlot>();
            inventoryForKeys.slots[3].item = new InventoryItem();
        }
    }
}