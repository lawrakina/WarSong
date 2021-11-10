using System.Collections.Generic;
using Code.Extension;
using Code.Fight.EcsFight.Animator;
using Code.Fight.EcsFight.Battle;
using Code.Fight.EcsFight.Camera;
using Code.Fight.EcsFight.Create;
using Code.Fight.EcsFight.Input;
using Code.Fight.EcsFight.Movement;
using Code.GameCamera;
using Code.Profile;
using Code.Profile.Models;
using Code.Unit;
using Leopotam.Ecs;
using ThirdPersonCameraWithLockOn;
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
                .Add(new AttackUnitS(5))
                .Add(new UnitBehaviourSettingsS())
                .Add(new CameraUpdateS())
                .Add(new SearchTargetS())
                .Add(new MovementUnitS())
                .Add(new WeaponsInUnitS())
                .Add(new AnimationUnitS())
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

    public class CreateEnemiesS : IEcsInitSystem{
        private EcsWorld _world;
        private EnemiesLevelModel _enemiesModel;
        private BattleCamera _camera;
        private InOutControlFightModel _model;

        public void Init(){
            foreach (var view in _enemiesModel.Enemies){
                view.HealthBar.SetCamera(_camera.transform);

                var entity = _world.NewEntity();
                entity.Get<EnemyTag>();
                entity.Get<AnimatorTag>();
                ref var unit = ref entity.Get<UnitC>();
                entity.Get<UnitC>().UnitMovement = view.UnitMovement;
                entity.Get<UnitC>().Animator = view.AnimatorParameters;
                entity.Get<UnitC>().Characteristics = view.UnitCharacteristics;
                entity.Get<UnitC>().Health = view.UnitHealth;
                entity.Get<UnitC>().Resource = view.UnitResource;
                entity.Get<UnitC>().UnitVision = view.UnitVision;
                entity.Get<UnitC>().Reputation = view.UnitReputation;
                entity.Get<UnitC>().UnitLevel = view.UnitLevel;
                entity.Get<UiEnemyHealthBarC>().value = view.HealthBar;

                view.HealthBar.SetOnOff(false);
                //Ragdoll
                // SearchNodesOfRagdoll(entity, view);

                var enemyEntity = new EnemyEntity(view, entity);
            }
        }

        /*        private static void SearchNodesOfRagdoll(EcsEntity entity, IBaseUnitView view)
        {
            entity.Get<ListRigidBAndCollidersComponent>()._rigidBodies
                = new List<Rigidbody>(view.Transform.GetComponentsInChildren<Rigidbody>());
            entity.Get<ListRigidBAndCollidersComponent>()._colliders
                = new List<Collider>(view.Transform.GetComponentsInChildren<Collider>());
            foreach (var rigidbody in entity.Get<ListRigidBAndCollidersComponent>()._rigidBodies)
                rigidbody.isKinematic = true;
            foreach (var collider in entity.Get<ListRigidBAndCollidersComponent>()._colliders)
                collider.enabled = false;
            // view.Rigidbody.isKinematic = false;
            view.Collider.enabled = true;
        }*/
    }

    public struct UiEnemyHealthBarC{
        public HealthBarView value;
    }
}