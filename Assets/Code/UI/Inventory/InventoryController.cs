using Code.Extension;
using Code.Profile;
using UnityEngine;


namespace Code.UI.Inventory
{
    public sealed class InventoryController : BaseController
    {
        private readonly Transform _placeForUi;
        private readonly ProfilePlayer _profilePlayer;
        private InventoryView _view;

        public InventoryController(bool activate, Transform placeForUi, ProfilePlayer profilePlayer): base(activate)
        {
            _placeForUi = placeForUi;
            _profilePlayer = profilePlayer;

            _view = ResourceLoader.InstantiateObject(_profilePlayer.Settings.UiViews.Inventory, _placeForUi, false);
            AddGameObjects(_view.gameObject);
            _view.Init();
            
            Init(activate);
        }
    }
}