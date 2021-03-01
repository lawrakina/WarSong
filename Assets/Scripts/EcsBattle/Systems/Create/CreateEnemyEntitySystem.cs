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
                entity.Get<BaseUnitComponent>().unitReputation = view.UnitReputation;
                entity.Get<MovementSpeed>().Value = view.CharAttributes.Speed;
                entity.Get<RotateSpeed>().Value = view.CharAttributes.RotateSpeedPlayer;
                entity.Get<UiEnemyHealthBarComponent>().Value = view.HealthBar;
                entity.Get<UnitHpComponent>().CurrentValue = view.CurrentHp;
                entity.Get<UnitHpComponent>().MaxValue = view.MaxHp;

                var enemyEntity = new EnemyEntity(view, entity);
            }
        }
    }
}