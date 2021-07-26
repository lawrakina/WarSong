using Code.Data.Unit;


namespace Code.Unit
{
    public sealed class CharacterFabric
    {
        private IPlayerFactory _playerFactory;
        private readonly LevelFactory _levelFactory;
        private readonly CharacterClassesFactory _classesFactory;

        public CharacterFabric(IPlayerFactory playerFactory, CharacterClassesFactory classesFactory,
            LevelFactory levelFactory)
        {
            _playerFactory = playerFactory;
            _classesFactory = classesFactory;
            _levelFactory = levelFactory;
        }

        public IPlayerView CreatePlayer(CharacterSettings item)
        {
            return _playerFactory.CreatePlayer(item);
        }

        public void RebuildCharacter(IPlayerView character, CharacterSettings value)
        {
            var settings = character.PersonCharacter;
            settings.CharacterGender = value.CharacterGender;
            settings.CharacterRace = value.CharacterRace;
            settings.Generate();

            character.UnitEquipment.SetEquipment(value.Equipment);
            character.UnitEquipment.RebuildEquipment();
            character.AnimatorParameters.WeaponType = character.UnitEquipment.GetWeaponType();

            _levelFactory.GenerateLevel(character.UnitLevel, value);
            _classesFactory.GenerateClass(character.CharacterClass, value);
        }
    }
}