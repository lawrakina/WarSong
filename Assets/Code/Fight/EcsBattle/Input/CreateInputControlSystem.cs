using Code.Profile.Models;
using Leopotam.Ecs;


namespace Code.Fight.EcsBattle.Input
{
    public class CreateInputControlSystem : IEcsInitSystem
    {
        private EcsWorld _world;
        private InOutControlFightModel _inputModel;
        private EcsFilter<PlayerComponent,UnitComponent> _playerFilter;

        public void Init()
        {
            foreach (var i in _playerFilter)
            {
                ref var playerEntity = ref _playerFilter.GetEntity(i);
                var entity = _world.NewEntity();
                entity.Get<InputControlComponent>()._value = _inputModel.InputControl.Joystick;
                entity.Get<InputControlComponent>()._clickTime = 0.0f;
                entity.Get<InputControlComponent>()._maxPressTimeForClickButton =
                    _inputModel.InputControl.MaxPressTimeForClickButton;
                entity.Get<InputControlComponent>()._maxOffsetForClick = _inputModel.InputControl.MaxOffsetForClick;
                entity.Get<InputControlComponent>()._maxOffsetForMovement = _inputModel.InputControl.MaxOffsetForMovement;
                entity.Get<TargetEntityComponent>()._value = playerEntity;
            }
        }
    }
}