using Code.Fight.EcsFight.Settings;
using Leopotam.Ecs;


namespace Code.Fight.EcsFight.Output{
    public class DisplayEffectsS : IEcsRunSystem{
        private EcsFilter<UnitC, UiEnemyHealthBarC> _updateHealthBar;
        public void Run(){
            foreach (var i in _updateHealthBar){
                ref var enemyEntity = ref _updateHealthBar.GetEntity(i);
                var maxHp = enemyEntity.Get<UnitC>().Health.MaxHp;
                var currentHp = enemyEntity.Get<UnitC>().Health.CurrentHp;
                var enemyHealthBar = enemyEntity.Get<UiEnemyHealthBarC>().value;
                enemyHealthBar.ChangeValue(currentHp, maxHp);
            }
        }
    }
}