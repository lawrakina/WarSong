using Code.Data.Unit;
using Code.Data.Unit.Enemy;


namespace Code.Unit.Factories{
    public class NpsHealthFactory{
        private readonly EnemiesData _data;

        public NpsHealthFactory(EnemiesData data){
            _data = data;
        }

        public UnitHealth GenerateHealth(EnemySettings settings, int baseLevel){
            var health = new UnitHealth();
            health.MaxHp = _data._baseHp + _data._hpForLevel * baseLevel + settings.AdditionalHp;
            health.CurrentHp = health.MaxHp;
            return health;
        }
    }
}