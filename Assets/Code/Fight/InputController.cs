using Code.Profile;
using UniRx;
using UnityEngine;
using Object = UnityEngine.Object;


namespace Code.Fight
{
    public class InputController : BaseController
    {
        private readonly Transform _placeForUi;
        private readonly ProfilePlayer _profilePlayer;
        private UltimateJoystick _joystick;

        public InputController(Transform placeForUi, ProfilePlayer profilePlayer)
        {
            _placeForUi = placeForUi;
            _profilePlayer = profilePlayer;

            _joystick = Object.Instantiate(_profilePlayer.Settings.FightInputData.Joystick,_placeForUi);
            AddGameObjects(_joystick.gameObject);
            
            _profilePlayer.Models.FightModel.FightState.Subscribe(ShowJoystick).AddTo(_subscriptions);

            _profilePlayer.Models.InOutControlFightModel.InputControl.Joystick = _joystick;
        }

        private void ShowJoystick(FightState state)
        {
            if (state == FightState.Fight)
            {
                _joystick.EnableJoystick();
            }
            else
            {
                _joystick.DisableJoystick();
            }
        }
    }
}