using Code.Data;
using Code.Extension;
using Code.Profile;
using UnityEngine;


namespace Code.UI.BottomNavigation
{
    public sealed class BottomNavigationController : BaseController
    {
        private Transform _placeForUi;
        private ProfilePlayer _profilePlayer;
        private BottomNavigationView _view;

        public BottomNavigationController(Transform placeForUi, ProfilePlayer profilePlayer)
        {
            _placeForUi = placeForUi;
            _profilePlayer = profilePlayer;
            
            _view = ResourceLoader.InstantiateObject(
                _profilePlayer.Settings.UiViews.BottomNavigation, _placeForUi, false);
            AddGameObjects(_view.gameObject);
            _view.Init(ShowAdventure, ShowCharacter, ShowInventory, ShowTavern, ShowShop);

            switch (_profilePlayer.WindowAfterStart)
            {
                case UiWindowAfterStart.Adventure:
                    _view._adventureToggle.isOn = true;
                    break;

                case UiWindowAfterStart.Characters:
                    _view._charToggle.isOn = true;
                    break;

                case UiWindowAfterStart.Inventory:
                    _view._inventoryToggle.isOn = true;
                    break;

                case UiWindowAfterStart.Tavern:
                    _view._tavernToggle.isOn = true;
                    break;

                case UiWindowAfterStart.Shop:
                    _view._shopToggle.isOn = true;
                    break;
            }
        }

        private void ShowAdventure()
        {
            _profilePlayer.CommandManager.ShowAdventureWindow.Execute();
        }

        private void ShowCharacter()
        {
            _profilePlayer.CommandManager.ShowCharacterWindow.Execute();
        }
        
        private void ShowInventory()
        {
            _profilePlayer.CommandManager.ShowInventoryWindow.Execute();
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