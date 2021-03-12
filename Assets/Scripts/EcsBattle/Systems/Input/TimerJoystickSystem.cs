using EcsBattle.Components;
using Extension;
using Leopotam.Ecs;
using UnityEngine;


namespace EcsBattle.Systems.Input
{
    public class TimerJoystickSystem : IEcsRunSystem
    {
        private EcsFilter<InputControlComponent> _filter;

        public void Run()
        {
            foreach (var index in _filter)
            {
                ref var input = ref _filter.Get1(index);
                ref var entity = ref _filter.GetEntity(index);

                if (input.Value.GetJoystickState())
                {
                    // Dbg.Log($"joystick.Value.GetJoystickState:TRUE");
                    var inputVector = new Vector3(
                        input.Value.GetHorizontalAxis(),
                        0.0f,
                        input.Value.GetVerticalAxis());
                    input.ClickTime += Time.deltaTime;
                    input.LastPosition = inputVector;
                }
                else
                {
                    // Dbg.Log($"joystick.Value.GetJoystickState:FALSE");
                    if (!(input.ClickTime > 0)) continue;
                    entity.Get<UnpressJoystickComponent>().PressTime = input.ClickTime;
                    entity.Get<UnpressJoystickComponent>().LastValueVector = input.LastPosition;

                    input.ClickTime = 0.0f;
                    input.LastPosition = Vector3.zero;
                }
            }
        }
    }
}