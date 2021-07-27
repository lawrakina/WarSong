using Code.Data.Unit;
using Code.Extension;
using Code.Profile;


namespace Code.Unit
{
    public sealed class CharacterFabric
    {
        private IPlayerFactory _playerFactory;
        private readonly LevelFactory _levelFactory;
        private readonly CharacterClassesFactory _classesFactory;
        private readonly ResourceFactory _resourceFactory;
        private readonly HealthFactory _healthFactory;
        private readonly CharacteristicsFactory _characteristicsFactory;

        public CharacterFabric(IPlayerFactory playerFactory, CharacterClassesFactory classesFactory,
            LevelFactory levelFactory, ResourceFactory resourceFactory, CharacteristicsFactory characteristicsFactory, HealthFactory healthFactory)
        {
            _playerFactory = playerFactory;
            _classesFactory = classesFactory;
            _levelFactory = levelFactory;
            _resourceFactory = resourceFactory;
            _characteristicsFactory = characteristicsFactory;
            _healthFactory = healthFactory;
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

            character.UnitLevel = _levelFactory.GenerateLevel(character.UnitLevel, value);
            character.CharacterClass = _classesFactory.GenerateClass(character.CharacterClass, value);
            character.UnitCharacteristics = _characteristicsFactory.GenerateCharacteristics(character.UnitCharacteristics, character.UnitEquipment, character.UnitLevel, value);
            character.UnitResource = _resourceFactory.GenerateResource(character.UnitResource, character.UnitCharacteristics, character.UnitLevel, value);
            character.UnitHealth = _healthFactory.GenerateHealth(character.UnitHealth, character.UnitCharacteristics);
        }
    }
}