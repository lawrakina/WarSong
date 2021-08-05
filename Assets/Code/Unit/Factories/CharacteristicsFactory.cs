using System.Linq;
using Code.Data.Unit;
using Code.Data.Unit.Player;


namespace Code.Unit.Factories
{
    public sealed class CharacteristicsFactory
    {
        private readonly PlayerClassesData _settings;

        public CharacteristicsFactory(PlayerClassesData settings)
        {
            _settings = settings;
        }

        public UnitCharacteristics GenerateCharacteristics(UnitCharacteristics characteristics, UnitEquipment equipment,
            UnitLevel level, CharacterSettings value)
        {
            if (characteristics == null)
            {
                characteristics = new UnitCharacteristics();
                var fromDataBase = _settings._classesStartCharacteristics.FirstOrDefault(
                    x => x.CharacterClass == value.CharacterClass);
                characteristics.Start = fromDataBase.Start;
                characteristics.ForOneLevel = fromDataBase.ForOneLevel;
            }
            characteristics.Values.Agility = equipment.FullAgility + characteristics.Start.Agility +
                                             characteristics.ForOneLevel.Agility * level.CurrentLevel;
            characteristics.Values.Intellect = equipment.FullIntellect +characteristics.Start.Intellect +
                                               characteristics.ForOneLevel.Intellect * level.CurrentLevel;
            characteristics.Values.Spirit = equipment.FullSpirit +characteristics.Start.Spirit + 
                                            characteristics.ForOneLevel.Spirit * level.CurrentLevel;
            characteristics.Values.Stamina = equipment.FullStamina +characteristics.Start.Stamina +
                                             characteristics.ForOneLevel.Stamina * level.CurrentLevel;
            characteristics.Values.Strength = equipment.FullStrength +characteristics.Start.Strength +
                                              characteristics.ForOneLevel.Strength * level.CurrentLevel;
            
            return characteristics;
        }
    }
}