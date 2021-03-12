using Data;
using EcsBattle.Components;
using Leopotam.Ecs;


namespace EcsBattle.Systems.Input
{
    public class CreateInputControlSystem : IEcsInitSystem
    {
        private EcsWorld _world;
        private BattleInputStruct _inputStruct;

        public void Init()
        {
            _inputStruct._joystick.transform.SetParent(_inputStruct._rootCanvas);
            
            var entity = _world.NewEntity();
            entity.Get<InputControlComponent>().Value = _inputStruct._joystick;
            entity.Get<InputControlComponent>().ClickTime = 0.0f;
            entity.Get<InputControlComponent>().MaxPressTimeForClickButton = _inputStruct._maxPressTimeForClickButton;
            entity.Get<InputControlComponent>().MaxOffsetForClick = _inputStruct._maxOffsetForClick;
        }
    }
}