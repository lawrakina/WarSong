namespace Unit.Enemies
{
    public abstract class BaseEnemyClass
    {
        public abstract string Name { get; }
        public abstract string Description { get; }
        public float CurrentHp { get; set; }
        public float BaseHp { get; set; }

        public float MaxHp
        {
            get => BaseHp; //ToDo добавить расчет бонуса от шмоток
        }
    }
}