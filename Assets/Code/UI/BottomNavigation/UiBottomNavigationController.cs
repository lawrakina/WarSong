using Code.Extension;
using Code.Profile;
using UnityEngine;


namespace Code.UI.BottomNavigation
{
    public sealed class UiBottomNavigationController : BaseController
    {
        private Transform _placeForUi;
        private ProfilePlayer _profilePlayer;
        private BottomNavigationView _view;

        public UiBottomNavigationController(Transform placeForUi, ProfilePlayer profilePlayer)
        {
            _placeForUi = placeForUi;
            _profilePlayer = profilePlayer;
            
            _view = ResourceLoader.LoadAndInstantiateObject<BottomNavigationView>($"Prefabs/bottomNavigationView", placeForUi, false);
            AddGameObjects(_view.gameObject);
            _view.Init(ShowAdventure, ShowCharacter, ShowInventory,ShowTavern, ShowShop);
        }

        private void ShowInventory()
        {
            _profilePlayer.CommandManager.ShowInventoryWindow.Execute();
        }
        private void ShowAdventure()
        {
            _profilePlayer.CommandManager.ShowAdventureWindow.Execute();
        }

        private void ShowCharacter()
        {
            _profilePlayer.CommandManager.ShowCharacterWindow.Execute();
        }

        private void ShowTavern()
        {
            _profilePlayer.CommandManager.ShowTavernWindow.Execute();
        }

        private void ShowShop()
        {
            _profilePlayer.CommandManager.ShowShopWindow.Execute();
        }
    }
}