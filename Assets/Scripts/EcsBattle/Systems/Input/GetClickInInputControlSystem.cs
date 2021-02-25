using EcsBattle.Components;
using Extension;
using Leopotam.Ecs;


namespace EcsBattle.Systems.Input
{
    public class GetClickInInputControlSystem : IEcsRunSystem
    {
        private EcsFilter<InputControlComponent, UnpressJoystickComponent> _filter;

        public void Run()
        {
            foreach (var index in _filter)
            {
                ref var joystick = ref _filter.Get1(index);
                ref var lastState = ref _filter.Get2(index);
                ref var entity = ref _filter.GetEntity(index);
                
                //имитация нажатия кнопки. Время удержания меньше и смещения нет
                if (lastState.PressTime <= joystick.MaxPressTimeForClickButton &&
                    lastState.LastValueVector.sqrMagnitude <= joystick.MaxOffsetForClick.sqrMagnitude)
                {
                    Dbg.Log($"joystick.Click");
                    entity.Del<UnpressJoystickComponent>();
                }
            }
        }
    }
}