using Code.Profile.Models;
using Leopotam.Ecs;


namespace Code.Fight.EcsBattle.Out.Gui
{
    public sealed class UpdateTargetInUiSystem : IEcsRunSystem
    {
        private InOutControlFightModel _model;
        private EcsFilter<PlayerComponent> _filter;

        public void Run()
        {
            foreach (var i in _filter)
            {
                ref var entity = ref _filter.GetEntity(i);

                if (entity.Has<CurrentTargetComponent>())
                {
                    ref var target = ref entity.Get<CurrentTargetComponent>();
                    _model.PlayerTarget.ChangeTarget(target._baseUnitView);
                    _model.PlayerTarget.ChangeMaxHp((int) target._baseUnitView.UnitHealth.MaxHp);
                    _model.PlayerTarget.ChangeCurrentHp((int) target._baseUnitView.UnitHealth.CurrentHp);
                }
                else
                {
                    _model.PlayerTarget.ChangeTarget(null);
                }
            }
        }
    }
}