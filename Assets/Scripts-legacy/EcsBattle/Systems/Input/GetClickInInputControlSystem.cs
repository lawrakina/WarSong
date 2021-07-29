using Code.Extension;
using EcsBattle.Components;
using Leopotam.Ecs;


namespace EcsBattle.Systems.Input
{
    public class GetClickInInputControlSystem : IEcsRunSystem
    {
        private EcsFilter<InputControlComponent, TargetEntityComponent, UnpressJoystickComponent> _input;

        public void Run()
        {
            foreach (var index in _input)
            {
                ref var entity = ref _input.GetEntity(index);
                ref var joystick = ref _input.Get1(index);
                ref var target = ref _input.Get2(index);
                ref var lastState = ref _input.Get3(index);

                //create event Click. time hold lastState less than offset и offset less than MaxOffsetForClick
                if (lastState._pressTime <= joystick._maxPressTimeForClickButton &&
                    lastState._lastValueVector.sqrMagnitude <= joystick._maxOffsetForClick.sqrMagnitude)
                {
                    target._value.Get<ClickEventComponent>();
                    entity.Del<UnpressJoystickComponent>();
                    Dbg.Log($"joystick.Click");
                }
            }
        }
    }
}