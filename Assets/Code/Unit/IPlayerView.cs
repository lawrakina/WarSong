using System;
using Code.Data.Unit;
using UnityEngine;


namespace Code.Unit{
    public interface IPlayerView : IBaseUnitView{
        BaseCharacterClass CharacterClass{ get; set; }
        UnitInventory UnitInventory{ get; set; }
        UnitPerson UnitPerson{ get; set; }
        UnitEquipment UnitEquipment{ get; set; }
        UnitAbilities UnitAbilities{ get; set; }
    }
}