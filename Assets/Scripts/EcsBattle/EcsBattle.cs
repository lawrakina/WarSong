using System;
using System.Collections.Generic;
using DuloGames.UI;
using EcsBattle.Systems.Animation;
using EcsBattle.Systems.Attacks;
using EcsBattle.Systems.Camera;
using EcsBattle.Systems.Enemies;
using EcsBattle.Systems.Input;
using EcsBattle.Systems.Player;
using EcsBattle.Systems.PlayerMove;
using EcsBattle.Systems.PlayerVision;
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
            _listForInject.Add(obj);
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
                //Create Player & Camera
                .Add(new CreatePlayerEntitySystem());
            _execute
                .Add(new CreateThirdCameraEntitySystem());
            _execute
                //InputControl
                .Add(new CreateInputControlSystem())
                .Add(new TimerJoystickSystem())

                //EVENTS Click\Swipe\Movement
                .Add(new GetClickInInputControlSystem())
                .Add(new GetMovementInInputControlSystem())
                .Add(new GetSwipeInInputControlSystem())

                //Player
                //Movement
                .Add(new MovementPlayer1SetDirectionSystem())
                .Add(new MovementPlayer2CalculateStepValueSystem());
            _fixedExecute
                .Add(new MovementPlayer3MoveRigidBodySystem())
                .Add(new MovementPlayer4RotateTransformSystem());
            
            //////////////
            //if us Rotate and Move camera systems than remove code in CreateThirdCameraEntitySystem
            //Moving Camera
            // _lateExecute
                // .Add(new CameraRotateOnPlayerSystem())
                // .Add(new CameraPositioningOnMarkerPlayerSystem());
            //////////////
            
            _execute

                //     

                // .Add(new TimerStopFollowingCameraInPlayerSystem())
                // .Add(new NeedLerpPositionCameraFollowingToTargetSystem())
                // .Add(new EnableFollowingCameraInPlayerNonBattleSystem())
                //     //Rotation Player
                //     .Add(new RestoreSavedRotationInUnitSystem())
                //     //Moving Player
                //     .Add(new MovementPlayer1SetDirectionSystem())
                //     .Add(new MovementPlayer2CalculateStepValueSystem());
                // _fixedExecute
                //     .Add(new MovementPlayer3MoveAndRotateRigidBodySystem());
                // _execute
                //     //Animation Player
                // .Add(new AnimationMoveSystem())
                //     //Enemies
                //     .Add(new CreateEnemyEntitySystem())
                //     .Add(new RotateUiHeathBarsToCameraSystem())
                //     //Ui Enemies
                //     .Add(new UpdateEnemiesCurrentHealthPointsSystem())
                //     //UI Player
                .Add(new UpdatePlayerHealthPointsInUiSystem())
                //
                //     //Battle Player
                //     //Vision
                //     .Add(new StartTimerForVisionPlayerSystem())
                //     .Add(new TickTimerForVisionForPlayerSystem(0.05f))
                //     .Add(new SearchClosesTargetForPlayerSystem())
                //     .Add(new AnimationBattleState())
                //     //Attack
                //     .Add(new TimerForGetPermissionAttackFromWeaponSystem())
                //     .Add(new TimerForStartAnimationFromWeaponSystem())
                //     .Add(new ApplyDamageInUnitSystem())
                //
                //     //Battle Enemies
                //     //Vision
                //     .Add(new TimerForVisionForEnemySystem(2.0f))
                //     .Add(new SearchClosesTargetForEnemySystem())
                //     //Moving
                //     .Add(new MovementEnemyToTargetSystem())
                //
                //     //Death Units
                //     .Add(new EnableRagdollByDeathSystem())
                ;

            // register one-frame components (order is important), for example:
            // .OneFrame<TestComponent1> ()
            // .OneFrame<TestComponent2> ()

            // inject service instances here (order doesn't important), for example:
            // .Inject (new CameraService ())
            // .Inject (new NavMeshSupport ())
            foreach (var obj in _listForInject)
                _execute.Inject(obj);
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
                _world.Destroy();
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
            // Dbg.Log($"LateExecute");
            _lateExecute?.Run();
        }

        #endregion


        private void LateUpdate()
        {
            if (_enable)
            {
                // Dbg.Log($"LateUpdate");
                 // CameraRotateOnPlayerSystem()
                // CameraPositioningOnMarkerPlayerSystem()
                // _lateExecute?.Run();
            }
        }
    }


    public sealed class MovementEnemyToTargetSystem : IEcsRunSystem
    {
        // private EcsFilter<EnemyComponent, BaseUnitComponent, CurrentTargetComponent, MovementSpeed, BattleInfoComponent>
        // _filter;

        public void Run()
        {
            // foreach (var i in _filter)
            // {
            //     ref var entity = ref _filter.GetEntity(i);
            //     ref var unit = ref _filter.Get2(i);
            //     ref var target = ref _filter.Get3(i);
            //     ref var moveSpeed = ref _filter.Get4(i);
            //     ref var weapon = ref _filter.Get5(i);
            //
            //     var distanceVector = (target.Target.position - unit.transform.position);
            //     var sqrDistance = distanceVector.sqrMagnitude;
            //     var direction = distanceVector * (moveSpeed.Value * Time.deltaTime);
            //
            //     if (sqrDistance > (weapon.Value.AttackDistance * weapon.Value.AttackDistance))
            //     {
            //         Dbg.Log(
            //             $"sqrDistance:{sqrDistance}, sqrAttackDistance:{weapon.Value.AttackDistance * weapon.Value.AttackDistance}");
            //         direction = distanceVector * (moveSpeed.Value * Time.deltaTime);
            //         unit.rigidbody.MovePosition(unit.transform.position + direction);
            //         unit.animator.Speed = direction.magnitude;
            //     }
            //     else
            //     {
            //         unit.animator.Speed = 0.0f;
            //         entity.Get<NeedAttackComponent>();
            //     }
            //
            //     unit.transform.LookAt(target.Target.position.Change(y: 0.0f));
            // }
        }
    }
}