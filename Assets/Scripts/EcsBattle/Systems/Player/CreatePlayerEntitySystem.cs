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
            entity.Get<PlayerComponent>(); 
            entity.Get<UnitComponent>().modelTransform =_view.TransformModel; 
            entity.Get<UnitComponent>().rootTransform =_view.Transform; 
            entity.Get<UnitComponent>().rigidbody = _view.Rigidbody;
            entity.Get<UnitComponent>().reputation = _view.UnitReputation;
            entity.Get<UnitComponent>().vision = _view.UnitVision;
            entity.Get<UnitComponent>().attributes = _view.Attributes;
            entity.Get<UnitComponent>().animator = _view.AnimatorParameters;
            //ui
            _playerModel.MaxHp = Mathf.RoundToInt(_view.CurrentHp);
            _playerModel.CurrentHp = Mathf.RoundToInt(_view.CurrentHp);
            
            entity.Get<UnitHpComponent>().CurrentValue = _view.CurrentHp;
            entity.Get<UnitHpComponent>().MaxValue = _view.CurrentHp;
            //battle
            void SetBattleInfoForMainWeapon()
            {
                entity.Get<BattleInfoMainWeaponComponent>().Value = _view.UnitPlayerBattle.MainWeapon;
                entity.Get<BattleInfoMainWeaponComponent>().Bullet = _view.UnitPlayerBattle.MainWeapon.StandardBullet;
                entity.Get<BattleInfoMainWeaponComponent>().AttackValue = _view.UnitPlayerBattle.MainWeapon.AttackValue;
                entity.Get<BattleInfoMainWeaponComponent>().WeaponTypeAnimation = _view.AnimatorParameters.WeaponType; // (int) _view.UnitPlayerBattle.Weapon.Type;
                entity.Get<BattleInfoMainWeaponComponent>().AttackMaxValueAnimation = 3;//todo сделать отсылку в локальную БД и вытаскивать кол-во доступных анимаций по типу оружия
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
                    entity.Get<BattleInfoMainWeaponComponent>().Value = _view.UnitPlayerBattle.MainWeapon;
                    entity.Get<BattleInfoMainWeaponComponent>().Bullet = _view.UnitPlayerBattle.MainWeapon.StandardBullet;
                    entity.Get<BattleInfoMainWeaponComponent>().AttackValue = _view.UnitPlayerBattle.MainWeapon.AttackValue;
                    entity.Get<BattleInfoMainWeaponComponent>().WeaponTypeAnimation = 1;//todo убрать магические числа, но это не обязательно  //_view.AnimatorParameters.WeaponType; // (int) _view.UnitPlayerBattle.Weapon.Type;
                    entity.Get<BattleInfoMainWeaponComponent>().AttackMaxValueAnimation = 4;//todo сделать отсылку в локальную БД и вытаскивать кол-во доступных анимаций по типу оружия
                    //left
                    entity.Get<BattleInfoSecondWeaponComponent>().Value = _view.UnitPlayerBattle.SecondWeapon;
                    entity.Get<BattleInfoSecondWeaponComponent>().Bullet = _view.UnitPlayerBattle.SecondWeapon.StandardBullet;
                    entity.Get<BattleInfoSecondWeaponComponent>().AttackValue = _view.UnitPlayerBattle.SecondWeapon.AttackValue;
                    entity.Get<BattleInfoSecondWeaponComponent>().WeaponTypeAnimation = 2;//todo убрать магические числа, но это не обязательно  //_view.AnimatorParameters.WeaponType; // (int) _view.UnitPlayerBattle.Weapon.Type;
                    entity.Get<BattleInfoSecondWeaponComponent>().AttackMaxValueAnimation = 4;//todo сделать отсылку в локальную БД и вытаскивать кол-во доступных анимаций по типу оружия
                    entity.Get<BattleInfoSecondWeaponComponent>().lagBeforeAttack = 0.5f; //todo в глобальные настройки персонажа - лаг перед ударом левой рукой
                    entity.Get<BattleInfoSecondWeaponComponent>().powerFactor = 0.5f; //todo в глобальные настройки персонажа - коэффициент силы удара
                    break;

                case ActiveWeapons.RightAndShield:
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }
            
            var directionMovement = Object.Instantiate(new GameObject(), _view.Transform, true);
            directionMovement.name = "->DirectionMoving<-";
            entity.Get<DirectionMovementComponent>().value = directionMovement.transform;
            
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