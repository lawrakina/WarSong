using System;
using Code.Data.Abilities;
using Code.Data.Dungeon;
using Code.Extension;
using Code.Fight.EcsFight.Battle;
using Code.Fight.EcsFight.Settings;
using Code.Fight.EcsFight.Timer;
using Code.Profile.Models;
using Leopotam.Ecs;
using UnityEngine;


namespace Code.Fight.EcsFight.Abilities{
    public class AbilitiesS<T> : IEcsInitSystem, IEcsRunSystem{
        private EcsWorld _world = null;
        private InOutControlFightModel _model;
        private DungeonParams _dungeonParams;
        private EcsFilter<PlayerTag, UnitC, PermissionForAbilitiesTag> _player;
        private EcsFilter<HubOfAbilitiesTag> _hubOfAbilitiesForPlayer;
        private EcsFilter<UnitC, FoundTargetC, NeedMoveToTargetC, AbilityC> _moveToTargetC;
        private EcsFilter<UnitC, AbilityC, NeedCheckResource> _checkAndDifferenceResource;
        private EcsFilter<NeedCanceledAbilityTag, AbilityC> _stoppingProcessOfAbilityBecauseNotEnoughResources;
        private EcsFilter<UnitC, FoundTargetC, Weapon<T>, AbilityC, NeedStartAbilityFromMeToTargetCommand,
            StartAbilityCommand> _startAbilityFromMeToUnit;
        private EcsFilter<AbilityC, TargetNotFoundedTag, NeedStartAbilityFromMeToTargetCommand>
            _stoppingProcessOfAbilityBecauseTargetNotFounded;
        private EcsFilter<UnitC, FoundTargetC, Weapon<T>, AbilityC, AttackEventWeapon<T>>.Exclude<
            Timer<LagBeforeAttackWeapon<T>>> _processAbilityFromMeToUnit;

        public void Init(){
            foreach (var i in _player){
                ref var entity = ref _player.GetEntity(i);
                ref var unit = ref _player.Get2(i);

                var abilities = _world.NewEntity();
                abilities.Get<HubOfAbilitiesTag>().OwnerEntity = entity;
                abilities.Get<HubOfAbilitiesTag>().Owner = unit;
                abilities.Get<HubOfAbilitiesTag>().Source = _model.InputControl.QueueOfAbilities;
            }
        }

        public void Run(){
            foreach (var i in _hubOfAbilitiesForPlayer){
                ref var hub = ref _hubOfAbilitiesForPlayer.Get1(i);

                while (hub.Source.Count != 0){
                    var ability = hub.Source.Dequeue();
                    if (hub.OwnerEntity.Has<AbilityC>()){
                        // если цель хаба уже имеет способность в статусе InProcess или Started
                        // то отменить только пришедшую способность и продолжить выполнение старой
                        // ability.State.Value = AbilityState.Canceled;
                        // return;
                        // если цель хаба уже имеет способность то отменить действующую и пришедшую способность
                        hub.OwnerEntity.Get<AbilityC>().Value.State.Value = AbilityState.Canceled;
                        ability.State.Value = AbilityState.Canceled;
                        return;
                    }

                    Dbg.Log($"New ability:{ability},{ability.AbilityTargetType},{ability.State}");

                    switch (ability.AbilityTargetType){
                        case AbilityTargetType.None:
                            break;
                        case AbilityTargetType.OnlyMe:
                            break;
                        case AbilityTargetType.OnlyTargetEnemy:
                            hub.OwnerEntity.Get<Timer<BattleTag>>().TimeLeftSec =
                                _dungeonParams.DurationOfAggreState;
                            hub.OwnerEntity.Get<NeedFindTargetCommand>();
                            hub.OwnerEntity.Get<NeedMoveToTargetC>();
                            hub.OwnerEntity.Get<NeedStartAbilityFromMeToTargetCommand>();
                            hub.OwnerEntity.Get<AbilityC>().Value = ability;
                            break;
                        case AbilityTargetType.OnlyTargetFriend:
                            break;
                        case AbilityTargetType.OnlyFriendlyWithoutMe:
                            break;
                        case AbilityTargetType.OnlyFriendlyWithMe:
                            break;
                        case AbilityTargetType.OnlyEnemies:
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                }
            }

            foreach (var i in _moveToTargetC){
                ref var entity = ref _moveToTargetC.GetEntity(i);
                ref var unit = ref _moveToTargetC.Get1(i);
                ref var target = ref _moveToTargetC.Get2(i);
                ref var ability = ref _moveToTargetC.Get4(i);
                ref var moveEvent = ref entity.Get<AutoMoveEventC>();

                if (unit.Transform == null || target.Value == null){
                    Dbg.Error($"Transform = null");
                    entity.Del<FoundTargetC>();
                    break;
                }

                if (target.Value.transform.SqrDistance(unit.Transform) >
                    ability.Value.Distance * ability.Value.Distance){
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
                    entity.Del<NeedMoveToTargetC>();
                    entity.Get<NeedCheckResource>();
                }
            }

            foreach (var i in _checkAndDifferenceResource){
                ref var entity = ref _checkAndDifferenceResource.GetEntity(i);
                ref var unit = ref _checkAndDifferenceResource.Get1(i);
                ref var ability = ref _checkAndDifferenceResource.Get2(i);

                entity.Del<NeedCheckResource>();
                if (0 < (unit.Resource.ResourceBaseValue - ability.Value.CostResource)){
                    FightLibrary.ChangeResource(entity, unit, ability.Value);
                    entity.Get<StartAbilityCommand>();
                } else{
                    entity.Get<NeedCanceledAbilityTag>();
                }
            }

            foreach (var i in _startAbilityFromMeToUnit){
                ref var entity = ref _startAbilityFromMeToUnit.GetEntity(i);
                ref var unit = ref _startAbilityFromMeToUnit.Get1(i);
                ref var target = ref _startAbilityFromMeToUnit.Get2(i);
                ref var weapon = ref _startAbilityFromMeToUnit.Get3(i);
                ref var ability = ref _startAbilityFromMeToUnit.Get4(i);

                unit.UnitMovement.Motor.SetRotation(
                    Quaternion.LookRotation(target.Value.transform.position -
                                            unit.Transform.position));

                Dbg.Log($"_startAbilityFromMeToUnit");
                ability.Value.State.Value = AbilityState.InProgress;
                unit.Animator.SetTriggerSpell();

                entity.Get<Timer<LagBeforeAttackWeapon<T>>>().TimeLeftSec = ability.Value.TimeLagBeforeAction;
                entity.Get<AttackEventWeapon<T>>();

                entity.Del<StartAbilityCommand>();
            }

            foreach (var i in _stoppingProcessOfAbilityBecauseNotEnoughResources){
                ref var entity = ref _stoppingProcessOfAbilityBecauseNotEnoughResources.GetEntity(i);
                ref var ability = ref _stoppingProcessOfAbilityBecauseNotEnoughResources.Get2(i);

                ability.Value.State.Value = AbilityState.Canceled;
                entity.Del<NeedCanceledAbilityTag>();
                entity.Del<NeedStartAbilityFromMeToTargetCommand>();
                entity.Del<AbilityC>();
            }

            foreach (var i in _stoppingProcessOfAbilityBecauseTargetNotFounded){
                ref var entity = ref _stoppingProcessOfAbilityBecauseTargetNotFounded.GetEntity(i);
                ref var ability = ref _stoppingProcessOfAbilityBecauseTargetNotFounded.Get1(i);

                ability.Value.State.Value = AbilityState.Canceled;
                entity.Del<TargetNotFoundedTag>();
                entity.Del<NeedStartAbilityFromMeToTargetCommand>();
                entity.Del<AbilityC>();
            }

            foreach (var i in _processAbilityFromMeToUnit){
                ref var entity = ref _processAbilityFromMeToUnit.GetEntity(i);
                ref var unit = ref _processAbilityFromMeToUnit.Get1(i);
                ref var target = ref _processAbilityFromMeToUnit.Get2(i);
                ref var weapon = ref _processAbilityFromMeToUnit.Get3(i);
                ref var ability = ref _processAbilityFromMeToUnit.Get4(i);

                entity.Del<AttackEventWeapon<T>>();

                ability.Value.State.Value = AbilityState.InProgress;

                ChangeCharacteristic(ref entity, ref unit, ref target, weapon, ability.Value.Cell.Body);

                ability.Value.State.Value = AbilityState.Completed;

                entity.Del<NeedStartAbilityFromMeToTargetCommand>();
                entity.Del<AbilityC>();
            }
        }

        private void ChangeCharacteristic(ref EcsEntity entity, ref UnitC unit, ref FoundTargetC target,
            Weapon<T> weapon, TemplateAbility ability){
            if (ability.changeableCharacteristicType == ChangeableCharacteristicType.HealthPoints){
                if (ability.effectValueType == EffectValueType.Negative){
                    FightLibrary.SingleMomentumAttackWithModifierFromAbility(target.Value, weapon, entity, ability);
                } else{
                    unit.Health.CurrentHp += ability.value;
                }
            }
        }
    }
}