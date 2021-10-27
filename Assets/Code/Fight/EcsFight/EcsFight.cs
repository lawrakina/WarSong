using System.Collections.Generic;
using Code.Extension;
using Code.Fight.EcsFight.Animator;
using Code.Fight.EcsFight.Battle;
using Code.Fight.EcsFight.Create;
using Code.Fight.EcsFight.Input;
using Code.Fight.EcsFight.Movement;
using Code.Profile;
using Code.Profile.Models;
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
                .Add(new InputControlS())
                .Add(new UnitBehaviourSettingsS())
                .Add(new SearchTargetS())
                .Add(new MovementExecuteUnitS());
            _fixedExecute
                .Add(new MovementFixedExecuteUnitS());
            _execute
                .Add(new WeaponsInUnitS())
                // .Add(new AttackUnitS())
                .Add(new AnimationUnitS())
                ;

            // register one-frame components (order is important), for example:
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

    public class SearchTargetS : IEcsInitSystem, IEcsRunSystem{
        private EcsWorld _world = null;
        private InOutControlFightModel _model;
        private EcsFilter<UnitC, NeedFindTargetTag> _searchFilter;

        public void Init(){
            foreach (var i in _searchFilter){
                ref var entity = ref _searchFilter.GetEntity(i);
                ref var list =ref entity.Get<ListOfTargetsC>();
                list.Value = new List<UnitC>();
            }
        }

        public void Run(){
            foreach (var i in _searchFilter){
                
            }
        }
    }

    public struct ListOfTargetsC{
        public List<UnitC> Value;
    }

    public struct NeedAttackTargetC{
    }

    public struct NeedFindTargetTag{
    }
}