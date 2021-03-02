using System.Collections.Generic;
using EcsBattle.Components;
using EcsBattle.Systems.Attacks;
using EcsBattle.Systems.Enemies;
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
                .Add(new StartTimerForVisionPlayerSystem())
                .Add(new TickTimerForVisionForPlayerSystem(1.0f))
                .Add(new SearchClosesTargetForPlayerSystem())
                //Attack
                .Add(new TimerForGetPermissionAttackFromWeaponSystem())
                .Add(new TimerForStartAnimationFromWeaponSystem())
                .Add(new ApplyDamageInUnitSystem())

                //Battle Enemies
                //Vision
                // .Add(new StartTimerFormVisionEnemySystem())
                // .Add(new TickTimerForVisionForEnemySystem(2.0f))
                .Add(new TimerForVisionForEnemySystem(2.0f))
                .Add(new SearchClosesTargetForEnemySystem())
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

    public sealed class TimerForVisionForEnemySystem : IEcsRunSystem
    {
        private readonly float _time;
        private EcsFilter<EnemyComponent> _filter;

        public TimerForVisionForEnemySystem(float time)
        {
            _time = time;
        }

        public void Run()
        {
            foreach (var i in _filter)
            {
                ref var entity = ref _filter.GetEntity(i);

                if (!entity.Has<TimerForVisionForEnemyComponent>())
                {
                    ref var timer = ref entity.Get<TimerForVisionForEnemyComponent>();
                    timer.CurrentTime = Random.Range(0.0f, 0.5f);
                    timer.MaxTime = _time;
                }
                else
                {
                    ref var timer = ref entity.Get<TimerForVisionForEnemyComponent>();
                    timer.CurrentTime += Time.deltaTime;
                    if (timer.CurrentTime > timer.MaxTime)
                    {
                        entity.Del<TimerForVisionForEnemyComponent>();
                        entity.Get<TimerTickedForVisionComponent>();
                    }
                }
            }
        }
    }

    public struct TimerForVisionForEnemyComponent
    {
        public float CurrentTime;
        public float MaxTime;
    }

    // public class TickTimerForVisionForEnemySystem : IEcsRunSystem
    // {
    //     private readonly float _time;
    //     
    //     private EcsFilter<EnemyComponent, AwaitTimerForVisionComponent> _filter;
    //
    //     public TickTimerForVisionForEnemySystem(float time)
    //     {
    //         _time = time;
    //     }
    //
    //     public void Run()
    //     {
    //         foreach (var index in _filter)
    //         {
    //             ref var entity = ref _filter.GetEntity(index);
    //             ref var timer = ref _filter.Get2(index);
    //             if (timer.Value < _time)
    //             {
    //                 timer.Value += Time.deltaTime;
    //             }
    //             else
    //             {
    //                 entity.Del<AwaitTimerForVisionComponent>();
    //                 entity.Get<TimerTickedForVisionComponent>();
    //             }
    //         }
    //     }
    // }
    //
    //
    // public class StartTimerFormVisionEnemySystem : IEcsRunSystem
    // {
    //     private EcsFilter<EnemyComponent>.Exclude<AwaitTimerForVisionComponent> _filter;
    //
    //     public void Run()
    //     {
    //         foreach (var index in _filter)
    //         {
    //             _filter.GetEntity(index).Get<AwaitTimerForVisionComponent>().Value = Random.Range(0.0f, 0.5f);
    //         }
    //     }
    // }

    public class SearchClosesTargetForEnemySystem : IEcsRunSystem
    {
        private EcsFilter<EnemyComponent, BaseUnitComponent, TimerTickedForVisionComponent> _filter;

        public void Run()
        {
            foreach (var i in _filter)
            {
                ref var unit = ref _filter.Get2(i);
                ref var transform = ref _filter.Get2(i).transform;
                ref var distanceDetection = ref _filter.Get2(i).unitVision.BattleDistance;
                ref var reputation = ref _filter.Get2(i).unitReputation;
                var position = transform.position;

                //поиск всех целей
                var colliders = new Collider[unit.unitVision.MaxCountTargets];
                var countColliders =
                    Physics.OverlapSphereNonAlloc(position, distanceDetection, colliders,1<< reputation.EnemyLayer);
                DebugExtension.DebugWireSphere(position, Color.red, distanceDetection, 1.0f);
                var listEnemies = new List<GameObject>();
                Dbg.Log($"countColliders:{countColliders}");

                //проверка прямой видимости
                for (var j = 0; j < countColliders; j++)
                {
                    if (colliders[j] == null) continue;
                    Dbg.Log($"target:{colliders[j]}, colliders:{colliders.Length}");
                    var hit = new RaycastHit[1];
                    var rayDirection = colliders[j].transform.position - transform.position;
                    var countHit = Physics.RaycastNonAlloc(
                        transform.position + new Vector3(0f, 1.0f, 0f), rayDirection, hit,
                        distanceDetection, 1<<reputation.EnemyLayer);
                    // DebugExtension.DebugArrow(transform.position + new Vector3(0f, 1.0f, 0f),
                    // colliders[i].transform.position - transform.position, Color.red, distanceDetection);
                    if (countHit > 0)
                    {
                        // Dbg.Log($"Visible Target:colliders[i].gameObject");
                        listEnemies.Add(colliders[j].gameObject);
                    }
                }

                //находим ближайшего врага
                if (listEnemies.Count >= 1)
                {
                    var targetGo = listEnemies[0];
                    var distance = Mathf.Infinity;
                    foreach (var target in listEnemies)
                    {
                        var diff = target.transform.position - transform.position;
                        var curDistance = diff.sqrMagnitude;
                        if (curDistance < distance)
                        {
                            targetGo = target;
                            distance = curDistance;
                        }
                    }

                    _filter.GetEntity(i).Get<CurrentTargetComponent>().Target = targetGo.transform;
                    _filter.GetEntity(i).Get<CurrentTargetComponent>().Distance = distance;
                }

                //restart timer
                _filter.GetEntity(i).Del<TimerTickedForVisionComponent>();
            }
        }
    }
}