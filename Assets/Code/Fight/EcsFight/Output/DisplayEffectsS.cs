using Code.Fight.EcsFight.Settings;
using Code.Profile.Models;
using Leopotam.Ecs;


namespace Code.Fight.EcsFight.Output{
    public class DisplayEffectsS : IEcsRunSystem{
        private InOutControlFightModel _model;
        private EcsFilter<PlayerTag, UnitC, ChangeHpC> _player;
        private EcsFilter<UnitC, UiEnemyHealthBarC> _updateHealthBar;
        public void Run(){
            foreach (var i in _player){
                ref var entity = ref _player.GetEntity(i);
                ref var unit = ref _player.Get2(i);

                _model.PlayerStats.CurrentHp = (int) unit.Health.CurrentHp;
                // _model.PlayerStats.MaxHp = (int) unit.Health.MaxHp;
                entity.Del<ChangeHpC>();
            }
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