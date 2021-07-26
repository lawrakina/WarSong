using Code.Extension;
using Code.Profile;
using UniRx;
using UnityEngine;


namespace Code.UI.Character
{
    public sealed class CharacterController : BaseController
    {
        private readonly Transform _placeForUi;
        private readonly ProfilePlayer _profilePlayer;
        private CharacterView _view;

        public CharacterController(Transform placeForUi, ProfilePlayer profilePlayer)
        {
            _placeForUi = placeForUi;
            _profilePlayer = profilePlayer;

            _view = ResourceLoader.InstantiateObject(
                _profilePlayer.Settings.UiViews.CharacterView, _placeForUi, false);
            AddGameObjects(_view.gameObject);
            _profilePlayer.InfoAboutCurrentPlayer
                .Subscribe(info => _view.InfoFormatted = info.AllCharacteristics).AddTo(_subscriptions);
            _view.Init();
        }
    }
}