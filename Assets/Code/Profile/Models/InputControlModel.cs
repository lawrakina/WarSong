using System;
using System.Collections.Generic;
using Code.UI.Fight;
using UniRx;
using UnityEngine;


namespace Code.Profile.Models{
    [Serializable] public class InputControlModel{
        private Vector3 _maxOffsetForMovement;
        private Vector3 _maxOffsetForClick;
        private UltimateJoystick _joystick;
        private float _maxPressTimeForClickButton;
        private Queue<Ability> _queueOfAbilities = new Queue<Ability>();
        public UltimateJoystick Joystick{
            get => _joystick;
            set => _joystick = value;
        }
        public float MaxPressTimeForClickButton{
            get => _maxPressTimeForClickButton;
            set => _maxPressTimeForClickButton = value;
        }
        public Vector3 MaxOffsetForClick{
            get => _maxOffsetForClick;
            set => _maxOffsetForClick = value;
        }
        public Vector3 MaxOffsetForMovement{
            get => _maxOffsetForMovement;
            set => _maxOffsetForMovement = value;
        }
        public Queue<Ability> QueueOfAbilities{
            get => _queueOfAbilities;
            set => _queueOfAbilities = value;
        }
    }
}