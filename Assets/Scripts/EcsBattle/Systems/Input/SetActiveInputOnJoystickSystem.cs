using EcsBattle.Components;
using Leopotam.Ecs;
using UnityEngine;


namespace EcsBattle.Systems.Input
{
    public class SetActiveInputOnJoystickSystem : IEcsRunSystem
    {
        private EcsFilter<InputControlComponent> _filter;

        public void Run()
        {
            foreach (var index in _filter)
            {
                ref var joystick = ref _filter.Get1(index);
                ref var entity = ref _filter.GetEntity(index);

                if (joystick.Value.GetJoystickState())
                {
                    var inputVector = new Vector3(
                        joystick.Value.GetHorizontalAxis(),
                        0.0f,
                        joystick.Value.GetVerticalAxis());
                    // entity.Get<PressJoystickComponent>();
                    joystick.ClickTime += Time.deltaTime;
                    joystick.LastPosition = inputVector;
                }
                else
                {
                    // entity.Del<PressJoystickComponent>();
                    if (!(joystick.ClickTime > 0)) continue;
                    entity.Get<UnpressJoystickComponent>().PressTime = joystick.ClickTime;
                    entity.Get<UnpressJoystickComponent>().LastValueVector = joystick.LastPosition;

                    joystick.ClickTime = 0.0f;
                    joystick.LastPosition = Vector3.zero;
                }
            }
        }
    }
}