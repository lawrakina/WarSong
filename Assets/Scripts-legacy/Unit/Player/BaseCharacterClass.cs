using Enums;


namespace Unit.Player
{
    public abstract class BaseCharacterClass
    {
        public abstract string Name { get; }
        public abstract string Description { get; }
        public abstract ResourceEnum ResourceType { get; }
        public float ResourceBaseValue { get; set; }
        public abstract CharacterClass Class { get; }
    }
}