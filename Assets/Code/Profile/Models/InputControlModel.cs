using System;
using System.Collections.Generic;
using Code.UI.Fight;
using UnityEngine;


namespace Code.Profile.Models
{
    [Serializable]
    public class InputControlModel
    {
        private float _maxPressTimeForClickButton;
        public UltimateJoystick Joystick;
        public float MaxPressTimeForClickButton
        {
            get => _maxPressTimeForClickButton;
            set => _maxPressTimeForClickButton = value;
        }

        public Vector3 MaxOffsetForClick { get; set; }
        public Vector3 MaxOffsetForMovement { get; set; }
        public Queue<Ability> QueueOfAbilities = new Queue<Ability>();
    }
}