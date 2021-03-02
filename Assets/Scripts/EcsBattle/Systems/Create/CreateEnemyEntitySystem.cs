using System.Collections.Generic;
using Battle;
using EcsBattle.Components;
using EcsBattle.Systems.Create;
using Extension;
using Interface;
using Leopotam.Ecs;
using Unit.Enemies;
using UnityEngine;


namespace EcsBattle.Systems.Enemies
{
    public sealed class CreateEnemyEntitySystem : IEcsInitSystem
    {
        private EcsWorld _world;
        private List<IEnemyView> _listEnemies;
        private IFightCamera _camera;

        public void Init()
        {
            foreach (var view in _listEnemies)
            {
                view.HealthBar.SetCamera(_camera.Transform);

                var entity = _world.NewEntity();
                entity.Get<EnemyComponent>();
                entity.Get<BaseUnitComponent>().transform = view.Transform;
                entity.Get<BaseUnitComponent>().rigidbody = view.Rigidbody;
                entity.Get<BaseUnitComponent>().animator = view.AnimatorParameters;
                // Dbg.Log($"view.UnitReputation.{view.UnitReputation.EnemyLayer}");
                entity.Get<BaseUnitComponent>().unitReputation = view.UnitReputation;
                // Dbg.Log($"entity.UnitReputation.EnemyLayer:{entity.Get<BaseUnitComponent>().unitReputation.EnemyLayer}");
                entity.Get<BaseUnitComponent>().unitVision = view.UnitVision;
                // Dbg.Log($"view.UnitReputation.EnemyLayer{LayerMask.LayerToName(view.UnitReputation.EnemyLayer)}");
                // Dbg.Log($"view.UnitReputation.FriendLayer{LayerMask.LayerToName(view.UnitReputation.FriendLayer)}");

                entity.Get<MovementSpeed>().Value = view.Attributes.Speed;
                entity.Get<RotateSpeed>().Value = view.Attributes.RotateSpeedPlayer;
                entity.Get<UiEnemyHealthBarComponent>().Value = view.HealthBar;
                entity.Get<UnitHpComponent>().CurrentValue = view.CurrentHp;
                entity.Get<UnitHpComponent>().MaxValue = view.MaxHp;

                var enemyEntity = new EnemyEntity(view, entity);
            }
        }
    }
}