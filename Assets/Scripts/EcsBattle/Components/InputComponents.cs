using UnityEngine;


namespace EcsBattle.Components
{
    public struct InputControlComponent
    {
        public float _clickTime;
        public float _maxPressTimeForClickButton;
        public Vector3 _maxOffsetForClick;
        public UltimateJoystick _value;
        public Vector3 _lastPosition;
        public Vector3 _maxOffsetForMovement;
    }

    public struct UnpressJoystickComponent
    {
        public Vector3 _lastValueVector;
        public float _pressTime;
    }

    
    public struct ClickEventComponent { }
    public struct SwipeEventComponent { }
    public struct MovementEventComponent
    {
        public Vector3 _value;
    }
    
    // public struct PressJoystickComponent
    // {
    // }
    
    public struct StartAttackComponent { }
    public struct StartJumpComponent { }
}