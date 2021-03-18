using EcsBattle.Components;
using Extension;
using Leopotam.Ecs;
using UnityEngine;


namespace EcsBattle.Systems.Input
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
                if(input.LastPosition.sqrMagnitude > input.MaxOffsetForMovement.sqrMagnitude)
                {
                    target.value.Get<MovementEventComponent>().value = input.LastPosition;
                    // Dbg.Log($"joystick.Movement:{input.LastPosition}");
                }
                else
                {
                    ref var movement = ref target.value.Get<MovementEventComponent>();
                    if (movement.value.sqrMagnitude > input.MaxOffsetForMovement.sqrMagnitude)
                    {
                        movement.value = Vector3.zero;
                    }
                }
            }
        }
    }

}