using Code.Data.Unit;


namespace Code.Unit.Factories
{
    public sealed class EquipmentFactory
    {
        private IPlayerView _character;
        private CharacterSettings _settings;
        private EquipmentPoints _equipPoints;
        private UnitPerson _person;

        public UnitEquipment GenerateEquip(IPlayerView character, CharacterSettings settings)
        {
            _character = character;
            _settings = settings;
            _person = character.UnitPerson;
            _equipPoints = _person.EquipmentPoints;

            //Create Person Appearance
            _person.Generate(_settings);
            
            var equip = new UnitEquipment(_person);

            return equip;
        }
    }
}