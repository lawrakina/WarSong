using Code.Extension;
using Code.Profile;
using UnityEngine;


namespace Code.UI.Shop
{
    public sealed class ShopController : BaseController
    {
        private readonly Transform _placeForUi;
        private readonly ProfilePlayer _profilePlayer;
        private ShopView _view;

        public ShopController(bool activate, Transform placeForUi, ProfilePlayer profilePlayer): base(activate)
        {
            _placeForUi = placeForUi;
            _profilePlayer = profilePlayer;
            
            _view = ResourceLoader.InstantiateObject(_profilePlayer.Settings.UiViews.Shop, _placeForUi, false);
            AddGameObjects(_view.gameObject);
            _view.Init();
            
            Init(activate);
        }
    }
}