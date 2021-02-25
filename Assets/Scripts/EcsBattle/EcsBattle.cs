using System.Collections.Generic;
using Data;
using EcsBattle.Components;
using EcsBattle.Systems.Input;
using EcsBattle.Systems.PlayerMove;
using EcsBattle.Systems.PlayerVision;
using EcsBattle.Systems.Ui;
using Extension;
using Leopotam.Ecs;
#if UNITY_EDITOR
using Leopotam.Ecs.UnityIntegration;
#endif
using UnityEngine;
using VIew;


namespace EcsBattle
{
    public sealed class EcsBattle : MonoBehaviour
    {
        #region Fields

        private EcsWorld _world;
        private EcsSystems _execute;
        private EcsSystems _fixedExecute;
        private readonly List<object> _listForInject = new List<object>();

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
#if UNITY_EDITOR
            EcsWorldObserver.Create(_world);
            EcsSystemsObserver.Create(_execute);
#endif

            _execute
                //Create Player & Camera
                .Add(new CreatePlayerEntitySystem())
                .Add(new CreateThirdCameraEntitySystem())
                //Moving Camera
                .Add(new CameraPositioningOfPlayerSystem())
                .Add(new CameraRotationOfPlayerSystem())
                //InputControl
                .Add(new CreateInputControlSystem())
                .Add(new SetActiveInputOnJoystickSystem())
                .Add(new GetClickInInputControlSystem())
                //Moving Player
                .Add(new MovementPlayer1SetDirectionSystem())
                .Add(new MovementPlayer2CalculateStepValueSystem());
            _fixedExecute
                .Add(new MovementPlayer3MoveAndRotateRigidBodySystem());
            _execute
                //Animation Player
                .Add(new AnimationMotionPlayerSystem())
                //Enemies
                .Add(new CreateEnemyEntitySystem())
                .Add(new RotateUiHeathBarsToCameraSystem())
                //Ui Enemies
                .Add(new UpdateEnemiesCurrentHealthPointsSystem())
                //UI Player
                .Add(new UpdatePlayerCurrentHealthPointsSystem())
                .Add(new UpdatePlayerMaxHealthPointsSystem())
                
                //Battle Player
                //Vision
                .Add(new StartTimerForVisionSystem())
                .Add(new TickTimerForVisionForPlayerSystem(1.0f))
                .Add(new SearchClosesTargetForPlayerSystem())
                //Attack
                .Add(new OneStrikeFromWeaponSystem())
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

            _execute.Init();
            _fixedExecute.Init();
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

        #endregion
    }

    public sealed class OneStrikeFromWeaponSystem : IEcsRunSystem
    {
        private EcsFilter<TransformComponent, CurrentTargetComponent, NeedAttackComponent, EquipmentWeaponComponent, AnimatorComponent> _filter;
        public void Run()
        {
            foreach (var i in _filter)
            {
                ref var transform = ref _filter.Get1(i);
                ref var target = ref _filter.Get2(i);
                ref var weapon = ref _filter.Get4(i);
                ref var animator = ref _filter.Get5(i);

                var direction = target.Target.position - transform.Value.position;
                var bullet = Object.Instantiate(weapon.Bullet, transform.Value.position + transform.OffsetHead,
                    Quaternion.Euler(direction));
                bullet.Rigidbody.AddForce(direction);
                
                animator.Value.SetTriggerAttack();
            }
        }
    }


    public class UpdateEnemiesCurrentHealthPointsSystem : IEcsRunSystem
    {
        private EcsFilter<UiEnemyHealthBarComponent, UnitHpComponent> _filter;

        public void Run()
        {
            foreach (var index in _filter)
            {
                _filter.Get1(index).Value.ChangeValue(_filter.Get2(index).CurrentValue, _filter.Get2(index).MaxValue);
            }
        }
    }
}