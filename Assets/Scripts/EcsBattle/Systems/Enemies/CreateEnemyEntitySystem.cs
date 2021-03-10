using System.Collections.Generic;
using EcsBattle.Components;
using EcsBattle.CustomEntities;
using Interface;
using Leopotam.Ecs;
using Unit;
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
                entity.Get<BaseUnitComponent>().collider = view.Collider;
                entity.Get<BaseUnitComponent>().rigidbody = view.Rigidbody;
                entity.Get<BaseUnitComponent>().animator = view.AnimatorParameters;
                entity.Get<BaseUnitComponent>().unitReputation = view.UnitReputation;
                entity.Get<BaseUnitComponent>().unitVision = view.UnitVision;
                entity.Get<MovementSpeed>().Value = view.Attributes.Speed;
                entity.Get<RotateSpeed>().Value = view.Attributes.RotateSpeedPlayer;
                entity.Get<UiEnemyHealthBarComponent>().Value = view.HealthBar;
                entity.Get<UnitHpComponent>().CurrentValue = view.CurrentHp;
                entity.Get<UnitHpComponent>().MaxValue = view.MaxHp;
                
                if(view.UnitBattle == null) throw new System.NotImplementedException("view.UnitBattle: NULL");
                entity.Get<BattleInfoComponent>().Value = view.UnitBattle.Weapon;
                entity.Get<BattleInfoComponent>().AttackValue = view.UnitBattle.Weapon.AttackValue;
                entity.Get<BattleInfoComponent>().Bullet = view.UnitBattle.Weapon.StandardBullet;
                
                //Ragdoll
                SearchNodesOfRagdoll(entity, view);

                var enemyEntity = new EnemyEntity(view, entity);
            }
        }

        private static void SearchNodesOfRagdoll(EcsEntity entity, IBaseUnitView view)
        {
            entity.Get<ListRigidBAndCollidersComponent>().rigidbodies
                = new List<Rigidbody>(view.Transform.GetComponentsInChildren<Rigidbody>());
            entity.Get<ListRigidBAndCollidersComponent>().colliders
                = new List<Collider>(view.Transform.GetComponentsInChildren<Collider>());
            foreach (var rigidbody in entity.Get<ListRigidBAndCollidersComponent>().rigidbodies)
                rigidbody.isKinematic = true;
            foreach (var collider in entity.Get<ListRigidBAndCollidersComponent>().colliders)
                collider.enabled = false;
            view.Rigidbody.isKinematic = false;
            view.Collider.enabled = true;
        }
    }

    public struct ListRigidBAndCollidersComponent
    {
        public List<Rigidbody> rigidbodies;
        public List<Collider> colliders;
    }
}