using EcsBattle.Components;
using EcsBattle.CustomEntities;
using Leopotam.Ecs;
using Unit;
using Unit.Player;
using UnityEngine;


namespace EcsBattle.Systems.Player
{
    public sealed class CreatePlayerEntitySystem : IEcsInitSystem
    {
        private EcsWorld _world;
        private IPlayerView _view;

        public void Init()
        {
            var entity = _world.NewEntity();
            //Base components
            entity.Get<PlayerComponent>().rootTransform =_view.Transform; 
            entity.Get<PlayerComponent>().modelTransform =_view.TransformModel; 
            entity.Get<RigidbodyComponent>().value = _view.Rigidbody;
            entity.Get<AnimatorComponent>().value = _view.AnimatorParameters;
            entity.Get<UnitReputationComponent>().value = _view.UnitReputation;
            entity.Get<UnitVisionComponent>().value = _view.UnitVision;
            //moving rotate
            entity.Get<MovementSpeed>().value = _view.Attributes.Speed;
            entity.Get<RotateSpeed>().value = _view.Attributes.RotateSpeedPlayer;
            //ui
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