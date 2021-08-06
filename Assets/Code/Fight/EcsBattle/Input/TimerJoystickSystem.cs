using Code.Extension;
using Leopotam.Ecs;
using UnityEngine;


namespace Code.Fight.EcsBattle.Input
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

                if (input._value.GetJoystickState())
                {
                    // Dbg.Log($"joystick.Value.GetJoystickState:TRUE");
                    var inputVector = new Vector3(
                        input._value.GetHorizontalAxis(),
                        0.0f,
                        input._value.GetVerticalAxis());
                    input._clickTime += Time.deltaTime;
                    input._lastPosition = inputVector;
                }
                else
                {
                    // Dbg.Log($"joystick.Value.GetJoystickState:FALSE");
                    if (!(input._clickTime > 0)) continue;
                    entity.Get<UnpressJoystickComponent>()._pressTime = input._clickTime;
                    entity.Get<UnpressJoystickComponent>()._lastValueVector = input._lastPosition;

                    input._clickTime = 0.0f;
                    input._lastPosition = Vector3.zero;
                }
            }
        }
    }
}