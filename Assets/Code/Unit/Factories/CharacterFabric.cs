using Code.Data.Unit;
using Code.Extension;
using Code.Profile;
using Code.Unit.Factories;


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
        private readonly VisionFactory _visionFactory;
        private readonly ReputationFactory _reputationFactory;

        public CharacterFabric(IPlayerFactory playerFactory, CharacterClassesFactory classesFactory,
            LevelFactory levelFactory, ResourceFactory resourceFactory, CharacteristicsFactory characteristicsFactory,
            HealthFactory healthFactory, VisionFactory visionFactory, ReputationFactory reputationFactory)
        {
            _playerFactory = playerFactory;
            _classesFactory = classesFactory;
            _levelFactory = levelFactory;
            _resourceFactory = resourceFactory;
            _characteristicsFactory = characteristicsFactory;
            _healthFactory = healthFactory;
            _visionFactory = visionFactory;
            _reputationFactory = reputationFactory;
        }

        public IPlayerView CreatePlayer(CharacterSettings item)
        {
            return _playerFactory.CreatePlayer(item);
        }

        public void RebuildCharacter(IPlayerView character, CharacterSettings value)
        {
            var personCharacter = character.PersonCharacter;
            personCharacter.CharacterGender = value.CharacterGender;
            personCharacter.CharacterRace = value.CharacterRace;
            personCharacter.ClearAll();

            character.UnitEquipment.SetEquipment(value.Equipment);
            character.UnitEquipment.RebuildEquipment();
            character.AnimatorParameters.WeaponType = character.UnitEquipment.GetWeaponType();

            character.UnitVision = _visionFactory.GenerateVision();
            character.UnitReputation = _reputationFactory.GeneratePlayerReputation();
            character.UnitLevel = _levelFactory.GenerateLevel(character.UnitLevel, value);
            character.CharacterClass = _classesFactory.GenerateClass(character.CharacterClass, value);
            character.UnitCharacteristics = _characteristicsFactory.GenerateCharacteristics(character.UnitCharacteristics, character.UnitEquipment, character.UnitLevel, value);
            character.UnitResource = _resourceFactory.GenerateResource(character.UnitResource, character.UnitCharacteristics, character.UnitLevel, value);
            character.UnitHealth = _healthFactory.GenerateHealth(character.UnitHealth, character.UnitCharacteristics);
            // character.UnitEquipment.PutAllEquip();
        }
    }
}