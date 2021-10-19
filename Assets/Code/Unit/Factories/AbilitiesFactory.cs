using Code.Data.Unit;
using Code.Data.Unit.Player;


namespace Code.Unit.Factories{
    public sealed class AbilitiesFactory{
        private readonly PlayerClassesData _data;

        public AbilitiesFactory(PlayerClassesData data){
            _data = data;
        }

        public UnitAbilities GenerateAbilities(CharacterSettings settings, int currentLevel){
            return new UnitAbilities(_data, settings.Abilities, currentLevel);
        }
    }
}