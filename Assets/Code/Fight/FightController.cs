using System;
using Code.Extension;
using Code.Fight.BuildingDungeon;
using Code.GameCamera;
using Code.Loading;
using Code.Profile;
using Code.Profile.Models;
using Code.UI.Fight;
using UniRx;
using UnityEngine;


namespace Code.Fight
{
    public sealed class FightController : BaseController
    {
        private readonly Controllers _controllers;
        private readonly Transform _placeForUi;
        private readonly ProfilePlayer _profilePlayer;
        private FightDungeonModel _model;

        private BuildStatusCheckerController _buildStatusCheckerController;
        private LoadingController _loadingController;
        private LevelGeneratorController _generator;
        private PlayerFightController _playerFightController;
        private EnemyFightController _enemyFightController;
        private CameraController _cameraController;
        private EcsBattleController _ecsBattleController;
        private UiFightController _uiFightController;

        public FightController(Controllers controllers, Transform placeForUi, ProfilePlayer profilePlayer,
            CameraController cameraController)
        {
            _controllers = controllers;
            _placeForUi = placeForUi;
            _profilePlayer = profilePlayer;
            _model = _profilePlayer.Models.FightModel;
            _cameraController = cameraController;

            _controllers.Add(this);

            _loadingController = new LoadingController(
                _profilePlayer.Settings.UiViews, _model.InfoState, _placeForUi);
            AddController(_loadingController);
            _controllers.Add(_loadingController);

            _buildStatusCheckerController = new BuildStatusCheckerController();
            _controllers.Add(_buildStatusCheckerController);
            AddController(_buildStatusCheckerController);
            _buildStatusCheckerController.OnDeactivate();
            _buildStatusCheckerController.CompleteCommand.Subscribe(x =>
            {
                _model.FightState.Value = FightState.Fight;
            }).AddTo(_subscriptions);

            _generator = new LevelGeneratorController(_controllers,
                _profilePlayer.Settings.DungeonGeneratorData, _model);
            _controllers.Add(_generator);
            AddController(_generator);
            _buildStatusCheckerController.AddToQueue(_generator);

            _playerFightController = new PlayerFightController(
                _profilePlayer, _model);
            _controllers.Add(_playerFightController);
            AddController(_playerFightController);
            _buildStatusCheckerController.AddToQueue(_playerFightController);

            _enemyFightController = new EnemyFightController(
                _model,
                _profilePlayer.Models.DungeonGeneratorModel,
                _profilePlayer.Models.EnemiesLevelModel,
                _profilePlayer.Settings.EnemiesData,
                _profilePlayer.CurrentPlayer,
                _profilePlayer.Settings.DungeonGeneratorData.PathfindingConfig);
            _controllers.Add(_enemyFightController);
            AddController(_enemyFightController);
            _buildStatusCheckerController.AddToQueue(_enemyFightController);

            _uiFightController = new UiFightController(_placeForUi, _profilePlayer);
            _controllers.Add(_uiFightController);
            AddController(_uiFightController);

            _ecsBattleController = new EcsBattleController(_controllers, _profilePlayer);
            _controllers.Add(_ecsBattleController);
            AddController(_ecsBattleController);
            _ecsBattleController.OnDeactivate();

            _ecsBattleController.Inject(_profilePlayer.CurrentPlayer);
            _ecsBattleController.Inject(_cameraController.BattleCamera);
            _ecsBattleController.Inject(_profilePlayer.Models.InOutControlFightModel);
            _ecsBattleController.Inject(_profilePlayer.Models.DungeonGeneratorModel.ActiveLevel);
            _ecsBattleController.Inject(_profilePlayer.Models.EnemiesLevelModel);

            _model.FightState.Subscribe(OnChangeFightState).AddTo(_subscriptions);
            _buildStatusCheckerController.OnActivate();
            OnChangeFightState(FightState.BuildingStart);
        }

        private void OnChangeFightState(FightState state)
        {
            Dbg.Log($"OnChangeFightState: {state}");
            switch (state)
            {
                case FightState.Default:
                    break;
                case FightState.BuildingStart:
                    _loadingController.ShowLoading();
                    _generator.BuildDungeon();
                    break;
                case FightState.BuildingProcess:
                    _loadingController.UpdateInfo();
                    break;
                case FightState.BuildingComplete:
                    _loadingController.UpdateInfo();
                    break;
                case FightState.Fight:
                    _ecsBattleController.StartFight();
                    _loadingController.HideLoading();
                    break;
                case FightState.Fail:
                    //Show menu restart
                    break;
                case FightState.Win:
                    //Show win tab and save fight results
                    break;
                case FightState.Pause:
                    //frize time
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(state), state, null);
            }
        }

        public override void Dispose()
        {
            _controllers.Remove(_generator);
            _controllers.Remove(_loadingController);
            _controllers.Remove(_playerFightController);
            _controllers.Remove(_enemyFightController);
            _controllers.Remove(this);
            base.Dispose();
        }
    }
}