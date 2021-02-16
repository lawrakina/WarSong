using System.Collections.Generic;
using Data;
using EcsBattle.Components;
using EcsBattle.Systems.Ui;
using Extension;
using Interface;
using Leopotam.Ecs;
using Unit.Enemies;
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
                //Moving Player
                .Add(new MovementPlayer1SetDirectionSystem())
                .Add(new MovementPlayer2CalculateStepValueSystem());
            _fixedExecute
                .Add(new MovementPlayer3MoveAndRotateRigidBodySystem());
            _execute
                //Animation Player
                .Add(new AnimationMotionPlayerSystem())
                .Add(new CreateEnemyEntitySystem())
                // .Add(new CreateHealthBarForEnemySystem())
                //UI Player
                // .Add(new CreateUiPlayerHealsSystem())
                .Add(new UpdatePlayerCurrentHealthSystem())
                .Add(new UpdatePlayerMaxHealthSystem())
                
                
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

        public void Execute(float deltaTime)
        {
            _execute?.Run();
        }
        
        public void FixedExecute(float fixedDeltaTime)
        {
            _fixedExecute?.Run();
        }

        #endregion
    }

    // public class CreateHealthBarForEnemySystem : IEcsInitSystem
    // {
    //     private EcsFilter<EnemyComponent>
    //     public void Init()
    //     {
    //         
    //
    //             
    //         view.Transform.gameObject.AddCode<HealthBarView>();
    //         var healthBar = view.Transform.gameObject.GetComponent<HealthBarView>();
    //         healthBar.Init(_camera.Transform);
    //         // entity.Get<NeedUpdateCurrentHpFromPlayerComponent>().Value = view.UnitClass.CurrentHp;
    //         // entity.Get<NeedUpdateMaxHpFromPlayerComponent>().Value = view.UnitClass.CurrentHp;
    //
    //         //todo повесить Healthbar на врагов 
    //         //todo прокинуть зависимости 
    //     }
    // }

    // public sealed class CreateUiPlayerHealsSystem : IEcsInitSystem
    // {
    //     private EcsWorld _world;
    //     private BattlePlayerModel _playerModel;
    //     public void Init()
    //     {
    //         var uiHeals = _world.NewEntity();
    //         uiHeals.Get<UiHealthPlayerComponent>().CurrentValue = _playerModel.CurrentHp;
    //         uiHeals.Get<UiHealthPlayerComponent>().MaxValue = _playerModel.MaxHp;
    //     }
    // }


    public sealed class CreateEnemyEntitySystem : IEcsInitSystem
    {
        private EcsWorld _world;
        private List<IEnemyView> _listEnemies;
        
        public void Init()
        {
            foreach (var view in _listEnemies)
            {
                
                var entity = _world.NewEntity();
                entity.Get<EnemyComponent>();
                entity.Get<TransformComponent>().Value = view.Transform;
                entity.Get<RigidBodyComponent>().Value = view.Rigidbody;
                entity.Get<MovementSpeed>().Value = view.CharAttributes.Speed;
                entity.Get<RotateSpeed>().Value = view.CharAttributes.RotateSpeedPlayer;
                entity.Get<AnimatorComponent>().Value = view.AnimatorParameters;
            }
        }
    }
}