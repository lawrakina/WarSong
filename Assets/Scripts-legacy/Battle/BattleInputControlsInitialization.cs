using Data;
using Interface;
using UnityEngine;


namespace Battle
{
    public sealed class BattleInputControlsInitialization: IInitialization
    {
        private readonly BattleInputData _data;
        private readonly Transform _rootCanvas;

        public BattleInputControlsInitialization(BattleInputData data, Transform rootCanvas)
        {
            _data = data;
            _rootCanvas = rootCanvas;
        }

        public void Init()
        {
            
        }

        public BattleInputStruct GetData()
        {
            var data = new BattleInputStruct();
            data._joystick = Object.Instantiate(_data._joystick);
            data._maxOffsetForMovement = _data._maxOffsetForMovement;
            data._maxOffsetForClick = _data._maxOffsetForClick;
            data._maxPressTimeForClickButton = _data._maxPressTimeForClickButton;
            data._rootCanvas = _rootCanvas;
            data._joystick.transform.SetParent(_rootCanvas.transform);
            return data;
        }
    }
}