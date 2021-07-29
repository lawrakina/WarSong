using UnityEngine;


namespace Data
{
    public sealed class BattleInputStruct
    {
        public Vector3 _maxOffsetForMovement { get; set; }
        public Transform _rootCanvas { get; set; }
        public float _maxPressTimeForClickButton { get; set; }
        public Vector3 _maxOffsetForClick { get; set; }
        public UltimateJoystick _joystick { get; set; }
    }
}