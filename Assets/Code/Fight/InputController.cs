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
            
            var inputModel = _profilePlayer.Models.InOutControlFightModel.InputControl;
            var inputSettings = _profilePlayer.Settings.FightInputData;

            _joystick = Object.Instantiate(inputSettings.Joystick,_placeForUi);
            AddGameObjects(_joystick.gameObject);
            
            _profilePlayer.Models.FightModel.FightState.Subscribe(ShowJoystick).AddTo(_subscriptions);

            inputModel.Joystick = _joystick;
            inputModel.MaxOffsetForClick = inputSettings.MaxOffsetForClick;
            inputModel.MaxOffsetForMovement = inputSettings.MaxOffsetForMovement;
            inputModel.MaxPressTimeForClickButton = inputSettings.MaxPressTimeForClickButton;
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