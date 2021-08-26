using Leopotam.Ecs;

namespace Code.Fight.EcsBattle.Unit.EnemyHealthBars
{
    public class ShowHealthBarForEnemiesInSightSystem : IEcsRunSystem
    {
        private EcsFilter<EnemyComponent, UiEnemyHealthBarComponent> _filter;
        
        //TODO: Изменение состояния врага, по типу: виден, скрыт, убит и т.д.
        
        public void Run()
        {
            foreach (var i in _filter)
            {
                ref var enemyEntity = ref _filter.GetEntity(i);
                ref var healthBar = ref _filter.Get2(i);
                
                healthBar._value.SetOnOff(enemyEntity.Has<EnemyInSight>());
            }
        }
    }
}
