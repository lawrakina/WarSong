using Enums;


namespace Unit.Player
{
    public abstract class BaseCharacterClass
    {
        public abstract string Name { get; }
        public abstract string Description { get; }
        public float CurrentHp { get; set; }
        public float BaseHp { get; set; }
        public float MaxHp
        {
            get => BaseHp;//ToDo добавить расчет бонуса от шмоток
        }
        public abstract ResourceEnum ResourceType { get; }
        public float ResourceBaseValue { get; set; }
    }
}