using Controller;
using EcsBattle.Components;
using Leopotam.Ecs;


namespace EcsBattle.Systems.Ui
{
    public sealed class UpdateTargetInUiSystem : IEcsRunSystem
    {
        private BattleTargetModel _targetModel;
        private EcsFilter<PlayerComponent> _filter;

        public void Run()
        {
            foreach (var i in _filter)
            {
                ref var entity = ref _filter.GetEntity(i);

                if (entity.Has<CurrentTargetComponent>())
                {
                    ref var target = ref entity.Get<CurrentTargetComponent>();
                    _targetModel.ChangeTarget(target._baseUnitView);
                    _targetModel.ChangeMaxHp((int) target._baseUnitView.MaxHp);
                    _targetModel.ChangeCurrentHp((int) target._baseUnitView.CurrentHp);
                }
                else
                {
                    _targetModel.ChangeTarget(null);
                }
            }
        }
    }
}