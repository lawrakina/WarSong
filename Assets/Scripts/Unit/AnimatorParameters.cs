using System;
using Extension;
using UnityEngine;


namespace Unit
{
    public sealed class AnimatorParameters
    {
        private readonly Animator _animator;
        private bool _attack;
        private int _attackType;

        private bool _battle;
        private float _horizontalSpeed;
        private bool _jump;
        private float _speed;
        private int _weaponType;


        public AnimatorParameters(Animator animator)
        {
            _animator = animator;
        }

        public bool Battle
        {
            get => _battle;
            set
            {
                // if (_battle && !value)
                    // _animator.SetTrigger(TagManager.ANIMATOR_PARAM_WEAPON_UNSHEATH_TRIGGER);
                // if (!_battle && value)
                    // _animator.SetTrigger(TagManager.ANIMATOR_PARAM_WEAPON_SHEATH_TRIGGER);
                _battle = value;
                _animator.SetBool(TagManager.ANIMATOR_PARAM_BATTLE, _battle);
            }
        }

        public float Speed
        {
            get => _speed;
            set
            {
                // if(Math.Abs(_speed - value) < float.Epsilon) return;
                _speed = value;
                _animator.SetFloat(TagManager.ANIMATOR_PARAM_SPEED, _speed);
            }
        }

        public bool Attack
        {
            get => _attack;
            private set
            {
                _attack = value;
                _animator.SetTrigger(TagManager.ANIMATOR_PARAM_ATTACK_TRIGGER);
                _attack = !value;
            }
        }

        public int WeaponType
        {
            get => _weaponType;
            set
            {
                _weaponType = value;
                _animator.SetFloat(TagManager.ANIMATOR_PARAM_WEAPON_TYPE, _weaponType);
            }
        }

        public int AttackType
        {
            get => _attackType;
            set
            {
                _attackType = value;
                _animator.SetFloat(TagManager.ANIMATOR_PARAM_ATTACK_TYPE, _attackType);
            }
        }

        public float HorizontalSpeed
        {
            get => _horizontalSpeed;
            set
            {
                _horizontalSpeed = value;
                _animator.SetFloat(TagManager.ANIMATOR_PARAM_HORIZONTAL_SPEED, _horizontalSpeed);
            }
        }

        public void SetTriggerAttack()
        {
            Attack = true;
        }

        public void Off()
        {
            _animator.enabled = false;
        }
    }
}