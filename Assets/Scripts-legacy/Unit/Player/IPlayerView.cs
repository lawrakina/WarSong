using CharacterCustomizing;
using Guirao.UltimateTextDamage;


namespace Unit.Player
{
    public interface IPlayerView : IBaseUnitView
    {
        UnitCharacteristics UnitCharacteristics { get; set; }
        BaseCharacterClass CharacterClass { get; set; }
        EquipmentPoints EquipmentPoints { get; set; }
        EquipmentItems EquipmentItems { get; set; }
        UnitPlayerBattle UnitPlayerBattle { get; set; }
        UltimateTextDamageManager UiTextManager { get; set; }
    }
}