using Code.Extension;
using Code.Profile;
using Code.UI;
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

            _generator = new LevelGeneratorController(_profilePlayer.Settings.DungeonGeneratorData);
            _controllers.Add(_generator);
            AddController(_generator);
        }

        protected override void OnDispose()
        {
            _controllers.Remove(_generator);
            _controllers.Remove(this);
            base.OnDispose();
        }
    }
}