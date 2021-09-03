using Code.Data.Unit;
using Code.Profile;


namespace Code.Unit.Factories
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
        private readonly EquipmentFactory _equipmentFactory;
        private readonly InventoryFactory _inventoryFactory;

        public CharacterFabric(IPlayerFactory playerFactory, CharacterClassesFactory classesFactory,
            LevelFactory levelFactory, ResourceFactory resourceFactory, CharacteristicsFactory characteristicsFactory,
            HealthFactory healthFactory, VisionFactory visionFactory, ReputationFactory reputationFactory,
            EquipmentFactory equipmentFactory, InventoryFactory inventoryFactory)
        {
            _playerFactory = playerFactory;
            _classesFactory = classesFactory;
            _levelFactory = levelFactory;
            _resourceFactory = resourceFactory;
            _characteristicsFactory = characteristicsFactory;
            _healthFactory = healthFactory;
            _visionFactory = visionFactory;
            _reputationFactory = reputationFactory;
            _equipmentFactory = equipmentFactory;
            _inventoryFactory = inventoryFactory;
        }

        public IPlayerView CreatePlayer(CharacterSettings item)
        {
            return _playerFactory.CreatePlayer(item);
        }

        public void RebuildCharacter(IPlayerView character, CharacterSettings value)
        {
            character.UnitEquipment = _equipmentFactory.GenerateEquip(character, value);
            character.UnitInventory = _inventoryFactory.GenerateInventory(character, value);
            // character.AnimatorParameters.WeaponType = character.UnitEquipment.GetWeaponType();
            character.UnitVision = _visionFactory.GenerateVision();
            character.UnitReputation = _reputationFactory.GeneratePlayerReputation();
            character.UnitLevel = _levelFactory.GenerateLevel(character.UnitLevel, value);
            character.CharacterClass = _classesFactory.GenerateClass(character.CharacterClass, value);
            character.UnitCharacteristics = _characteristicsFactory.GenerateCharacteristics(character.UnitCharacteristics, character.UnitEquipment, character.UnitLevel, value);
            character.UnitResource = _resourceFactory.GenerateResource(character.UnitResource, character.UnitCharacteristics, character.UnitLevel, value);
            character.UnitHealth = _healthFactory.GenerateHealth(character.UnitHealth, character.UnitCharacteristics);
        }
    }
}