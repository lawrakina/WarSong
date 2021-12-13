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
                characterUnitLevel = new UnitLevel(_settings.Levels);
            characterUnitLevel.CurrentLevel = 0;
            foreach (var level in _settings.Levels){
                var experiencePoints = value.ExperiencePoints;
                if (experiencePoints - level.MaxiPointsExperience >= 0)
                {
                    experiencePoints -= level.MaxiPointsExperience;
                    characterUnitLevel.CurrentExperiencePoints = experiencePoints;
                    characterUnitLevel.CurrentLevel++;
                }
            }

            return characterUnitLevel;
        }
    }
}