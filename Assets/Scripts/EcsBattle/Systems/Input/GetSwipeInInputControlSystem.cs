using EcsBattle.Components;
using Extension;
using Leopotam.Ecs;


namespace EcsBattle.Systems.Input
{
    public sealed class GetSwipeInInputControlSystem : IEcsRunSystem
    {
        private EcsFilter<InputControlComponent, TargetEntityComponent,UnpressJoystickComponent> _input;
        //.Exclude<UnpressJoystickComponent>
        
        public void Run()
        {
            foreach (var index in _input)
            {
                ref var entity = ref _input.GetEntity(index);
                ref var joystick = ref _input.Get1(index);
                ref var target = ref _input.Get2(index);
                ref var lastState = ref _input.Get3(index);

                // create event swipe. time hold less и offset more than MaxOffsetForClick => SwipeEvent
                if (lastState._pressTime <= joystick._maxPressTimeForClickButton &&
                    lastState._lastValueVector.sqrMagnitude > joystick._maxOffsetForClick.sqrMagnitude)
                {
                    target._value.Get<SwipeEventComponent>();
                    entity.Del<UnpressJoystickComponent>();
                    Dbg.Log($"joystick.Swipe");
                }
            }
        }
    }
}