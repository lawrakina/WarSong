namespace Code.Data.Unit
{
    public class UnitLevel
    {
        private int _currentLevel;
        public int _currentExperiencePoints;
        public float _rewardForKilling;
        
        public int CurrentLevel
        {
            get => _currentLevel;
            set => _currentLevel = value;
        }

        public int CurrentExperiencePoints
        {
            get => _currentExperiencePoints;
            set => _currentExperiencePoints = value;
        }
    }
}