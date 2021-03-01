using EcsBattle.Components;
using Extension;
using Leopotam.Ecs;
using Unit;
using Unit.Player;
using UnityEngine;


namespace EcsBattle
{
    public sealed class CreatePlayerEntitySystem : IEcsInitSystem
    {
        private EcsWorld _world;
        private IPlayerView _player;

        public void Init()
        {
            // Debug.Log($"CreatePlayerEntitySystem.Init()");
            var player = _world.NewEntity();
            //Base components
            player.Get<PlayerComponent>();
            
            player.Get<BaseUnitComponent>().transform = _player.Transform;
            player.Get<BaseUnitComponent>().rigidbody = _player.Rigidbody;
            player.Get<BaseUnitComponent>().animator = _player.AnimatorParameters;
            player.Get<BaseUnitComponent>().unitReputation = _player.UnitReputation;
            player.Get<BaseUnitComponent>().offsetHead = _player.UnitVision.OffsetHead;
            
            Dbg.Log($"_player.UnitReputation.EnemyLayer{LayerMask.LayerToName(_player.UnitReputation.EnemyLayer)}");
            Dbg.Log($"_player.UnitReputation.FriendLayer{LayerMask.LayerToName(_player.UnitReputation.FriendLayer)}");
            
            // player.Get<TransformComponent>().Value = _player.Transform;
            // player.Get<TransformComponent>().OffsetHead = _player.UnitVision.OffsetHead;
            // player.Get<RigidBodyComponent>().Value = _player.Rigidbody;
            //moving rotate
            player.Get<MovementSpeed>().Value = _player.CharAttributes.Speed;
            player.Get<RotateSpeed>().Value = _player.CharAttributes.RotateSpeedPlayer;
            player.Get<AnimatorComponent>().Value = _player.AnimatorParameters;
            //ui
            player.Get<NeedUpdateCurrentHpFromPlayerComponent>().Value = _player.CurrentHp;
            player.Get<NeedUpdateMaxHpFromPlayerComponent>().Value = _player.CurrentHp;
            //vision
            player.Get<DetectionDistanceEnemyComponent>().Value = _player.UnitVision.BattleDistance;
            player.Get<LayerMaskEnemiesComponent>().Value = _player.UnitVision.LayersEnemies;//ДУБЛИРОВАНИЕ СЛОЕВ
            //battle
            player.Get<AutoBattleDisableComponent>();
            player.Get<BattleComponent>().Value = _player.UnitBattle.Weapon;
            player.Get<BattleComponent>().Bullet = _player.UnitBattle.Weapon.StandardBullet;
            player.Get<BattleComponent>().AttackValue = _player.UnitBattle.Weapon.AttackValue;
            


            var goTarget = Object.Instantiate(new GameObject(), _player.Transform, true);
            goTarget.name = "->DirectionMoving<-";
            var goTargetEntity = _world.NewEntity();
            goTargetEntity.Get<TransformComponent>().Value = goTarget.transform;

            player.Get<GoTargetComponent>().Value = goTargetEntity;
        }
    }

    public struct BaseUnitComponent
    {
        public UnitReputation unitReputation;
        public Transform transform;
        public Rigidbody rigidbody;
        public AnimatorParameters animator;
        public Vector3 offsetHead;
    }
}