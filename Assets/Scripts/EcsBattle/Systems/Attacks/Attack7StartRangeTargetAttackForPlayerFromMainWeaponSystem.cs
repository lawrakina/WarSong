using System.Linq;
using Battle;
using EcsBattle.Components;
using Leopotam.Ecs;
using UnityEngine;


namespace EcsBattle.Systems.Attacks
{
    public sealed class Attack7StartRangeTargetAttackForPlayerFromMainWeaponSystem : IEcsRunSystem
    {
        private EcsWorld _world;

        private EcsFilter<
            FinalAttackFromMainWeaponComponent,
            PlayerComponent,
            UnitComponent,
            CurrentTargetComponent,
            BattleInfoMainWeaponComponent> _filter;

        private EcsFilter<WeaponBulletComponent, DisableComponent> _poolBullets;

        public void Run()
        {
            foreach (var i in _filter)
            {
                ref var entity = ref _filter.GetEntity(i);
                ref var transform = ref _filter.Get3(i)._rootTransform;
                ref var vision = ref _filter.Get3(i)._vision;
                ref var target = ref _filter.Get4(i);
                ref var battleInfo = ref _filter.Get5(i);

                var startPosition =
                    transform.position + vision.offsetHead;

                EcsEntity bulletEntity = EcsEntity.Null;
                foreach (var b in _poolBullets)
                {
                    bulletEntity = _poolBullets.GetEntity(b);
                    break;
                }

                var weaponBulletComponent = bulletEntity.Get<WeaponBulletComponent>();
                if (bulletEntity == EcsEntity.Null)
                {
                    weaponBulletComponent._value = Object.Instantiate(battleInfo._bullet, transform, true);
                    weaponBulletComponent._value.name = battleInfo._bullet.name;
                }
                else
                {
                    bulletEntity.Del<DisableComponent>();
                }
                
                weaponBulletComponent._value.gameObject.SetActive(true);
                weaponBulletComponent._value.transform.position = startPosition;
                weaponBulletComponent._value.Target = target._baseUnitView;
                weaponBulletComponent._value.Trail.Clear();


                bulletEntity.Get<WeaponBulletComponent>()._value = weaponBulletComponent._value;
                bulletEntity.Get<WeaponBulletComponent>()._collision =
                    new InfoCollision(battleInfo._attackValue.GetAttack(), entity);

                entity.Del<FinalAttackFromMainWeaponComponent>();
            }
        }
    }
}