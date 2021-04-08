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
                entity.Get<UnitComponent>().rootTransform = view.Transform;
                entity.Get<UnitComponent>().rigidbody = view.Rigidbody;
                entity.Get<UnitComponent>().collider = view.Collider;
                entity.Get<UnitComponent>().animator = view.AnimatorParameters;
                entity.Get<UnitComponent>().reputation = view.UnitReputation;
                entity.Get<UnitComponent>().vision = view.UnitVision;
                entity.Get<UnitComponent>().attributes = view.Attributes;
                entity.Get<UnitComponent>().level = view.UnitLevel;
                entity.Get<UiEnemyHealthBarComponent>().Value = view.HealthBar;
                entity.Get<UnitHpComponent>().CurrentValue = view.CurrentHp;
                entity.Get<UnitHpComponent>().MaxValue = view.MaxHp;
                
                if(view.UnitBattle == null) throw new System.NotImplementedException("view.UnitBattle: NULL");
                entity.Get<BattleInfoMainWeaponComponent>().Value = view.UnitBattle.Weapon;
                entity.Get<BattleInfoMainWeaponComponent>().AttackValue = view.UnitBattle.Weapon.AttackValue;
                entity.Get<BattleInfoMainWeaponComponent>().Bullet = view.UnitBattle.Weapon.StandardBullet;
                
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
}