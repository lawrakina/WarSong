namespace Unit.Player
{
    public abstract class BaseCharacterClass
    {
        public abstract string Name { get; }
        public abstract string Description { get; }
        public abstract int Hp { get; }
        public abstract int MaxHp { get; }
    }
}