using Code.Data.Unit;
using Code.Extension;
using Code.Fight.EcsFight.Settings;
using Code.Fight.EcsFight.Timer;
using Code.Unit;
using Leopotam.Ecs;
using UnityEngine;


namespace Code.Fight.EcsFight.Battle{
    public sealed class AttackS : IEcsInitSystem, IEcsRunSystem{
        private EcsFilter<UnitC> _units;
        private readonly int _bulletCapacity;
        private EcsWorld _world = null;
        private EcsFilter<UnitC, NeedAttackTargetC, TargetListC> _needAttackBehaviour;
        private EcsFilter<UnitC, AutoAttackTag, TargetListC, MainWeaponC> _autoAttack;

        private EcsFilter<WeaponBulletC, DisableTag> _poolBullets;

        private EcsFilter<UnitC, AttackCollisionC> _applyDamage;

        public AttackS(int bulletCapacity){
            _bulletCapacity = bulletCapacity;
        }

        public void Init(){
            foreach (var i in _units){
                ref var entity = ref _units.GetEntity(i);
                ref var unit = ref _units.Get1(i);
                if (Equals(unit.InfoAboutWeapons, null)) continue;
                if (unit.InfoAboutWeapons.WeaponRange > 2f){
                    if (entity.Has<MainWeaponC>()){
                        ref var weapon = ref entity.Get<MainWeaponC>();
                        for (int j = 0; j < _bulletCapacity; j++){
                            var go = Object.Instantiate(weapon.Value.Bullet, unit.Transform, true);
                            go.gameObject.SetActive(false);
                            var entityBullet = _world.NewEntity();
                            entityBullet.Get<WeaponBulletC>().Value = go;
                            entityBullet.Get<DisableTag>();
                        }
                    }

                    if (entity.Has<SecondWeaponC>()){
                        ref var weapon = ref entity.Get<SecondWeaponC>();
                        for (int j = 0; j < _bulletCapacity; j++){
                            var go = Object.Instantiate(weapon.Value.Bullet, unit.Transform, true);
                            go.gameObject.SetActive(false);
                            var entityBullet = _world.NewEntity();
                            entityBullet.Get<WeaponBulletC>().Value = go;
                            entityBullet.Get<DisableTag>();
                        }
                    }
                }
            }
        }

        public void Run(){
            foreach (var i in _needAttackBehaviour){
                ref var entity = ref _needAttackBehaviour.GetEntity(i);
                ref var unit = ref _needAttackBehaviour.Get1(i);
                ref var target = ref _needAttackBehaviour.Get3(i);
                if (target.IsExist){
                    ref var moveEvent = ref entity.Get<AutoMoveEventC>();

                    //бежим к цели
                    if (target.Current.transform.SqrDistance(unit.Transform) >= unit.InfoAboutWeapons.SqrDistance){
                        var direction = target.Current.transform.position - unit.Transform.position;
                        moveEvent.Vector = direction;
                        // DebugExtension.DebugArrow(unit.Transform.position, direction, Color.red);
                        if (entity.Has<CameraC>()){
                            var camera = entity.Get<CameraC>();
                            moveEvent.CameraRotation = camera.Value.Transform.rotation;
                        }

                        var move = new AutoMoveEventC{
                            Vector = new Vector3(
                                direction.x,
                                direction.y,
                                direction.z
                            ),
                        };
                        entity.Get<AutoMoveEventC>() = move;
                    } else{
                        moveEvent.Vector = Vector3.zero;
                        //ударить
                        entity.Get<AutoAttackTag>();
                        entity.Del<NeedAttackTargetC>();
                    }
                } else{
                    entity.Del<NeedAttackTargetC>();
                }
            }

            foreach (var i in _autoAttack){
                ref var entity = ref _autoAttack.GetEntity(i);
                ref var unit = ref _autoAttack.Get1(i);
                ref var target = ref _autoAttack.Get3(i);
                ref var weapon = ref _autoAttack.Get4(i);

                if (!entity.Has<Timer<AttackBannedWeapon1Tag>>()){
                    if (!entity.Has<Timer<Reload1WeaponTag>>()){
                        if (!entity.Has<Timer<LagBeforeWeapon1>>()){
                            unit.Animator.SetTriggerAttack();
                            entity.Get<Timer<LagBeforeWeapon1>>().TimeLeftSec = weapon.LagBefAttack;
                        } else{
                            entity.Get<Timer<Reload1WeaponTag>>().TimeLeftSec = weapon.Speed;
                        }
                    } else{
                        if (!entity.Has<Timer<LagBeforeWeapon1>>()){
                            if ((target.Current.transform.position - unit.Transform.position).sqrMagnitude <
                                unit.InfoAboutWeapons.SqrDistance){
                                unit.UnitMovement.Motor.SetRotation(
                                    Quaternion.LookRotation(target.Current.transform.position -
                                                            unit.Transform.position));
                                SingleMomentumAttack(target, weapon, entity);
                            } else{
                                entity.Get<NeedAttackTargetC>();
                            }

                            entity.Get<Timer<AttackBannedWeapon1Tag>>().TimeLeftSec =
                                entity.Get<Timer<Reload1WeaponTag>>().TimeLeftSec;
                        }
                    }
                }
            }

            foreach (var i in _applyDamage){
                ref var entity = ref _applyDamage.GetEntity(i);
                ref var unit = ref _applyDamage.Get1(i);
                ref var infoCollision = ref _applyDamage.Get2(i);

                var damage = infoCollision.Value.Damage;
                unit.Health.CurrentHp -= damage;

                entity.Get<NeedShowUiEventC>().PointsDamage = damage;
                entity.Get<NeedShowUiEventC>().DamageType = infoCollision.Value.DamageType;

                if (unit.Health.CurrentHp <= 0.0f){
                    entity.Get<DeathEventC>().Killer = infoCollision.Value.Attacker;
                }

                entity.Del<AttackCollisionC>();
            }
        }

        private void SingleMomentumAttack(TargetListC target, MainWeaponC weapon, EcsEntity unit){
            var targetCollision = target.Current.transform.GetComponent<ICollision>();
            var collision =
                new InfoCollision(weapon.Value.GetDamage(), unit);
            targetCollision?.OnCollision(collision);
        }

        // private void RangeShotAttack(TargetListC target, MainWeaponC weapon, UnitC unit){
        //     var startPosition =
        //         unit.Transform.position + unit.UnitVision.OffsetHead;
        //
        //     EcsEntity bulletEntity = EcsEntity.Null;
        //     foreach (var b in _poolBullets){
        //         bulletEntity = _poolBullets.GetEntity(b);
        //         break;
        //     }
        //
        //     var weaponBulletComponent = bulletEntity.Get<WeaponBulletC>();
        //     if (bulletEntity == EcsEntity.Null){
        //         weaponBulletComponent.Value = Object.Instantiate(weapon.Value.Bullet, unit.Transform, true);
        //     } else{
        //         bulletEntity.Del<DisableTag>();
        //     }
        //
        //     weaponBulletComponent.Value.gameObject.SetActive(true);
        //     weaponBulletComponent.Value.transform.position = startPosition;
        //     weaponBulletComponent.Value.TargetGameObject = target.Current; //.GetComponent<IBaseUnitView>();
        //     weaponBulletComponent.Value.Clear();
        //
        //
        //     bulletEntity.Get<WeaponBulletC>().Value = weaponBulletComponent.Value;
        //     bulletEntity.Get<WeaponBulletC>().Collision =
        //         new InfoCollision(weapon.Value.GetDamage(), unit);
        // }
        //
        // private void SplashMomentumAttack(MainWeaponC weapon, UnitC unit){
        //     var attackPositionCenter =
        //         unit.Transform.position + unit.UnitMovement.MeshRoot.forward + unit.UnitVision.OffsetHead;
        //     var maxColliders = 10;
        //     var hitColliders = new Collider[maxColliders];
        //     var numColliders = Physics.OverlapSphereNonAlloc(attackPositionCenter,
        //         1.0f, hitColliders, 1 << unit.Reputation.EnemyLayer);
        //
        //     for (int index = 0; index < numColliders; index++){
        //         var tempObj = hitColliders[index].gameObject.GetComponent<ICollision>();
        //         if (tempObj != null){
        //             var collision = new InfoCollision(weapon.Value.GetDamage(), unit);
        //             tempObj.OnCollision(collision);
        //         }
        //     }
        // }
    }

    public struct DeathEventC{
        public EcsEntity Killer;
    }

    public struct NeedShowUiEventC{
        public DamageType DamageType;
        public float PointsDamage;
    }
}