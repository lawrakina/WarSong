using Code.Data.Unit;

namespace Code.Unit.Factories
{
    public interface IPlayerFactory
    {
        IPlayerView CreatePlayer(CharacterSettings settings);
        IPlayerView RebuildModel(IPlayerView character, CharacterSettings settings,
            RaceCharacteristics raceCharacteristics);
    }
}