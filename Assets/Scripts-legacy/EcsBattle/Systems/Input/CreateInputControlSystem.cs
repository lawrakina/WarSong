using Controller;
using Data;
using EcsBattle.Components;
using Leopotam.Ecs;


namespace EcsBattle.Systems.Input
{
    public class CreateInputControlSystem : IEcsInitSystem
    {
        private EcsWorld _world;
        private BattleInputStruct _inputStruct;
        // private AbilitiesFightModel _abilitiesFightModel;
        private EcsFilter<PlayerComponent> _playerFilter;

        public void Init()
        {
            foreach (var i in _playerFilter)
            {
                ref var playerEntity = ref _playerFilter.GetEntity(i);
                var entity = _world.NewEntity();
                entity.Get<InputControlComponent>()._value = _inputStruct._joystick;
                entity.Get<InputControlComponent>()._clickTime = 0.0f;
                entity.Get<InputControlComponent>()._maxPressTimeForClickButton =
                    _inputStruct._maxPressTimeForClickButton;
                entity.Get<InputControlComponent>()._maxOffsetForClick = _inputStruct._maxOffsetForClick;
                entity.Get<InputControlComponent>()._maxOffsetForMovement = _inputStruct._maxOffsetForMovement;
                entity.Get<TargetEntityComponent>()._value = playerEntity;

                //for Button.OnClick event 
                // var abilitiesEntity = new AbilitiesEntity(_abilitiesFightModel, entity);
            }
        }
    }

    // public class AbilitiesEntity
    // {
    //     #region Fields
    //
    //     private readonly AbilitiesFightModel _model;
    //     private readonly EcsEntity _entity;
    //
    //     #endregion
    //
    //
    //     #region ClassLiveCycles
    //
    //     public AbilitiesEntity(AbilitiesFightModel model, EcsEntity entity)
    //     {
    //         _model = model;
    //         _entity = entity;
    //
    //         _model._spell1 += StartSpell;
    //         _model._spell2 += StartSpell;
    //         _model._spell3 += StartSpell;
    //     }
    //
    //     ~AbilitiesEntity()
    //     {
    //         _model._spell1 -= StartSpell;
    //         _model._spell2 -= StartSpell;
    //         _model._spell3 -= StartSpell;
    //     }
    //
    //     #endregion
    //
    //
    //     #region Methods
    //
    //     private void StartSpell(AbilityItem ability)
    //     {
    //         _entity.Get<StartSpellComponent>()._value = ability;
    //     }
    //
    //     #endregion
    // }
}