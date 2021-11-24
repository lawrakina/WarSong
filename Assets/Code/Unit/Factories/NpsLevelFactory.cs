using Code.Data.Unit;
using Code.Data.Unit.Enemy;


namespace Code.Unit.Factories{
    internal class NpsLevelFactory{
        private readonly EnemiesData _data;

        public NpsLevelFactory(EnemiesData data){
            _data = data;
        }

        public UnitLevel GenerateLevel(EnemySettings settings, int baseLevel){
            var level = new UnitLevel(null);
            level.CurrentLevel = baseLevel;
            level.RewardForKilling = _data.DefaultRewardForKillind + settings.RewardForKillingModifier * baseLevel;
            return level;
        }
    }
}