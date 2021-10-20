﻿using Code.Data.Unit;
using Code.Profile;


namespace Code.Unit.Factories
{
    public sealed class CharacterFabric
    {
        private readonly IPlayerFactory _playerFactory;
        private readonly LevelFactory _levelFactory;
        private readonly CharacterClassesFactory _classesFactory;
        private readonly ResourceFactory _resourceFactory;
        private readonly HealthFactory _healthFactory;
        private readonly CharacteristicsFactory _characteristicsFactory;
        private readonly VisionFactory _visionFactory;
        private readonly ReputationFactory _reputationFactory;
        private readonly EquipmentFactory _equipmentFactory;
        private readonly InventoryFactory _inventoryFactory;
        private readonly AbilitiesFactory _abilitiesFactory;
        private readonly BattleFactory _battleFactory;

        public CharacterFabric(IPlayerFactory playerFactory, CharacterClassesFactory classesFactory,
            LevelFactory levelFactory, ResourceFactory resourceFactory, CharacteristicsFactory characteristicsFactory,
            HealthFactory healthFactory, VisionFactory visionFactory, ReputationFactory reputationFactory,
            EquipmentFactory equipmentFactory, InventoryFactory inventoryFactory, AbilitiesFactory abilitiesFactory,
            BattleFactory battleFactory)
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
            _abilitiesFactory = abilitiesFactory;
            _battleFactory = battleFactory;
        }

        public IPlayerView CreatePlayer(CharacterSettings item)
        {
            return _playerFactory.CreatePlayer(item);
        }

        public void RebuildCharacter(IPlayerView character, CharacterSettings value, AttackCharacteristicCoeffsData attackCharacteristicCoeffsData)
        {
            character = _playerFactory.RebuildModel(character, value, _classesFactory.GetSettingsByRace(value.CharacterRace));
            character.UnitLevel = _levelFactory.GenerateLevel(character.UnitLevel, value);
            character.UnitEquipment = _equipmentFactory.GenerateEquip(character, value, character.UnitLevel.CurrentLevel);
            character.UnitInventory = _inventoryFactory.GenerateInventory(character, value);
            character.UnitAbilities = _abilitiesFactory.GenerateAbilities(value, character.UnitLevel.CurrentLevel);
            // character.AnimatorParameters.WeaponType = character.UnitEquipment.GetWeaponType();
            character.UnitVision = _visionFactory.GenerateVision();
            character.UnitReputation = _reputationFactory.GeneratePlayerReputation();
            character.CharacterClass = _classesFactory.GenerateClass(character.CharacterClass, value);
            character.UnitCharacteristics = _characteristicsFactory.GenerateCharacteristics(character.UnitCharacteristics, character.UnitEquipment, character.UnitLevel, value);
            character.UnitResource = _resourceFactory.GenerateResource(character.UnitResource, character.UnitCharacteristics, character.UnitLevel, value);
            character.UnitHealth = _healthFactory.GenerateHealth(character.UnitHealth, character.UnitCharacteristics);
            character.UnitBattle = _battleFactory.GenerateBattle(character.UnitCharacteristics, character.UnitEquipment);
        }
    }
}