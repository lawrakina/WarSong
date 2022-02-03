using Code.Extension;
using Code.Fight.EcsFight.Battle;
using Code.Fight.EcsFight.Settings;
using Code.Fight.EcsFight.Timer;
using Code.GameCamera;
using Code.Profile.Models;
using Code.Unit;
using Leopotam.Ecs;
using UnityEngine;


namespace Code.Fight.EcsFight.Input{
    public class InputControlS : IEcsInitSystem, IEcsRunSystem{
        private EcsWorld _world = null;
        private InOutControlFightModel _model;
        private BattleCamera _camera;
        private EcsFilter<PlayerTag, UnitC> _playerFilter;
        private EcsFilter<InputControlC, ControlOfThisTargetC> _inputFilter;
        private EcsFilter<InputControlC, ControlOfThisTargetC, UnpressJoystickC> _unpressJoystick;

        public void Init(){
            foreach (var i in _playerFilter){
                var entity = _world.NewEntity();
                ref var player = ref _playerFilter.GetEntity(i);
                ref var input = ref entity.Get<InputControlC>();
                input.joystick = _model.InputControl.Joystick;
                input.Time = 0.0f;
                input.TimeToClick = _model.InputControl.MaxPressTimeForClickButton;
                input.MaxOffsetForClick = _model.InputControl.MaxOffsetForClick;
                input.MaxOffsetForMovement = _model.InputControl.MaxOffsetForMovement;
                entity.Get<ControlOfThisTargetC>().Value = player;
            }
        }

        public void Run(){
            foreach (var i in _inputFilter){
                ref var entity = ref _inputFilter.GetEntity(i);
                ref var input = ref _inputFilter.Get1(i);
                ref var target = ref _unpressJoystick.Get2(i);

                //Get input events
                if (input.joystick.GetJoystickState()){
                    var inputVector = new Vector3(
                        input.joystick.GetHorizontalAxis(),
                        0.0f,
                        input.joystick.GetVerticalAxis());
                    input.Time += Time.deltaTime;
                    input.LastPosition = inputVector;
                } else{
                    if (!(input.Time > 0)) continue;
                    entity.Get<UnpressJoystickC>().PressTime = input.Time;
                    entity.Get<UnpressJoystickC>().LastValueVector = input.LastPosition;

                    input.Time = 0.0f;
                    input.LastPosition = Vector3.zero;
                }

                ref var moveEvent = ref target.Value.Get<ManualMoveEventC>();
                // create event movement
                if (input.joystick.GetJoystickState()){
                    moveEvent.Vector = input.LastPosition;
                    moveEvent.CameraRotation = _camera.Transform.rotation;
                    target.Value.Del<NeedAttackTargetCommand>();
                    target.Value.Del<Timer<BattleTag>>();
                } else if(!input.joystick.GetJoystickState() ){
                    moveEvent.Vector = Vector3.zero;
                }
            }

            foreach (var i in _unpressJoystick){
                ref var entity = ref _unpressJoystick.GetEntity(i);
                ref var joystick = ref _unpressJoystick.Get1(i);
                ref var target = ref _unpressJoystick.Get2(i);
                ref var lastState = ref _unpressJoystick.Get3(i);

                //create event Click. time hold lastState less than offset и offset less than MaxOffsetForClick
                if (lastState.PressTime < joystick.TimeToClick &&
                    lastState.LastValueVector.sqrMagnitude <= joystick.MaxOffsetForClick.sqrMagnitude){
                    target.Value.Get<ClickEventC>();
                    entity.Del<UnpressJoystickC>();
                    Dbg.Log($"joystick.Click");
                }

                // create event swipe. time hold less и offset more than MaxOffsetForClick => SwipeEvent
                if (lastState.PressTime < joystick.TimeToClick &&
                    lastState.LastValueVector.sqrMagnitude > joystick.MaxOffsetForClick.sqrMagnitude){
                    target.Value.Get<SwipeEventC>().Value = lastState.LastValueVector;
                    entity.Del<UnpressJoystickC>();
                    Dbg.Log($"joystick.Swipe");
                }
            }
        }
    }

   
}