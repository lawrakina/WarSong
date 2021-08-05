using Leopotam.Ecs;
using UnityEngine;


namespace Code.Fight.EcsBattle.Input
{
    public sealed class GetMovementInInputControlSystem : IEcsRunSystem
    {
        private EcsFilter<InputControlComponent, TargetEntityComponent> _input;

        public void Run()
        {
            foreach (var index in _input)
            {
                ref var input = ref _input.Get1(index);
                ref var target = ref _input.Get2(index);
                
                //имитация движения джойстика. смещение больше погрешности
                if(input._lastPosition.sqrMagnitude > input._maxOffsetForMovement.sqrMagnitude)
                {
                    target._value.Get<MovementEventComponent>()._value = input._lastPosition;
                    // Dbg.Log($"joystick.Movement:{input.LastPosition}");
                }
                else
                {
                    ref var movement = ref target._value.Get<MovementEventComponent>();
                    if (movement._value.sqrMagnitude > input._maxOffsetForMovement.sqrMagnitude)
                    {
                        movement._value = Vector3.zero;
                    }
                }
            }
        }
    }
}