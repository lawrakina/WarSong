using Controller;
using Leopotam.Ecs;
using UniRx;


namespace Unit.Player
{
    public interface IPlayerView : IBaseUnitView
    {
        BaseCharacterClass CharacterClass { get; set; }
        StringReactiveProperty Description { get; }
        EquipmentPoints EquipmentPoints { get; set; }
    }
}