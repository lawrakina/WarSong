using CharacterCustomizing;
using Guirao.UltimateTextDamage;


namespace Unit.Player
{
    public interface IPlayerView : IBaseUnitView
    {
        BasicCharacteristics BasicCharacteristics { get; set; }
        BaseCharacterClass CharacterClass { get; set; }
        EquipmentPoints EquipmentPoints { get; set; }
        EquipmentItems EquipmentItems { get; set; }
        UnitPlayerBattle UnitPlayerBattle { get; set; }
        UltimateTextDamageManager UiTextManager { get; set; }
    }
}