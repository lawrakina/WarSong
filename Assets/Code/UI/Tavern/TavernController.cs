using System;
using Code.Extension;
using Code.Profile;
using UnityEngine;


namespace Code.UI.Tavern
{
    public sealed class TavernController : BaseController
    {
        private readonly Transform _placeForUi;
        private readonly ProfilePlayer _profilePlayer;
        private TavernView _view;

        public TavernController(Transform placeForUi, ProfilePlayer profilePlayer)
        {
            _placeForUi = placeForUi;
            _profilePlayer = profilePlayer;
            
            _view = ResourceLoader.InstantiateObject(_profilePlayer.Settings.UiViews.TavernView, _placeForUi, false);
            AddGameObjects(_view.gameObject);
            _view.Init();
        }
    }
}