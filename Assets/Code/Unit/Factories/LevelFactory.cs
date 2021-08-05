using Code.Data.Unit;
using Code.Data.Unit.Player;


namespace Code.Unit.Factories
{
    public sealed class LevelFactory
    {
        private readonly UnitLevelData _settings;

        public LevelFactory(UnitLevelData settings)
        {
            _settings = settings;
        }

        public UnitLevel GenerateLevel(UnitLevel characterUnitLevel, CharacterSettings value)
        {
            if (characterUnitLevel == null)
                characterUnitLevel = new UnitLevel();
            characterUnitLevel.CurrentLevel = 0;
            foreach (var level in _settings.Levels)
            {
                if (value.ExperiencePoints - level.MaxiPointsExperience >= 0)
                {
                    value.ExperiencePoints -= level.MaxiPointsExperience;
                    characterUnitLevel.CurrentExperiencePoints = value.ExperiencePoints;
                    characterUnitLevel.CurrentLevel++;
                }
            }

            return characterUnitLevel;
        }
    }
}