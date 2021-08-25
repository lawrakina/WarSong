using Leopotam.Ecs;

namespace Code.Fight.EcsBattle.Unit.EnemyHealthBars
{
    public class RotateUiHeathBarsToCameraSystem : IEcsRunSystem
    {
        private EcsFilter<UiEnemyHealthBarComponent> _filter;
    
        public void Run()
        {
            foreach (var i in _filter)
            {
                ref var enemyEntity = ref _filter.GetEntity(i);
                enemyEntity.Get<UiEnemyHealthBarComponent>()._value.AlignCamera();
            }
        
        }
    }
}
