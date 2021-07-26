using System;
using Code.Data;
using Code.Data.Unit;
using UnityEngine;


namespace Code.Unit
{
    public interface IBaseUnitView: ICollision
    {
        Transform Transform { get; }
        Transform TransformModel { get; set; }
        Collider Collider { get; }
        Rigidbody Rigidbody { get; set; }
        MeshRenderer MeshRenderer { get; }
        Animator Animator { get; set; }
        AnimatorParameters AnimatorParameters { get; }
        // UnitHealth UnitHealth { get; set; }
        // UnitAttributes Attributes { get; set; }
        // UnitVision UnitVision { get; set; }
        // UnitReputation UnitReputation { get; set; }
        UnitLevel UnitLevel { get; set; }
        UnitCharacteristics UnitCharacteristics { get; set; }
        // UnitInventory UnitInventory { get; set; }
        UnitEquipment UnitEquipment { get; set; }
        event Action<InfoCollision> OnApplyDamageChange;
    }
}