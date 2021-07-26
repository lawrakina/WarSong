using Code.Extension;
using Code.Profile;
using Code.UI;
using Code.UI.BottomNavigation;
using Code.UI.TopNavigation;
using UnityEngine;


namespace Code
{
    public sealed class MainMenuController : BaseController
    {
        private readonly ProfilePlayer _profilePlayer;
        private MainMenuView _view;

        public MainMenuController(Transform placeForUi, ProfilePlayer profilePlayer)
        {
            _profilePlayer = profilePlayer;
            _view = ResourceLoader.InstantiateObject(
                _profilePlayer.Settings.UiViews.MainMenu, placeForUi, false);
            AddGameObjects(_view.gameObject);
            _view.Init();

            var topNavController = ConfigureTopNavController(_view._topNavPanel, _profilePlayer);
            var bottomNavController = ConfigureBottomNavController(_view._bottomNavPanel, _profilePlayer);
            var contentController = ConfigureContentController(_view._contentPanel, _profilePlayer);
        }

        private MainContentController ConfigureContentController(Transform placeForUi, ProfilePlayer profilePlayer)
        {
            var controller = new MainContentController(placeForUi.transform, profilePlayer);
            AddController(controller);
            return controller;
        }

        private BottomNavigationController ConfigureBottomNavController(Transform placeForUi,
            ProfilePlayer profilePlayer)
        {
            var controller = new BottomNavigationController(placeForUi.transform, profilePlayer);
            AddController(controller);
            return controller;
        }

        private TopNavigationController ConfigureTopNavController(Transform placeForUi, ProfilePlayer profilePlayer)
        {
            var controller = new TopNavigationController(placeForUi.transform, profilePlayer);
            AddController(controller);
            return controller;
        }
    }
}