using System;
using System.Collections.Generic;
using EcsBattle.Components;
using EcsBattle.Systems.Animation;
using EcsBattle.Systems.Attacks;
using EcsBattle.Systems.BattleLiveCycles;
using EcsBattle.Systems.Camera;
using EcsBattle.Systems.Enemies;
using EcsBattle.Systems.Input;
using EcsBattle.Systems.Player;
using EcsBattle.Systems.PlayerMove;
using EcsBattle.Systems.Statistics;
using EcsBattle.Systems.Ui;
using Extension;
using Leopotam.Ecs;
#if UNITY_EDITOR
using Leopotam.Ecs.UnityIntegration;
#endif
using UnityEngine;


namespace EcsBattle
{
    public sealed class EcsBattle : MonoBehaviour
    {
        #region Fields

        private EcsWorld _world;
        private EcsSystems _execute;
        private EcsSystems _fixedExecute;
        private EcsSystems _lateExecute;
        private readonly List<object> _listForInject = new List<object>();
        private bool _enable = false;

        #endregion


        public void Inject(object obj)
        {
            if (obj == null) return;
            _listForInject.Add(obj);
            Dbg.Log($"Inject object in EcsWorld:{obj}");
        }

        public void Init()
        {
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
                //GameManager
                .Add(new CreateTimerStatisticsObserverSystem())
                .Add(new TimerTickForStatisticsObserverSystem())
                
                .Add(new SpawnGoalLevelSystem())
                .Add(new EndOfBattleSystem())
                //Create Player & Camera
                .Add(new CreatePlayerEntitySystem())
                .Add(new CreateThirdCameraEntitySystem())
                //InputControl
                .Add(new CreateInputControlSystem())
                .Add(new TimerJoystickSystem())
                //    EVENTS Click\Swipe\Movement
                .Add(new GetClickInInputControlSystem())
                .Add(new GetMovementInInputControlSystem())
                .Add(new GetSwipeInInputControlSystem())
                .Add(new BindingEventsToActionSystem())
                //UI Player
                .Add(new UpdatePlayerHealthPointsInUiSystem())
                .Add(new UpdateTargetInUiSystem())
                //Player
                //    Movement
                .Add(new MovementPlayer1SetDirectionSystem())
                .Add(new MovementPlayer2CalculateStepValueSystem());
            _fixedExecute
                .Add(new MovementPlayer3MoveRigidBodySystem())
                .Add(new MovementPlayer4RotateTransformSystem());
            //////////////
            //if need using Rotate and Move camera systems than remove code in CreateThirdCameraEntitySystem
            //Moving Camera
            // _lateExecute
            // .Add(new CameraRotateOnPlayerSystem())
            // .Add(new CameraPositioningOnMarkerPlayerSystem());
            //////////////
            _execute

                //Attack
                .Add(new TimerForGettingPermissionAttackFromMainWeaponSystem())
                .Add(new TimerForGettingPermissionAttackFromSecondWeaponSystem())
                .Add(new Attack1StartProcessSystem())
                .Add(new Attack2StartGetTargetSystem())
                .Add(new Attack3LookAtTargetSystem())
                .Add(new Attack4MoveToTargetSystem())
                // .Add(new Attack5StartAnimationStrikeSystem())
                .Add(new Attack6StartTimerLagBeforeAttackFromMainWeaponSystem())
                .Add(new Attack6StartTimerLagBeforeAttackFromSecondWeaponSystem());
            switch (GlobalLinks.Player.CharacterClass.Class)
            {
                case CharacterClass.Warrior:
                    _execute
                        .Add(new Attack7FinalSplashAttackForPlayerFromMainWeaponSystem())
                        .Add(new Attack7FinalSplashAttackForPlayerFromSecondWeaponSystem());
                    break;

                case CharacterClass.Rogue:
                    _execute
                        .Add(new Attack7FinalTargetAttackForPlayerFromMainWeaponSystem())
                        .Add(new Attack7FinalTargetForPlayerFromSecondWeaponSystem());
                    break;

                case CharacterClass.Hunter:
                    _execute
                        .Add(new CreatePoolOfAmmunitionForRangeWeaponSystem(5))
                        .Add(new Attack7StartRangeTargetAttackForPlayerFromMainWeaponSystem())
                        .Add(new Attack8MoveBulletRangeTargetAttackForPlayerFromMainWeaponSystem());
                    break;

                case CharacterClass.Mage:
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }

            _execute
                .Add(new ApplyDamageInUnitSystem())

                //Enemies
                .Add(new CreateEnemyEntitySystem())
                .Add(new RotateUiHeathBarsToCameraSystem())
                //Ui Enemies
                .Add(new UpdateEnemiesCurrentHealthPointsSystem())
                .Add(new ShowUiMessageByDamageSystem())

                //     //Death Units
                .Add(new ProcessingUnitEventDeathSystem())
                // Animation
                .Add(new AnimationMoveForPlayerSystem())
                .Add(new AnimationBattleState())

                //Battle Enemies
                //Vision
                .Add(new TimerForCheckVisionForUnitsSystem(2.0f))
                .Add(new SearchClosesTargetForUnitsSystem())
                //Moving
                .Add(new CalculateStepForUnitsToTargetSystem())
                .Add(new AnimationMoveSystemByStepSystem());
            _fixedExecute
                .Add(new MovementUnitByStepSystem());
            _execute
                //Attack
                .Add(new StartAnimationStrikeFromMainWeaponForUnitsSystem())
                .Add(new StartAnimationStrikeFromSecondWeaponForUnitsSystem())
                .Add(new FinalAttackForUnitsFromMainWeaponSystem())
                .Add(new FinalAttackForUnitsFromSecondWeaponSystem())
                .Add(new UpdateStatisticsOfBattleGroundSystem())
                ;

            // register one-frame components (order is important), for example:
            // .OneFrame<TestComponent1> ()
            // .OneFrame<TestComponent2> ()

            // inject service instances here (order doesn't important), for example:
            // .Inject (new CameraService ())
            // .Inject (new NavMeshSupport ())
            foreach (var obj in _listForInject)
            {
                Debug.Log(obj);
                _execute.Inject(obj);
            }

            foreach (var obj in _listForInject)
                _fixedExecute.Inject(obj);
            foreach (var obj in _listForInject)
                _lateExecute.Inject(obj);

            _execute.Init();
            _fixedExecute.Init();
            _lateExecute.Init();

            _enable = true;
        }


        #region ClassLiveCycles

        void OnDestroy()
        {
            if (_execute != null)
            {
                _execute.Destroy();
                _execute = null;
            }

            if (_fixedExecute != null)
            {
                _fixedExecute.Destroy();
                _fixedExecute = null;
            }

            if (_lateExecute != null)
            {
                _lateExecute.Destroy();
                _lateExecute = null;
            }

            if (_execute == null && _fixedExecute == null && _lateExecute == null)
            {
                _world?.Destroy();
                _world = null;
            }
        }

        public void Execute()
        {
            _execute?.Run();
        }

        public void FixedExecute()
        {
            _fixedExecute?.Run();
        }

        public void LateExecute()
        {
            _lateExecute?.Run();
        }

        #endregion
    }

    public sealed class EndOfBattleSystem : IEcsRunSystem
    {
        private EcsFilter<GoalLevelComponent, GoalLevelAchievedComponent> _filter;

        public void Run()
        {
            foreach (var i in _filter)
            {
                ref var entity = ref _filter.GetEntity(i);

                Time.timeScale = 0;
            }
        }
    }

    public sealed class TimerForCheckingWinningConditionsSystem : IEcsRunSystem
    {
        public void Run()
        {
        }
    }
}