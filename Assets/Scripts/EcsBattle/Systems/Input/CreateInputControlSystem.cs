using Data;
using EcsBattle.Components;
using Leopotam.Ecs;


namespace EcsBattle.Systems.Input
{
    public class CreateInputControlSystem : IEcsInitSystem
    {
        private EcsWorld _world;
        private BattleInputStruct _inputStruct;
        private EcsFilter<PlayerComponent> _playerFilter;

        public void Init()
        {
            foreach (var i in _playerFilter)
            {
                ref var playerEntity = ref _playerFilter.GetEntity(i);
                var entity = _world.NewEntity();
                entity.Get<InputControlComponent>().Value = _inputStruct._joystick;
                entity.Get<InputControlComponent>().ClickTime = 0.0f;
                entity.Get<InputControlComponent>().MaxPressTimeForClickButton = _inputStruct._maxPressTimeForClickButton;
                entity.Get<InputControlComponent>().MaxOffsetForClick = _inputStruct._maxOffsetForClick;
                entity.Get<InputControlComponent>().MaxOffsetForMovement = _inputStruct._maxOffsetForMovement;
                entity.Get<TargetEntityComponent>().value = playerEntity;
            }
        }
    }
}