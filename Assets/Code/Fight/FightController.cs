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
        private readonly Transform _placeForUi;
        private readonly ProfilePlayer _profilePlayer;
        private FightDungeonModel _model;

        private BuildStatusCheckerController _buildStatusCheckerController;
        private LoadingController _loadingController;
        private LevelGeneratorController _generator;
        private PlayerFightController _playerFightController;
        private EnemyFightController _enemyFightController;
        private CameraController _cameraController;
        private IBattleController _battleController;
        private UiFightController _uiFightController;

        public FightController(Transform placeForUi, ProfilePlayer profilePlayer,
            CameraController cameraController)
        {
            _placeForUi = placeForUi;
            _profilePlayer = profilePlayer;
            _model = _profilePlayer.Models.FightModel;
            _cameraController = cameraController;

            Controllers.Add(this);

            _loadingController = new LoadingController(
                _profilePlayer.Settings.UiViews, _model.InfoState, _placeForUi);
            Controllers.Add(_loadingController);

            _buildStatusCheckerController = new BuildStatusCheckerController();
            Controllers.Add(_buildStatusCheckerController);
            _buildStatusCheckerController.OnDeactivate();
            _buildStatusCheckerController.CompleteCommand.Subscribe(x =>
            {
                _model.FightState.Value = FightState.Fight;
            }).AddTo(_subscriptions);

            _generator = new LevelGeneratorController(_profilePlayer.Settings.DungeonGeneratorData, _model);
            Controllers.Add(_generator);
            _buildStatusCheckerController.AddToQueue(_generator);

            _playerFightController = new PlayerFightController(
                _profilePlayer, _model);
            Controllers.Add(_playerFightController);
            _buildStatusCheckerController.AddToQueue(_playerFightController);

            _enemyFightController = new EnemyFightController(
                _model,
                _profilePlayer.Models.DungeonGeneratorModel,
                _profilePlayer.Models.EnemiesLevelModel,
                _profilePlayer.Settings.EnemiesData,
                _profilePlayer.CurrentPlayer);
            Controllers.Add(_enemyFightController);
            _buildStatusCheckerController.AddToQueue(_enemyFightController);

            _uiFightController = new UiFightController(_placeForUi, _profilePlayer);
            Controllers.Add(_uiFightController);

            _battleController = new EcsBattleController( _profilePlayer);
            Controllers.Add(_battleController as BaseController);
            (_battleController as BaseController)?.OnDeactivate();

            _battleController.Inject(_profilePlayer.CurrentPlayer);
            _battleController.Inject(_cameraController.BattleCamera);
            _battleController.Inject(_profilePlayer.Models.InOutControlFightModel);
            _battleController.Inject(_profilePlayer.Models.DungeonGeneratorModel.ActiveLevel);
            _battleController.Inject(_profilePlayer.Models.EnemiesLevelModel);

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
                    _cameraController.StartFight();
                    _battleController.StartFight();
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
            Controllers.Remove(_generator);
            Controllers.Remove(_loadingController);
            Controllers.Remove(_playerFightController);
            Controllers.Remove(_enemyFightController);
            Controllers.Remove(_battleController as BaseController);
            Controllers.Remove(this);
            base.Dispose();
        }
    }
}