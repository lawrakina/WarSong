using Code.Extension;
using Code.Profile;
using UnityEngine;


namespace Code.UI.TopNavigation
{
    public sealed class TopNavigationController: BaseController
    {
        private readonly Transform _placeForUi;
        private readonly ProfilePlayer _profilePlayer;
        private TopNavigationView _view;

        public TopNavigationController(Transform placeForUi, ProfilePlayer profilePlayer)
        {
            _placeForUi = placeForUi;
            _profilePlayer = profilePlayer;
            
            _view = ResourceLoader.InstantiateObject(
                _profilePlayer.Settings.UiViews.TopNavigation, _placeForUi, false);
            AddGameObjects(_view.gameObject);
            _view.Init();
        }
    }
}