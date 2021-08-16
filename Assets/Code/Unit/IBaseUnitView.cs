using System;
using Code.Data.Unit;
using UnityEngine;
using UnitCharacteristics = Code.Data.Unit.UnitCharacteristics;
using UnitHealth = Code.Data.Unit.UnitHealth;
using UnitLevel = Code.Data.Unit.UnitLevel;
using UnitReputation = Code.Data.Unit.UnitReputation;
using UnitVision = Code.Data.Unit.UnitVision;


namespace Code.Unit
{
    public interface IBaseUnitView: ICollision
    {
        Transform Transform { get; set; }
        Collider Collider { get; set; }
        Rigidbody Rigidbody { get; set; }
        MeshRenderer MeshRenderer { get; }
        Animator Animator { get; set; }
        AnimatorParameters AnimatorParameters { get; }
        UnitHealth UnitHealth { get; set; }
        UnitVision UnitVision { get; set; }
        UnitReputation UnitReputation { get; set; }
        UnitResource UnitResource { get; set; }
        UnitLevel UnitLevel { get; set; }
        UnitCharacteristics UnitCharacteristics { get; set; }
        event Action<InfoCollision> OnApplyDamageChange;
    }
}