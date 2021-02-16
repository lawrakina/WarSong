using Enums;


namespace Unit.Player
{
    public abstract class BaseCharacterClass
    {
        private int _currnetLevel;
        public int CurrentLevel
        {
            get { return _currnetLevel; }
            set { _currnetLevel = value; }
        }
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