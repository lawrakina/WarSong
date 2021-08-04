using System;
using UnityEngine;


namespace Code.Profile.Models
{
    [Serializable]
    public class InputControlModel
    {
        private float _maxPressTimeForClickButton;
        public UltimateJoystick Joystick;
        public float MaxPressTimeForClickButton => _maxPressTimeForClickButton;
        public Vector3 MaxOffsetForClick { get; set; }
        public Vector3 MaxOffsetForMovement { get; set; }
    }
}