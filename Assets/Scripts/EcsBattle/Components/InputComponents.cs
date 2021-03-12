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
        public Vector3 MaxOffsetForMovement;
    }

    public struct UnpressJoystickComponent
    {
        public Vector3 LastValueVector;
        public float PressTime;
    }

    
    public struct ClickEventComponent { }
    public struct SwipeEventComponent { }
    public struct MovementEventComponent
    {
        public Vector3 value;
    }
    
    // public struct PressJoystickComponent
    // {
    // }
    
    public struct NeedAttackComponent { }
}