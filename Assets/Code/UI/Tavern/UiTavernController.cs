using Code.Extension;
using Code.Profile;
using Code.UI.Tavern;
using UnityEngine;


namespace Code
{
    public sealed class UiTavernController : BaseController
    {
        private readonly Transform _placeForUi;
        private readonly ProfilePlayer _profilePlayer;
        private TavernView _view;

        public UiTavernController(Transform placeForUi, ProfilePlayer profilePlayer)
        {
            _placeForUi = placeForUi;
            _profilePlayer = profilePlayer;
            
            _view = ResourceLoader.LoadAndInstantiateObject<TavernView>($"Prefabs/tavernView", placeForUi, false);
            AddGameObjects(_view.gameObject);
            _view.Init();
        }
    }
}