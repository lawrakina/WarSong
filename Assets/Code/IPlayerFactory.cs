using Code.Data;
using Code.Data.Unit;
using Code.Unit;


namespace Code
{
    public interface IPlayerFactory
    {
        IPlayerView CreatePlayer(CharacterSettings settings);
    }
}