using Data;
using Enums;
using UniRx;


namespace Unit.Player
{
    public sealed class PrototypePlayerModel
    {
        #region ClassLiveCycles

        public PrototypePlayerModel()
        {
            CharacterClass = new ReactiveProperty<CharacterClass>(Enums.CharacterClass.Warrior);
            CharacterGender = new ReactiveProperty<CharacterGender>(Enums.CharacterGender.Male);
            CharacterRace = new ReactiveProperty<CharacterRace>(Enums.CharacterRace.Human);
        }

        #endregion


        #region Properties

        public IReactiveProperty<CharacterClass> CharacterClass { get; }
        public IReactiveProperty<CharacterGender> CharacterGender { get; }
        public IReactiveProperty<CharacterRace> CharacterRace { get; }

        public CharacterSettings GetCharacterSettings
        {
            get
            {
                var result = new CharacterSettings();
                result.CharacterClass = CharacterClass.Value;
                result.CharacterGender = CharacterGender.Value;
                result.CharacterRace = CharacterRace.Value;
                return result;
            }
        }

        #endregion
    }
}