using Enums;
using Extension;
using InputMovement;
using Interface;
using UniRx;
using Unit;
using UnityEngine;


namespace Controller
{
    public sealed class MovePlayerController : BaseController, IExecute, IFixedExecute, ICleanup
    {
        #region Fields

        private readonly IBaseUnitView _unitView;

        private Vector3 _inputVector;
        private readonly IUserInputProxy _horizontalInputProxy;
        private readonly IUserInputProxy _verticalInputProxy;
        private Vector3 _direction;
        private bool _isGoTarget;
        private GameObject _goTarget;
        private float _gravityForce;
        private float _deltaImpulce;
        private readonly IReactiveProperty<EnumFightCamera> _typeCameraAndCharControl;
        private readonly IReactiveProperty<EnumBattleWindow> _battleState;

        private delegate void ActionMove(float deltaTime);

        private ActionMove _move;

        #endregion


        #region Properties

        private bool IsGrounded =>
            Physics.Raycast(_unitView.Transform.position + Vector3.up / 2, Vector3.down, out _,
                1.0f, LayerManager.GroundLayer);

        public GameObject GoTarget
        {
            get
            {
                if (!_isGoTarget)
                {
                    _goTarget = Object.Instantiate(new GameObject(), _unitView.Transform, true);
                    _goTarget.name = "->DirectionMoving<-";
                    // Debug.Log($"CreateTargetMovingDirection: {_goTarget}");
                    _isGoTarget = true;
                }

                return _goTarget;
            }
        }

        #endregion


        #region Methods

        private void VerticalOnAxisOnChange(float value)
        {
            _inputVector.z = value;
        }

        private void HorizontalOnAxisOnChange(float value)
        {
            _inputVector.x = value;
        }

        public override void Cleanup()
        {
            base.Cleanup();

            _horizontalInputProxy.AxisOnChange -= HorizontalOnAxisOnChange;
            _verticalInputProxy.AxisOnChange -= VerticalOnAxisOnChange;
        }

        #endregion
        
        #region ctor

        public MovePlayerController(IReactiveProperty<EnumBattleWindow> battleState,
            (IUserInputProxy inputHorizontal, IUserInputProxy inputVertical) input,
            IBaseUnitView unitView, IReactiveProperty<EnumFightCamera> typeCameraAndCharControl)
        {
            _battleState = battleState;
            _typeCameraAndCharControl = typeCameraAndCharControl;
            _unitView = unitView;
            _horizontalInputProxy = input.inputHorizontal;
            _verticalInputProxy = input.inputVertical;
            _horizontalInputProxy.AxisOnChange += HorizontalOnAxisOnChange;
            _verticalInputProxy.AxisOnChange += VerticalOnAxisOnChange;

            _battleState.Subscribe(_ =>
            {
                if (_battleState.Value == EnumBattleWindow.Fight)
                    _isEnable = true;
                else
                    _isEnable = false;
            });

            _typeCameraAndCharControl.Subscribe(_ =>
            {
                if (_typeCameraAndCharControl.Value == EnumFightCamera.TopView) _move = TopViewMove;

                if (_typeCameraAndCharControl.Value == EnumFightCamera.ThirdPersonView) _move = ThirdPersonViewMove;
            });
        }

        #endregion


        public void Execute(float deltaTime)
        {
            if (!_isEnable) return;

            // CheckGravity();
        }

        public void FixedExecute(float deltaTime)
        {
            if (!_isEnable) return;

            //правильное скалярное умножение векторов => скорость под углом 45` ~ 0.706
            _direction = Vector3.ClampMagnitude(_inputVector, 1f);

            _move(deltaTime);
        }

        public override void On()
        {
            base.On();
        }

        public override void Off()
        {
            base.Off();
        }

        private void TopViewMove(float deltaTime)
        {
            var movingVector = new Vector3(_direction.x, 0f, _direction.z);
            if (Vector3.Angle(Vector3.forward, _inputVector) > 1f || Vector3.Angle(Vector3.forward, _inputVector) == 0)
            {
                var directToRotate =
                    Vector3.RotateTowards(
                        _unitView.Transform.forward,
                        _inputVector,
                        _unitView.CharAttributes.Speed,
                        0.0f);
                _unitView.Transform.rotation =
                    Quaternion.LookRotation(new Vector3(directToRotate.x, 0f, directToRotate.z));
            }

            _unitView.Rigidbody.MovePosition(_unitView.Transform.position +
                                             movingVector * (_unitView.CharAttributes.Speed * deltaTime));
            AnimatorSpeedUpdate(movingVector);
        }

        private void AnimatorSpeedUpdate(Vector3 movingVector)
        {
            _unitView.AnimatorParameters.Speed = movingVector.z;
            // if (movingVector.z < 0)
            //     _unitView.AnimatorParameters.Speed = -1.0f;
            _unitView.AnimatorParameters.HorizontalSpeed = movingVector.x;
        }

        private void ThirdPersonViewMove(float deltaTime)
        {
            //MovingPlayer
            GoTarget.transform.localPosition = _direction;
            
            //остановился тут
            var target = _unitView.Transform.position - GoTarget.transform.position;
            _unitView.Rigidbody.MovePosition(
                _unitView.Transform.position -
                target * (_unitView.CharAttributes.Speed * deltaTime)
            );
            AnimatorSpeedUpdate(_direction);

            //RotatePlayer
            if (_unitView.EnemyTarget == null)
            {
                _unitView.Transform.RotateAround(
                    _unitView.Transform.position,
                    Vector3.up,
                    _unitView.CharAttributes.RotateSpeedPlayer * deltaTime * _direction.x);
            }
            else
            {
                var sqrDistance = (_unitView.Transform.position - _unitView.EnemyTarget.position).sqrMagnitude;
                if (_unitView.CharAttributes.AgroDistance * _unitView.CharAttributes.AgroDistance > sqrDistance)
                {
                    Debug.Log($"Противник рядом: {sqrDistance}");
                    var newDir = Vector3.RotateTowards(
                        _unitView.Transform.forward,
                        _unitView.EnemyTarget.transform.position - _unitView.Transform.position,
                        10f * Time.deltaTime, _unitView.CharAttributes.AgroDistance);
                    newDir.y = 0;
                    _unitView.Transform.rotation = Quaternion.LookRotation(newDir, Vector3.up);
                }
            }
        }

        private void CheckGravity()
        {
            if (IsGrounded)
                _gravityForce = -1.0f;
            else
                _gravityForce -= 2.0f;

            _direction.y = _gravityForce;
        }


        
    }
}