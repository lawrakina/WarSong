using Code.Extension;
using Code.Fight.EcsFight.Battle;
using Code.Fight.EcsFight.Settings;
using Code.Fight.EcsFight.Timer;
using Code.Unit;
using Leopotam.Ecs;
using UnityEngine;


namespace Code.Fight.EcsFight{
    public class BattleS : IEcsInitSystem, IEcsRunSystem{
        private EcsFilter<UnitC> _units;
        private readonly int _bulletCapacity;
        private EcsWorld _world = null;
        private EcsFilter<MainWeaponC>.Exclude<Timer<PermisAttack1Weapon>, PermisAttack1Weapon> _permission1W;
        private EcsFilter<UnitC, FoundTargetC, NeedAttackTargetCommand> _gotoTarget;
        private EcsFilter<UnitC, FoundTargetC, StartAttackCommand, MainWeaponC,PermisAttack1Weapon> _startAttack;
        private EcsFilter<UnitC, FoundTargetC, AttackEvent1W, MainWeaponC>.Exclude<Timer<LagBeforeAttack1W>>
            _attackEvent;
        private EcsFilter<UnitC, AttackCollisionC> _applyDamage;
        private EcsFilter<UnitC, Timer<BattleTag>, FoundTargetC> _battleState;

        public BattleS(int bulletCapacity){
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
            foreach (var i in _permission1W){
                ref var entity = ref _permission1W.GetEntity(i);
                ref var weapon = ref _permission1W.Get1(i);
                entity.Get<Timer<PermisAttack1Weapon>>().TimeLeftSec = weapon.Speed;

                var timer = _world.NewEntity();
                timer.Get<PermisAttack1Weapon>();
                timer.Get<TimerForAdd>().TargetEntity = entity;
                timer.Get<TimerForAdd>().TimeLeftSec = weapon.Speed;
            }

            foreach (var i in _gotoTarget){
                ref var entity = ref _gotoTarget.GetEntity(i);
                ref var unit = ref _gotoTarget.Get1(i);
                ref var target = ref _gotoTarget.Get2(i);
                ref var moveEvent = ref entity.Get<AutoMoveEventC>();

                if (target.Value.transform.SqrDistance(unit.Transform) > unit.InfoAboutWeapons.SqrDistance){
                    //если дальше чем дистанция атаки то бежим в направлении цели
                    var direction = target.Value.transform.position - unit.Transform.position;
                    moveEvent.Vector = direction;

                    if (entity.Has<CameraC>()){
                        moveEvent.CameraRotation = entity.Get<CameraC>().Value.Transform.rotation;
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
                    //если на дистанции атаки то бьем
                    entity.Del<NeedAttackTargetCommand>();
                    entity.Get<StartAttackCommand>();
                }
            }

            foreach (var i in _startAttack){
                ref var entity = ref _startAttack.GetEntity(i);
                ref var unit = ref _startAttack.Get1(i);
                ref var target = ref _startAttack.Get2(i);
                ref var weapon = ref _startAttack.Get4(i);

                unit.UnitMovement.Motor.SetRotation(
                    Quaternion.LookRotation(target.Value.transform.position -
                                            unit.Transform.position));

                unit.Animator.SetTriggerAttack();
                entity.Get<Timer<LagBeforeAttack1W>>().TimeLeftSec = weapon.LagBefAttack;
                entity.Get<AttackEvent1W>();

                entity.Del<PermisAttack1Weapon>();
                entity.Del<StartAttackCommand>();
            }

            foreach (var i in _attackEvent){
                ref var entity = ref _attackEvent.GetEntity(i);
                ref var unit = ref _attackEvent.Get1(i);
                ref var target = ref _attackEvent.Get2(i);
                ref var weapon = ref _attackEvent.Get4(i);

                // switch (weapon.Value.AttackType(Splash, SingleTarget, Range, Magic,...)){
                // case "Splash":
                // case "SingleTarget":
                // case "Range":
                // case "Magic":
                // }
                if(target.Value.transform.SqrDistance(unit.Transform) > unit.InfoAboutWeapons.SqrDistance)
                    continue;
                    
                SingleMomentumAttack(target.Value, weapon, entity);
                entity.Del<AttackEvent1W>();

                entity.Get<Timer<BattleTag>>().TimeLeftSec = 5f;
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

            foreach (var i in _battleState){
                ref var entity = ref _battleState.GetEntity(i);
                
                entity.Get<NeedFindTargetCommand>();
                entity.Get<NeedAttackTargetCommand>();
            }
        }

        private void SingleMomentumAttack(GameObject target, MainWeaponC weapon, EcsEntity unit){
            var targetCollision = target.transform.GetComponent<ICollision>();
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
}