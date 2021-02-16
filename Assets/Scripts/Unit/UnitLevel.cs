namespace Unit
{
    public class UnitLevel
    {
        #region Properties

        public int CurrentLevel { get; private set; }
        public int CurrentExperiencePoints { get; set; }

        #endregion


        #region ctor

        public UnitLevel()
        {
            CurrentLevel = 1;
            CurrentExperiencePoints = 1;
        }

        #endregion


        #region Methods

        public void UpNextLevel()
        {
            CurrentLevel++;
        }

        #endregion
    }
}