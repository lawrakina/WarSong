using System;
using Enums;
using Manager;
using UnityEngine;
using UnityEngine.AI;


namespace VIew
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(CapsuleCollider))]
    [RequireComponent(typeof(NavMeshAgent))]
    public abstract class BaseUnitView : MonoBehaviour
    {
        public NavMeshAgent Agent;
        public Animator Animator;
        public AnimatorParammeters AnimatorParams;
        public Collider Collider;
        public HealthBarView HealthBar;
        public bool isEnable = true;
        public Rigidbody Rigidbody;
        public Transform Transform;


        #region Properties

        public float Speed
        {
            get => _speed;
            set
            {
                _speed = value;
                AnimatorParams.Speed = value;
            }
        }

        #endregion


        protected virtual void Awake()
        {
            Transform = GetComponent<Transform>();
            Rigidbody = GetComponent<Rigidbody>();
            Collider = GetComponent<Collider>();
            Agent = GetComponent<NavMeshAgent>();
            Animator = GetComponent<Animator>();
            AnimatorParams = new AnimatorParammeters(Animator);
            HealthBar = GetComponentInChildren<HealthBarView>();
        }

        public void ToSwim()
        {
            OnSwimEvent?.Invoke();
        }

        public void ToUnSwim()
        {
            OnUnSwimEvent?.Invoke();
        }


        #region PrivateClass

        public class AnimatorParammeters
        {
            private readonly Animator _animator;
            private bool _attack;
            private int _attackType;

            private bool _battle;
            private bool _falling;
            private bool _jump;
            private float _speed;
            private UnitState _unitState;
            private int _weaponType;


            public AnimatorParammeters(Animator animator)
            {
                _animator = animator;
            }

            public bool Battle
            {
                get => _battle;
                set
                {
                    if (_battle && !value)
                        _animator.SetTrigger(TagManager.ANIMATOR_PARAM_WEAPON_UNSHEATH_TRIGGER);
                    if (!_battle && value)
                        _animator.SetTrigger(TagManager.ANIMATOR_PARAM_WEAPON_SHEATH_TRIGGER);
                    _battle = value;
                    _animator.SetBool(TagManager.ANIMATOR_PARAM_BATTLE, value);
                }
            }

            public float Speed
            {
                get => _speed;
                set
                {
                    _speed = value;
                    _animator.SetFloat(TagManager.ANIMATOR_PARAM_SPEED, value);
                }
            }

            public bool Falling
            {
                get => _falling;
                set
                {
                    _falling = value;
                    _animator.SetBool(TagManager.ANIMATOR_PARAM_FALLING, value);
                }
            }

            public bool Jump
            {
                get => _jump;
                private set
                {
                    _jump = value;
                    _animator.SetTrigger(TagManager.ANIMATOR_PARAM_JUMP_TRIGGER);
                    _jump = !value;
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
                    _animator.SetInteger(TagManager.ANIMATOR_PARAM_WEAPON_TYPE, value);
                }
            }

            public int AttackType
            {
                get => _attackType;
                set
                {
                    _attackType = value;
                    _animator.SetInteger(TagManager.ANIMATOR_PARAM_ATTACK_TYPE, value);
                }
            }

            public UnitState UnitState
            {
                get => _unitState;
                set
                {
                    if (_unitState == value) return;
                    _unitState = value;
                    _animator.SetFloat(TagManager.ANIMATOR_PARAM_STATE_UNIT, (float) value);
                    // switch (_unitState)
                    // {
                    //     case UnitState.None:
                    //         //todo придумать зачем этот вариант нужен
                    //         break;
                    //     case UnitState.Dead:
                    //         //todo реализовать анимацию смерти
                    //         break;
                    //     case UnitState.Normal:
                    //         _animator.SetFloat(TagManager.ANIMATOR_PARAM_SWIM, false);
                    //         // _animator.SetBool(TagManager.ANIMATOR_PARAM_FLY, false);
                    //         // _animator.SetBool(TagManager.ANIMATOR_PARAM_STUNNED, false);
                    //         break;
                    //     case UnitState.Swim:
                    //         _animator.SetBool(TagManager.ANIMATOR_PARAM_SWIM, true);
                    //         _animator.SetBool(TagManager.ANIMATOR_PARAM_FLY, false);
                    //         _animator.SetBool(TagManager.ANIMATOR_PARAM_STUNNED, false);
                    //         break;
                    //     case UnitState.Fly:
                    //         _animator.SetBool(TagManager.ANIMATOR_PARAM_SWIM, false);
                    //         _animator.SetBool(TagManager.ANIMATOR_PARAM_FLY, true);
                    //         _animator.SetBool(TagManager.ANIMATOR_PARAM_STUNNED, false);
                    //         break;
                    //     case UnitState.Stunned:
                    //         _animator.SetBool(TagManager.ANIMATOR_PARAM_SWIM, false);
                    //         _animator.SetBool(TagManager.ANIMATOR_PARAM_FLY, false);
                    //         _animator.SetBool(TagManager.ANIMATOR_PARAM_STUNNED, true);
                    //         break;
                    // }
                }
            }

            public void SetTriggerJump()
            {
                Jump = true;
            }

            public void SetTriggerAttack()
            {
                Attack = true;
            }

            public void ResetTrigger(string name)
            {
                _animator.ResetTrigger(name);
            }
        }

        #endregion


        #region fields

        private float _speed;

        [Header("Movement")]


        #region movenment

        public float _moveSpeed = 5.0f;

        public float distanceToCheckGround = 0.51f;
        public float accelerationOfGravity = 1f;

        #endregion

        #endregion


        #region Events

        public event Action OnSwimEvent;
        public event Action OnUnSwimEvent;

        #endregion
    }
}