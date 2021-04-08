using EcsBattle.Components;
using Leopotam.Ecs;


namespace EcsBattle.Systems.Ui
{
    public class UpdateEnemiesCurrentHealthPointsSystem : IEcsRunSystem
    {
        private EcsFilter<UiEnemyHealthBarComponent, UnitHpComponent> _filter;

        public void Run()
        {
            foreach (var index in _filter)
            {
                _filter.Get1(index)._value.ChangeValue(_filter.Get2(index)._currentValue, _filter.Get2(index)._maxValue);
            }
        }
    }
}