using System;
using Code.Extension;
using Code.Profile;
using UnityEngine;


namespace Code.UI.Adventure
{
    public sealed class AdventureController : BaseController
    {
        private readonly Transform _placeForUi;
        private readonly ProfilePlayer _profilePlayer;
        private AdventureView _view;

        public AdventureController(Transform placeForUi, ProfilePlayer profilePlayer)
        {
            _placeForUi = placeForUi;
            _profilePlayer = profilePlayer;

            _view = ResourceLoader.InstantiateObject(_profilePlayer.Settings.UiViews.AdventureView, _placeForUi,false);
            AddGameObjects(_view.gameObject);
            _view.Init();
        }
    }
}