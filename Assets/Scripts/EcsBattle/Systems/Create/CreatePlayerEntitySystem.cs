using EcsBattle.Components;
using Extension;
using Leopotam.Ecs;
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
            player.Get<TransformComponent>().Value = _player.Transform;
            player.Get<TransformComponent>().OffsetHead = _player.UnitVision.OffsetHead;
            player.Get<RigidBodyComponent>().Value = _player.Rigidbody;
            //moving rotate
            player.Get<MovementSpeed>().Value = _player.CharAttributes.Speed;
            player.Get<RotateSpeed>().Value = _player.CharAttributes.RotateSpeedPlayer;
            player.Get<AnimatorComponent>().Value = _player.AnimatorParameters;
            //ui
            player.Get<NeedUpdateCurrentHpFromPlayerComponent>().Value = _player.CurrentHp;
            player.Get<NeedUpdateMaxHpFromPlayerComponent>().Value = _player.CurrentHp;
            //vision
            player.Get<DetectionDistanceEnemyComponent>().Value = _player.UnitVision.BattleDistance;
            player.Get<LayerMaskEnemiesComponent>().Value = _player.UnitVision.LayersEnemies;
            //battle
            player.Get<AutoBattleDisableComponent>();
            Dbg.Log($"_player.UnitBattle.Weapon:{_player.UnitBattle.Weapon}");
            Dbg.Log($"_player.UnitBattle.Weapon.StandardBullet:{_player.UnitBattle.Weapon.StandardBullet}");
            player.Get<EquipmentWeaponComponent>().Value = _player.UnitBattle.Weapon;
            player.Get<EquipmentWeaponComponent>().Bullet = _player.UnitBattle.Weapon.StandardBullet;
            


            var goTarget = Object.Instantiate(new GameObject(), _player.Transform, true);
            goTarget.name = "->DirectionMoving<-";
            var goTargetEntity = _world.NewEntity();
            goTargetEntity.Get<TransformComponent>().Value = goTarget.transform;

            player.Get<GoTargetComponent>().Value = goTargetEntity;
        }
    }
}