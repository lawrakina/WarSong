using Code.Data.Unit;
using Code.Unit.Factories;
using UnityEngine;


namespace Code.Unit
{
    public interface IPlayerView : IBaseUnitView
    {
        Transform TransformModel { get; set; }
        UnitEquipment UnitEquipment { get; set; }
        BaseCharacterClass CharacterClass { get; set; }
        UnitPerson UnitPerson { get; set; }
        UnitInventory UnitInventory { get; set; }
    }
}