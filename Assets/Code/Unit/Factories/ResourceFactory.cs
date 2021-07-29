using System;
using System.Linq;
using Code.Data;
using Code.Data.Unit;
using Code.Data.Unit.Player;


namespace Code.Unit
{
    public sealed class ResourceFactory
    {
        private readonly PlayerClassesData _settings;

        public ResourceFactory(PlayerClassesData settings)
        {
            _settings = settings;
        }

        public UnitResource GenerateResource(UnitResource resource, UnitCharacteristics characteristics, UnitLevel level,
            CharacterSettings value)
        {
            resource = null;
            if (resource == null)
                resource = new UnitResource();


            var fromDataBase = _settings._presetCharacters.listPresetsSettings.FirstOrDefault(
                x => x.CharacterClass == value.CharacterClass);

            resource.ResourceType = fromDataBase.ResourceType;
            resource.ResourceBaseValue = fromDataBase.ResourceBaseValue;

            switch (resource.ResourceType)
            {
                case ResourceEnum.Rage:
                    resource.MaxValue = (int) _settings.MaxRageValue;
                    break;
                case ResourceEnum.Energy:
                    resource.MaxValue = (int) _settings.MaxEnergyValue;
                    break;
                case ResourceEnum.Concentration:
                    resource.MaxValue = (int) _settings.MaxConcentrationValue;
                    break;
                case ResourceEnum.Mana:
                    resource.MaxValue = (int) (characteristics.Values.Intellect * level.CurrentLevel *
                                               _settings.ManaPointsPerIntellect);
                    break;
                default:
                    throw new ArgumentOutOfRangeException($"Resource type not found: {resource.ResourceType}");
            }
            return resource;
        }
    }
}