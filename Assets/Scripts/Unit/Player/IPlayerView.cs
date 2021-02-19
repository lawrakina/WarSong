using CharacterCustomizing;


namespace Unit.Player
{
    public interface IPlayerView : IBaseUnitView
    {
        UnitLevel Level { get; set; }
        BasicCharacteristics BasicCharacteristics { get; set; }
        BaseCharacterClass CharacterClass { get; set; }
        EquipmentPoints EquipmentPoints { get; set; }
        EquipmentItems EquipmentItems { get; set; }
        Vision Vision { get; set; }
    }
}