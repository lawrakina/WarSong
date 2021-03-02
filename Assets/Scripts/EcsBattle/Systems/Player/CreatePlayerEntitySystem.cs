using EcsBattle.Components;
using Leopotam.Ecs;
using Unit.Player;
using UnityEngine;


namespace EcsBattle.Systems.Player
{
    public sealed class CreatePlayerEntitySystem : IEcsInitSystem
    {
        private EcsWorld _world;
        private IPlayerView _player;

        public void Init()
        {
            var player = _world.NewEntity();
            //Base components
            player.Get<PlayerComponent>();
            player.Get<BaseUnitComponent>().transform = _player.Transform;
            player.Get<BaseUnitComponent>().rigidbody = _player.Rigidbody;
            player.Get<BaseUnitComponent>().animator = _player.AnimatorParameters;
            player.Get<BaseUnitComponent>().unitReputation = _player.UnitReputation;
            player.Get<BaseUnitComponent>().unitVision = _player.UnitVision;
            
            // Dbg.Log($"_player.UnitReputation.EnemyLayer{LayerMask.LayerToName(_player.UnitReputation.EnemyLayer)}");
            // Dbg.Log($"_player.UnitReputation.FriendLayer{LayerMask.LayerToName(_player.UnitReputation.FriendLayer)}");
            //
            // player.Get<TransformComponent>().Value = _player.Transform;
            // player.Get<TransformComponent>().OffsetHead = _player.UnitVision.OffsetHead;
            // player.Get<RigidBodyComponent>().Value = _player.Rigidbody;
            //moving rotate
            player.Get<MovementSpeed>().Value = _player.Attributes.Speed;
            player.Get<RotateSpeed>().Value = _player.Attributes.RotateSpeedPlayer;
            player.Get<AnimatorComponent>().Value = _player.AnimatorParameters;
            //ui
            player.Get<NeedUpdateCurrentHpFromPlayerComponent>().Value = _player.CurrentHp;
            player.Get<NeedUpdateMaxHpFromPlayerComponent>().Value = _player.CurrentHp;
            //battle
            player.Get<AutoBattleDisableComponent>();
            player.Get<BattleInfoComponent>().Value = _player.UnitBattle.Weapon;
            player.Get<BattleInfoComponent>().Bullet = _player.UnitBattle.Weapon.StandardBullet;
            player.Get<BattleInfoComponent>().AttackValue = _player.UnitBattle.Weapon.AttackValue;
            


            var goTarget = Object.Instantiate(new GameObject(), _player.Transform, true);
            goTarget.name = "->DirectionMoving<-";
            var goTargetEntity = _world.NewEntity();
            goTargetEntity.Get<TransformComponent>().Value = goTarget.transform;

            player.Get<GoTargetComponent>().Value = goTargetEntity;
        }
    }
}