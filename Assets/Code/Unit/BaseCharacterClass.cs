using Code.Data;


namespace Code.Unit
{
    public class BaseCharacterClass
    {
        public string Name;
        public ResourceEnum ResourceType;
        public float ResourceBaseValue;
        public CharacterClass Class;

        public BaseCharacterClass()
        {
            Name = "";
            ResourceType = ResourceEnum.Mana;
            ResourceBaseValue = 1;
            Class = CharacterClass.None;
        }
    }
}