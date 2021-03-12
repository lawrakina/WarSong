using Data;
using Interface;
using UnityEngine;


namespace Battle
{
    public sealed class BattleInputControlsInitialization: IInitialization
    {
        private readonly BattleInputData _data;
        private readonly Transform _rootCanvas;
        private UltimateJoystick _joystick;

        public BattleInputControlsInitialization(BattleInputData data, Transform rootCanvas)
        {
            _data = data;
            _rootCanvas = rootCanvas;
        }

        public void Initialization()
        {
            _joystick.transform.SetParent(_rootCanvas.transform);
        }

        public BattleInputStruct GetData()
        {
            _joystick = Object.Instantiate(_data._joystick);
            
            var data = new BattleInputStruct();
            data._joystick = _joystick;
            data._maxOffsetForClick = _data._maxOffsetForClick;
            data._maxPressTimeForClickButton = _data._maxPressTimeForClickButton;
            data._rootCanvas = _rootCanvas;
            return data;
        }

        public void On()
        {
            
        }

        public void Off()
        {
        }
    }
}