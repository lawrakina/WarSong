using EcsBattle.Components;
using EcsBattle.CustomEntities;
using Leopotam.Ecs;
using Models;
using Unit;
using Unit.Player;
using UnityEngine;


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
            entity.Get<BattleInfoComponent>().Value = _view.UnitPlayerBattle.Weapon;
            entity.Get<BattleInfoComponent>().Bullet = _view.UnitPlayerBattle.Weapon.StandardBullet;
            entity.Get<BattleInfoComponent>().AttackValue = _view.UnitPlayerBattle.Weapon.AttackValue;

            var directionMovement = Object.Instantiate(new GameObject(), _view.Transform, true);
            directionMovement.name = "->DirectionMoving<-";
            entity.Get<DirectionMovementComponent>().value = directionMovement.transform;
            
            //for collision 
            var playerEntity = new PlayerEntity(_view, entity);
        }
    }
}