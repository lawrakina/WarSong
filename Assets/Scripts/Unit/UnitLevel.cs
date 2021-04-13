namespace Unit
{
    public class UnitLevel
    {
        #region Properties

        public int CurrentLevel { get; private set; }
        public int CurrentExperiencePoints { get; set; }
        public float RewardForKilling { get; set; }

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
    

    public class UnitHealth
    {
        public float CurrentHp { get; set; }
        public float MaxHp { get; set; }
        // todo add new class //UnitHealth
        // {
        //     CurrentHp
        //         MaxHp
        //     ChangeCurrentHp
        //         ChangeMaxHp
        //     CurrentResource
        //         MaxResource
        //     ChangeCurrentResource
        //         ChangeMaxResource
        //     List<Buff> - все положительные эффекты
        //     List<Debuff> - все отрицательные эффекты
        // }
    }
}