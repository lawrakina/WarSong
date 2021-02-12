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
            player.Get<PlayerComponent>();
            player.Get<TransformComponent>().Value = _player.Transform;
            player.Get<RigidBodyComponent>().Value = _player.Rigidbody;
            player.Get<MovementSpeed>().Value = _player.CharAttributes.Speed;
            player.Get<RotateSpeed>().Value = _player.CharAttributes.RotateSpeedPlayer;
            player.Get<AnimatorComponent>().Value = _player.AnimatorParameters;
            Dbg.Log($"1111111_player.CharacterClass{_player.CharacterClass}");
            player.Get<NeedUpdateCurrentHpFromPlayerComponent>().Value = _player.CharacterClass.CurrentHp;
            player.Get<NeedUpdateMaxHpFromPlayerComponent>().Value = _player.CharacterClass.CurrentHp;

            var goTarget = Object.Instantiate(new GameObject(), _player.Transform, true);
            goTarget.name = "->DirectionMoving<-";
            var goTargetEntity = _world.NewEntity();
            goTargetEntity.Get<TransformComponent>().Value = goTarget.transform;

            player.Get<GoTargetComponent>().Value = goTargetEntity;
        }
    }
}