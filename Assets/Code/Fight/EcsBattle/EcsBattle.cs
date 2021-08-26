using System;
using System.Collections.Generic;
using Code.Data;
using Code.Extension;
using Code.Fight.EcsBattle.Input;
using Code.Fight.EcsBattle.Out.Gui;
using Code.Fight.EcsBattle.Statistics;
using Code.Fight.EcsBattle.Unit;
using Code.Fight.EcsBattle.Unit.Animation;
using Code.Fight.EcsBattle.Unit.Attack;
using Code.Fight.EcsBattle.Unit.Create;
using Code.Fight.EcsBattle.Unit.EnemyHealthBars;
using Code.Fight.EcsBattle.Unit.Move;
using Code.Fight.EcsBattle.Unit.Vision;
using Code.Profile;
using Leopotam.Ecs;
#if UNITY_EDITOR
using Leopotam.Ecs.UnityIntegration;
#endif


namespace Code.Fight.EcsBattle
{
    public sealed class EcsBattle : BaseController, IExecute, ILateExecute, IFixedExecute
    {
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
        private bool _enable = false;
        private ProfilePlayer _profilePlayer;

        #endregion
        
        
        public Guid Id => Guid.Empty;


        public void Inject(object obj)
        {
            if (obj == null) return;
            _listForInject.Add(obj);
            Dbg.Log($"Inject object in EcsWorld:{obj}");
        }
        
        public void OnExecute()
        {
            _enable = true;
        }

        public void OffExecute()
        {
            _enable = false;
        }

        public void DisposeWorld()
        {
            OnDestroy();
        }

        public void Init(ProfilePlayer profilePlayer)
        {
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
                //GameManager
                .Add(new CreateTimerStatisticsObserverSystem())
                .Add(new TimerTickForStatisticsObserverSystem())
                // .Add(new SpawnGoalLevelSystem())
                // .Add(new EndOfBattleSystem())
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
            switch (_profilePlayer.CurrentPlayer.CharacterClass.Class)
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
                    _execute
                        .Add(new CreatePoolOfAmmunitionForRangeWeaponSystem(5))
                        .Add(new Attack7StartRangeTargetAttackForPlayerFromMainWeaponSystem())
                        .Add(new Attack8MoveBulletRangeTargetAttackForPlayerFromMainWeaponSystem());
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
                
                //оно крашит всё
                .Add(new CheckForEnemyInSightSystem())
                
                
                //Включить чтоб здоровье отображалось только на виду у игрока
                //.Add(new ShowHealthBarForEnemiesInSightSystem())
                // .Add(new ShowUiMessageByDamageSystem())

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

                //Regeneration
                .Add(new RegenerationHealthSystem())
                ;

            // register one-frame components (order is important), for example:
            // .OneFrame<TestComponent1> ()
            // .OneFrame<TestComponent2> ()

            // inject service instances here (order doesn't important), for example:
            // .Inject (new CameraService ())
            // .Inject (new NavMeshSupport ())
            foreach (var obj in _listForInject)
            {
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

        #endregion



        public bool IsOn => _enable;
        public void FixedExecute(float deltaTime)
        {
            _fixedExecute?.Run();
        }

        public void LateExecute(float deltaTime)
        {
            _lateExecute?.Run();
        }

        public void Execute(float deltaTime)
        {
            _execute?.Run();
        }
    }
}