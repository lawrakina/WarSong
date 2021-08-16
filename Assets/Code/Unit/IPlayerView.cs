using Code.CharacterCustomizing;
using Code.Data.Unit;
using UnityEngine;


namespace Code.Unit
{
    public interface IPlayerView : IBaseUnitView
    {
        Transform TransformModel { get; set; }
        UnitEquipment UnitEquipment { get; set; }
        BaseCharacterClass CharacterClass { get; set; }
        PersonCharacter PersonCharacter { get;  }
    }
}