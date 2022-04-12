using System;
using System.Collections;
using System.Collections.Generic;
using Code.Data.Abilities;
using Code.Data.Dungeon;
using Code.Data.Unit;
using Code.Extension;
using Code.Fight.EcsFight.Abilities;
using Code.Fight.EcsFight.Animator;
using Code.Fight.EcsFight.Battle;
using Code.Fight.EcsFight.Camera;
using Code.Fight.EcsFight.Create;
using Code.Fight.EcsFight.Input;
using Code.Fight.EcsFight.Movement;
using Code.Fight.EcsFight.Output;
using Code.Fight.EcsFight.Settings;
using Code.Fight.EcsFight.Timer;
using Code.Profile;
using Code.Profile.Models;
using Code.UI.Fight;
using Leopotam.Ecs;
using UnityEngine;
#if UNITY_EDITOR
using Leopotam.Ecs.UnityIntegration;


#endif
namespace Code.Fight.EcsFight{
    public class EcsFight : BaseController, IExecute, ILateExecute, IFixedExecute{
        /// <summary>
        /// https://github.com/Leopotam/ecs
        /// https://github.com/Leopotam/ecs-unityintegration
        /// </summary>


        #region Fields

        private EcsWorld _world;

        private EcsSystems _execute;
        private EcsSystems _fixedExecute;
        private EcsSystems _lateExecute;
        private readonly List<object> _listForInject = new List<object>();
        private ProfilePlayer _profilePlayer;

        #endregion


        public void Inject(object obj){
            if (obj == null) return;
            _listForInject.Add(obj);
            Dbg.Log($"Inject object in EcsWorld:{obj}");
        }

        public void DisposeWorld(){
            OnDestroy();
        }

        public void Init(ProfilePlayer profilePlayer){
            _profilePlayer = profilePlayer;
            _world = new EcsWorld();
            _execute = new EcsSystems(_world);
            _fixedExecute = new EcsSystems(_world);
            _lateExecute = new EcsSystems(_world);

#if UNITY_EDITOR
            EcsWorldObserver.Create(_world);
            EcsSystemsObserver.Create(_execute);
            EcsSystemsObserver.Create(_fixedExecute);
            EcsSystemsObserver.Create(_lateExecute);
#endif

            _execute
                .Add(new CreatePlayerS())
                .Add(new CreateEnemiesS())
                .Add(new InputControlS())
                .Add(new UnitBehaviourSettingsS())
                .Add(new AbilitiesS<MainHand>())
                .Add(new SearchTargetS())
                .Add(new BattleS<MainHand>(5))
                .Add(new BattleS<SecondHand>(5))
                .Add(new CameraUpdateS())
                .Add(new MovementUnitS())
                .Add(new AnimationUnitS())
                .Add(new DisplayEffectsS())
                .Add(new DeathS())

                //Timers
                .Add(new UniversalTimerS())
                .Add(new TimerS<PermisAttackWeapon<MainHand>>())
                .Add(new TimerS<PermisAttackWeapon<SecondHand>>())
                .Add(new TimerS<LagBeforeAttackWeapon<MainHand>>())
                .Add(new TimerS<LagBeforeAttackWeapon<SecondHand>>())
                .Add(new TimerS<CheckVisionTag>())
                ;
            // .OneFrame<TestComponent1> ()
            // .OneFrame<TestComponent2> ()

            // inject service instances here (order doesn't important), for example:
            // .Inject (new CameraService ())
            // .Inject (new NavMeshSupport ())

            foreach (var obj in _listForInject){
                _execute.Inject(obj);
            }

            foreach (var obj in _listForInject)
                _fixedExecute.Inject(obj);
            foreach (var obj in _listForInject)
                _lateExecute.Inject(obj);

            _execute.Init();
            _fixedExecute.Init();
            _lateExecute.Init();

            Init(true);
        }


        #region ClassLiveCycles

        void OnDestroy(){
            if (_execute != null){
                _execute.Destroy();
                _execute = null;
            }

            if (_fixedExecute != null){
                _fixedExecute.Destroy();
                _fixedExecute = null;
            }

            if (_lateExecute != null){
                _lateExecute.Destroy();
                _lateExecute = null;
            }

            if (_execute == null && _fixedExecute == null && _lateExecute == null){
                _world?.Destroy();
                _world = null;
            }
        }

        #endregion


        public void FixedExecute(float deltaTime){
            _fixedExecute?.Run();
        }

        public void LateExecute(float deltaTime){
            _lateExecute?.Run();
        }

        public void Execute(float deltaTime){
            _execute?.Run();
        }
    }

    public class AbilitiesS<T> : IEcsInitSystem, IEcsRunSystem{
        private EcsWorld _world = null;
        private InOutControlFightModel _model;
        private DungeonParams _dungeonParams;
        private EcsFilter<PlayerTag, UnitC, PermissionForAbilitiesTag> _player;
        private EcsFilter<HubOfAbilitiesTag> _hubOfAbilitiesForPlayer;
        private EcsFilter<UnitC, FoundTargetC, NeedMoveToTargetC, AbilityC> _moveToTargetC;
        private EcsFilter<UnitC, FoundTargetC, Weapon<T>, AbilityC, NeedStartAbilityFromMeToTargetCommand,
            StartAbilityCommand> _startAbilityFromMeToUnit;
        private EcsFilter<UnitC, FoundTargetC, Weapon<T>, AbilityC, AttackEventWeapon<T>, HubOfAbilitiesTag>.Exclude<
            Timer<LagBeforeAttackWeapon<T>>> _processAbilityFromMeToUnit;

        public void Init(){
            foreach (var i in _player){
                ref var unit = ref _player.Get2(i);

                var abilities = _world.NewEntity();
                abilities.Get<HubOfAbilitiesTag>().Owner = unit;
                abilities.Get<HubOfAbilitiesTag>().Source = _model.InputControl.QueueOfAbilities;
                // abilities.Get<HubOfAbilitiesTag>().ChangeStateOfLastAbility = state => { };
            }
        }

        public void Run(){
            foreach (var i in _hubOfAbilitiesForPlayer){
                ref var entity = ref _hubOfAbilitiesForPlayer.GetEntity(i);
                ref var hub = ref _hubOfAbilitiesForPlayer.Get1(i);

                while (hub.Source.Count != 0){
                    var ability = hub.Source.Dequeue();

                    if (0 < (hub.Owner.Resource.ResourceBaseValue - ability.CostResource)){
                        hub.Owner.Resource.ResourceBaseValue -= ability.CostResource;
                        
                        switch (ability.AbilityTargetType){
                            case AbilityTargetType.None:
                                break;
                            case AbilityTargetType.OnlyMe:
                                break;
                            case AbilityTargetType.OnlyTargetEnemy:
                                entity.Get<Timer<BattleTag>>().TimeLeftSec = _dungeonParams.DurationOfAggreState;
                                entity.Get<NeedFindTargetCommand>();
                                entity.Get<NeedMoveToTargetC>();
                                entity.Get<NeedStartAbilityFromMeToTargetCommand>();
                                entity.Get<AbilityC>().Value = ability;
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
                        // hub.ChangeStateOfLastAbility?.Invoke(AbilityState.Start);
                    } else{
                        // hub.ChangeStateOfLastAbility?.Invoke(AbilityState.Cancel);
                    }

                    Dbg.Log($"New Ability Dequeue - {ability.UiInfo.Title}");
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
                    entity.Get<StartAbilityCommand>();
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

                unit.Animator.SetTriggerSpell();

                entity.Get<Timer<LagBeforeAttackWeapon<T>>>().TimeLeftSec = ability.Value.TimeLagBeforeAction;
                entity.Get<AttackEventWeapon<T>>();

                entity.Del<StartAbilityCommand>();
            }

            foreach (var i in _processAbilityFromMeToUnit){
                ref var entity = ref _processAbilityFromMeToUnit.GetEntity(i);
                ref var unit = ref _processAbilityFromMeToUnit.Get1(i);
                ref var target = ref _processAbilityFromMeToUnit.Get2(i);
                ref var weapon = ref _processAbilityFromMeToUnit.Get3(i);
                ref var ability = ref _processAbilityFromMeToUnit.Get4(i);
                ref var hub = ref _processAbilityFromMeToUnit.Get6(i);

                entity.Del<AttackEventWeapon<T>>();

                ability.Value.StartReload();
                Dbg.Log($"asklfdjosalikdjoikasjd aiksdj lkasjd aslkdjas");
                // hub.ChangeStateOfLastAbility?.Invoke(AbilityState.Process);
                ///
                ///
                ///
                /// 

                // hub.ChangeStateOfLastAbility?.Invoke(AbilityState.Complete);
            }
        }
    }

    public struct StartAbilityCommand{
    }

    public struct NeedMoveToTargetC{
    }

    public struct AbilityC{
        public Ability Value;
    }

    public struct NeedStartAbilityFromMeToTargetCommand{
    }
}