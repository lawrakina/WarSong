using Code.Profile;
using UnityEngine;


namespace Code
{
    public sealed class FightController: BaseController
    {
        private readonly Controllers _controllers;
        private readonly Transform _placeForUi;
        private readonly ProfilePlayer _profilePlayer;

        public FightController(Controllers controllers, Transform placeForUi, ProfilePlayer profilePlayer)
        {
            _controllers = controllers;
            _placeForUi = placeForUi;
            _profilePlayer = profilePlayer;

            _controllers.Add(this);
            
            
        }

        protected override void OnDispose()
        {
            _controllers.Remove(this);
            base.OnDispose();
        }
    }
}