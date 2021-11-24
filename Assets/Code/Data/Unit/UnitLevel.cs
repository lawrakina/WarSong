using Code.Data.Unit.Player;


namespace Code.Data.Unit{
    public class UnitLevel{
        private readonly LevelValue[] _levels;
        private int _currentLevel;
        private int _currentExperiencePoints;
        private float _rewardForKilling;

        public int CurrentExperiencePoints{
            get => _currentExperiencePoints;
            set => _currentExperiencePoints = value;
        }
        public int CurrentLevel{
            get => _currentLevel;
            set => _currentLevel = value;
        }
        public int MaximumPossibleCharacterLevel => _levels.Length;
        public int RewardForKilling{
            get => (int) _rewardForKilling;
            set => _rewardForKilling = value;
        }

        public UnitLevel(LevelValue[] levels){
            _levels = levels;
        }
    }
}