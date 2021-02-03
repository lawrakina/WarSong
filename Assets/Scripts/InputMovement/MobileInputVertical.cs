using System;
using Extension;


namespace InputMovement
{
    public sealed class MobileInputVertical : IUserInputProxy
    {
        public event Action<float> AxisOnChange = delegate { };

        public void GetAxis()
        {
            // AxisOnChange.Invoke(Input.GetAxis(StringManager.AXIS_VERTICAL));
            AxisOnChange.Invoke(UltimateJoystick.GetVerticalAxis(StringManager.ULTIMATE_JOYSTICK_MOVENMENT));
            // Debug.Log($"MobileInputVertic:{UltimateJoystick.GetVerticalAxis(StringManager.ULTIMATE_JOYSTICK_MOVENMENT)}");
        }
    }
}