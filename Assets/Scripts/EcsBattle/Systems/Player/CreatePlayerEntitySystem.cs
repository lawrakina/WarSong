using EcsBattle.Components;
using EcsBattle.CustomEntities;
using Leopotam.Ecs;
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
            entity.Get<PlayerComponent>();
            entity.Get<BaseUnitComponent>().transform = _view.Transform;
            entity.Get<BaseUnitComponent>().rigidbody = _view.Rigidbody;
            entity.Get<BaseUnitComponent>().animator = _view.AnimatorParameters;
            entity.Get<BaseUnitComponent>().unitReputation = _view.UnitReputation;
            entity.Get<BaseUnitComponent>().unitVision = _view.UnitVision;
            
            // Dbg.Log($"_player.UnitReputation.EnemyLayer{LayerMask.LayerToName(_player.UnitReputation.EnemyLayer)}");
            // Dbg.Log($"_player.UnitReputation.FriendLayer{LayerMask.LayerToName(_player.UnitReputation.FriendLayer)}");
            //
            // player.Get<TransformComponent>().Value = _player.Transform;
            // player.Get<TransformComponent>().OffsetHead = _player.UnitVision.OffsetHead;
            // player.Get<RigidBodyComponent>().Value = _player.Rigidbody;
            //moving rotate
            entity.Get<MovementSpeed>().Value = _view.Attributes.Speed;
            entity.Get<RotateSpeed>().Value = _view.Attributes.RotateSpeedPlayer;
            entity.Get<AnimatorComponent>().Value = _view.AnimatorParameters;
            //ui
            // entity.Get<NeedUpdateCurrentHpFromPlayerComponent>().Value = _view.CurrentHp;
            // entity.Get<NeedUpdateMaxHpFromPlayerComponent>().Value = _view.CurrentHp;
            entity.Get<UnitHpComponent>().CurrentValue = _view.CurrentHp;
            entity.Get<UnitHpComponent>().MaxValue = _view.CurrentHp;
            //battle
            entity.Get<AutoBattleDisableComponent>();
            entity.Get<BattleInfoComponent>().Value = _view.UnitPlayerBattle.Weapon;
            entity.Get<BattleInfoComponent>().Bullet = _view.UnitPlayerBattle.Weapon.StandardBullet;
            entity.Get<BattleInfoComponent>().AttackValue = _view.UnitPlayerBattle.Weapon.AttackValue;
            


            var goTarget = Object.Instantiate(new GameObject(), _view.Transform, true);
            goTarget.name = "->DirectionMoving<-";
            var goTargetEntity = _world.NewEntity();
            goTargetEntity.Get<TransformComponent>().Value = goTarget.transform;

            entity.Get<GoTargetComponent>().Value = goTargetEntity;
            
            var playerEntity = new PlayerEntity(_view, entity);
        }
    }
}