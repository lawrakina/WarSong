using EcsBattle.Components;
using Extension;
using Leopotam.Ecs;
using UnityEngine;


namespace EcsBattle.Systems.Input
{
    public class CreateInputControlSystem : IEcsInitSystem
    {
        private EcsWorld _world;

        public void Init()
        {
            var entity = _world.NewEntity();
            entity.Get<InputControlComponent>().Value = UltimateJoystick.GetUltimateJoystick(StringManager.ULTIMATE_JOYSTICK_MOVENMENT);
            entity.Get<InputControlComponent>().ClickTime = 0.0f;
            entity.Get<InputControlComponent>().MaxPressTimeForClickButton = 0.5f;
            entity.Get<InputControlComponent>().MaxOffsetForClick = new Vector3(0.2f, 0.2f, 0.2f);
        }
    }
}