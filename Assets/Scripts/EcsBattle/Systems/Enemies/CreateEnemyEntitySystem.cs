using System.Collections.Generic;
using EcsBattle.Components;
using EcsBattle.CustomEntities;
using EcsBattle.Systems.Player;
using Extension;
using Interface;
using Leopotam.Ecs;
using Models;
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
        private BattleProgressModel _battleProgressModel;

        public void Init()
        {
            foreach (var view in _listEnemies)
            {
                view.HealthBar.SetCamera(_camera.Transform);

                var entity = _world.NewEntity();
                entity.Get<EnemyComponent>();
                entity.Get<UnitComponent>()._view = view;
                entity.Get<UnitComponent>()._rootTransform = view.Transform;
                entity.Get<UnitComponent>()._rigidBody = view.Rigidbody;
                entity.Get<UnitComponent>()._collider = view.Collider;
                entity.Get<UnitComponent>()._animator = view.AnimatorParameters;
                entity.Get<UnitComponent>()._reputation = view.UnitReputation;
                entity.Get<UnitComponent>()._vision = view.UnitVision;
                entity.Get<UnitComponent>()._attributes = view.Attributes;
                entity.Get<UnitComponent>()._level = view.UnitLevel;
                entity.Get<UnitComponent>()._health = view.UnitHealth;
                entity.Get<UiEnemyHealthBarComponent>()._value = view.HealthBar;
                
                if(view.UnitBattle == null) throw new System.NotImplementedException("view.UnitBattle: NULL");
                entity.Get<BattleInfoMainWeaponComponent>()._value = view.UnitBattle.Weapon;
                entity.Get<BattleInfoMainWeaponComponent>()._attackValue = view.UnitBattle.Weapon.AttackValue;
                entity.Get<BattleInfoMainWeaponComponent>()._bullet = view.UnitBattle.Weapon.StandardBullet;
                
                //Ragdoll
                SearchNodesOfRagdoll(entity, view);

                var enemyEntity = new EnemyEntity(view, entity);
            }
        }

        private static void SearchNodesOfRagdoll(EcsEntity entity, IBaseUnitView view)
        {
            entity.Get<ListRigidBAndCollidersComponent>()._rigidBodies
                = new List<Rigidbody>(view.Transform.GetComponentsInChildren<Rigidbody>());
            entity.Get<ListRigidBAndCollidersComponent>()._colliders
                = new List<Collider>(view.Transform.GetComponentsInChildren<Collider>());
            foreach (var rigidbody in entity.Get<ListRigidBAndCollidersComponent>()._rigidBodies)
                rigidbody.isKinematic = true;
            foreach (var collider in entity.Get<ListRigidBAndCollidersComponent>()._colliders)
                collider.enabled = false;
            view.Rigidbody.isKinematic = false;
            view.Collider.enabled = true;
        }
    }
}