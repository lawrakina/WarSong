using Code.Data;


namespace Code.Unit
{
    public class BaseCharacterClass
    {
        public string Name;
        public CharacterClass Class;

        public BaseCharacterClass()
        {
            Name = "";
            Class = CharacterClass.None;
        }
    }
}