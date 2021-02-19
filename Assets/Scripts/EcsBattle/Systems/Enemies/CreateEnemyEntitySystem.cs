using System.Collections.Generic;
using EcsBattle.Components;
using Interface;
using Leopotam.Ecs;
using Unit.Enemies;


namespace EcsBattle
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
                entity.Get<TransformComponent>().Value = view.Transform;
                entity.Get<RigidBodyComponent>().Value = view.Rigidbody;
                entity.Get<MovementSpeed>().Value = view.CharAttributes.Speed;
                entity.Get<RotateSpeed>().Value = view.CharAttributes.RotateSpeedPlayer;
                entity.Get<AnimatorComponent>().Value = view.AnimatorParameters;
                entity.Get<UiEnemyHealthBarComponent>().Value = view.HealthBar;
                entity.Get<UnitHpComponent>().CurrentValue = view.CurrentHp;
                entity.Get<UnitHpComponent>().MaxValue = view.MaxHp;
            }
        }
    }
}