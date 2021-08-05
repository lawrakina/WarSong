using System.Linq;
using Code.Data.Unit;
using Code.Data.Unit.Player;


namespace Code.Unit.Factories
{
    public sealed class CharacterClassesFactory
    {
        private readonly PlayerClassesData _settings;

        public CharacterClassesFactory(PlayerClassesData settings)
        {
            _settings = settings;
        }

        public BaseCharacterClass GenerateClass(BaseCharacterClass characterClass, CharacterSettings value)
        {
            if(characterClass == null)
                characterClass = new BaseCharacterClass();

            var fromDataBase = _settings._presetCharacters.listPresetsSettings.FirstOrDefault(
                x => x.CharacterClass == value.CharacterClass);
            
            characterClass.Class = value.CharacterClass;
            characterClass.Name = value.CharacterClass.ToString(); //ToDo need using Localization;

            return characterClass;
        }
    }
}