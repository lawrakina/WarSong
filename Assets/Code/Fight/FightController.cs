using System;
using Code.Extension;
using Code.Profile;
using Code.UI;
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
        private LevelGeneratorController _generator;
        private FightState _oldState;

        public FightController(Controllers controllers, Transform placeForUi, ProfilePlayer profilePlayer)
        {
            _controllers = controllers;
            _placeForUi = placeForUi;
            _profilePlayer = profilePlayer;

            _controllers.Add(this);
            
            _loadingView = ResourceLoader.InstantiateObject(
                _profilePlayer.Settings.UiViews.LoadingView, placeForUi, false);
            AddGameObjects(_loadingView.gameObject);
            _loadingView.Init();
            _loadingView.Hide();

            _generator = new LevelGeneratorController(_profilePlayer.Settings.DungeonGeneratorData);
            _controllers.Add(_generator);
            AddController(_generator);
            _generator.FightState.Subscribe(OnChangeFightState).AddTo(_subscriptions);
            
            _generator.GenerateDemoLevel();
            _generator.BuildDungeon();
        }

        private void OnChangeFightState(FightState state)
        {
            if (state == _oldState) return;
            Dbg.Log($"OnChangeFightState: {state}");
            switch (state)
            {
                case FightState.Default:
                    break;
                case FightState.BuildingStart:
                    _loadingView.Show();
                    break;
                case FightState.BuildingProcess:
                    break;
                case FightState.BuildingComplete:
                    _loadingView.Hide();
                    break;
                case FightState.Fight:
                    break;
                case FightState.Fail:
                    break;
                case FightState.Win:
                    break;
                case FightState.Pause:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(state), state, null);
            }
        }

        protected override void OnDispose()
        {
            _controllers.Remove(_generator);
            _controllers.Remove(this);
            base.OnDispose();
        }
    }
}