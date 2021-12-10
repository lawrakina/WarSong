using Code.Data.Unit;
using Code.Data.Unit.Enemy;


namespace Code.Unit.Factories{
    public class NpsBattleFactory{
        private readonly EnemiesData _data;

        public NpsBattleFactory(EnemiesData data){
            _data = data;
        }

        public UnitBattle GenerateBattle(EnemySettings settings, UnitCharacteristics characteristics){
            return new UnitBattle(characteristics, settings.Weapon);
        }
    }
}