using System;
using Code.Extension;
using Code.Loading;
using Code.Profile;
using Code.Profile.Models;
using UniRx;
using UnityEngine;


namespace Code.Fight
{
    public sealed class FightController: BaseController
    {
        private readonly Controllers _controllers;
        private readonly Transform _placeForUi;
        private readonly ProfilePlayer _profilePlayer;
        private LoadingView _loadingView;
        private FightDungeonModel _model;
        
        private LevelGeneratorController _generator;

        private LoadingController _loadingController;

        private PlayerFightController _playerFightController;

        private EnemyFightController _enemyFightController;

        public FightController(Controllers controllers, Transform placeForUi, ProfilePlayer profilePlayer)
        {
            _controllers = controllers;
            _placeForUi = placeForUi;
            _profilePlayer = profilePlayer;
            _model = _profilePlayer.Models.FightModel;

            _controllers.Add(this);

            _loadingController = new LoadingController(
                _profilePlayer.Settings.UiViews, _model.InfoState, _placeForUi);
            AddController(_loadingController);
            _controllers.Add(_loadingController);

            _generator = new LevelGeneratorController(_controllers,
                _profilePlayer.Settings.DungeonGeneratorData, _model);
            _controllers.Add(_generator);
            AddController(_generator);

            _playerFightController = new PlayerFightController(
                _profilePlayer, _model);
            _controllers.Add(_playerFightController);
            AddController(_playerFightController);

            _enemyFightController = new EnemyFightController(
                _profilePlayer.Models.DungeonGeneratorModel,
                _profilePlayer.Models.EnemiesLevelModel);
            _controllers.Add(_enemyFightController);
            AddController(_enemyFightController);
            
            _model.FightState.Subscribe(OnChangeFightState).AddTo(_subscriptions);
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
                    _enemyFightController.SpawnEnemies();
                    _model.FightState.Value = FightState.Fight;
                    break;
                case FightState.Fight:
                    //ToDo for me
                    //_cameraController.SetMode(state);
                    // _inputController.ShowFightInterface();
                    // _ecsBattle.StartFight();
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

        protected override void OnDispose()
        {
            _controllers.Remove(_generator);
            _controllers.Remove(_loadingController);
            _controllers.Remove(_playerFightController);
            _controllers.Remove(_enemyFightController);
            _controllers.Remove(this);
            base.OnDispose();
        }
    }
}