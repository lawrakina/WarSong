using Code.Data.Unit;


namespace Code.Unit.Factories
{
    public sealed class EquipmentFactory
    {
        private IPlayerView _character;

        private CharacterSettings _settings;
        private int _characterLevel;

        public UnitEquipment GenerateEquip(IPlayerView character, CharacterSettings settings, int characterLevel)
        {
            _character = character;
            _settings = settings;
            _characterLevel = characterLevel;

            return new UnitEquipment(_character.UnitPerson, _settings.Equipment, _characterLevel);
        }
    }
}