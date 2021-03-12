using UnityEngine;


namespace EcsBattle.Components
{
    public struct InputControlComponent
    {
        public float ClickTime;
        public float MaxPressTimeForClickButton;
        public Vector3 MaxOffsetForClick;
        public UltimateJoystick Value;
        public Vector3 LastPosition;
    }

    public struct UnpressJoystickComponent
    {
        public Vector3 LastValueVector;
        public float PressTime;
    }

    // public struct PressJoystickComponent
    // {
    // }
    
    public struct NeedAttackComponent
    {
    }
}