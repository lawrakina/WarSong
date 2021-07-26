using Code.CharacterCustomizing;
using Code.Data.Unit;


namespace Code.Unit
{
    public interface IPlayerView : IBaseUnitView
    {
        BaseCharacterClass CharacterClass { get; set; }
        PersonCharacter PersonCharacter { get;  }
    }
}