using EcsBattle.Components;
using Extension;
using Leopotam.Ecs;
using UnityEngine;


namespace EcsBattle
{
    public class Movement1SetDirectionForPlayerSystem : IEcsRunSystem
    {
        private EcsFilter<PlayerComponent> _filter;
        
        public void Run()
        {
            foreach (var index in _filter)
            {
                var inputVector = new Vector3(
                    UltimateJoystick.GetHorizontalAxis(StringManager.ULTIMATE_JOYSTICK_MOVENMENT),
                    0.0f,
                    UltimateJoystick.GetVerticalAxis(StringManager.ULTIMATE_JOYSTICK_MOVENMENT));
                _filter.GetEntity(index).Get<DirectionMoving>().Value =
                    Vector3.ClampMagnitude(inputVector, 1f);
            }
        }
    }
}