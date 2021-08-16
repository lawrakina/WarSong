using System.Collections.Generic;
using Code.Extension;
using Code.Fight.EcsBattle.CustomEntities;
using Code.Profile.Models;
using Code.Unit;
using Leopotam.Ecs;
using ThirdPersonCameraWithLockOn;
using UnityEngine;


namespace Code.Fight.EcsBattle.Unit.Create
{
    public sealed class CreateEnemyEntitySystem : IEcsInitSystem
    {
        private EcsWorld _world;
        private EnemiesLevelModel _enemiesModel;
        private ThirdPersonCamera _camera;
        private InOutControlFightModel _model;

        public void Init()
        {
            Dbg.Log($"LIST model: {_enemiesModel}");
            Dbg.Log($"LIST ENEMIES: {_enemiesModel.Enemies}");
            foreach (var view in _enemiesModel.Enemies)
            {
                Dbg.Error($"view.HealthBar:{view.HealthBar}. _camera:{_camera}");
                view.HealthBar.SetCamera(_camera.transform);

                var entity = _world.NewEntity();
                entity.Get<EnemyComponent>();
                entity.Get<UnitComponent>()._view = view;
                entity.Get<UnitComponent>()._rootTransform = view.Transform;
                entity.Get<UnitComponent>()._rigidBody = view.Rigidbody;
                entity.Get<UnitComponent>()._collider = view.Collider;
                entity.Get<UnitComponent>()._animator = view.AnimatorParameters;
                entity.Get<UnitComponent>()._reputation = view.UnitReputation;
                entity.Get<UnitComponent>()._vision = view.UnitVision;
                entity.Get<UnitComponent>()._level = view.UnitLevel;
                entity.Get<UnitComponent>()._health = view.UnitHealth;
                entity.Get<UiEnemyHealthBarComponent>()._value = view.HealthBar;
                
                // entity.Get<BattleInfoMainWeaponComponent>()._attackValue = view.a.UnitEquipment.MainWeapon.AttackValue;
                // entity.Get<BattleInfoMainWeaponComponent>()._bullet = view.UnitEquipment.MainWeapon.StandardBullet;
                
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