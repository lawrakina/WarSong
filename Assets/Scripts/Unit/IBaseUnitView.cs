using System;
using Battle;
using UnityEngine;


namespace Unit
{
    public interface IBaseUnitView: ICollision
    {
        Transform Transform { get; set; }
        Transform TransformModel { get; set; }
        Collider Collider { get; }
        Rigidbody Rigidbody { get; set; }
        MeshRenderer MeshRenderer { get; }
        Animator Animator { get; set; }
        AnimatorParameters AnimatorParameters { get; }
        UnitAttributes Attributes { get; set; }
        UnitVision UnitVision { get; set; }
        UnitReputation UnitReputation { get; set; }
        UnitLevel UnitLevel { get; set; }
        float CurrentHp { get; set; }
        float BaseHp { get; set; }
        float MaxHp { get; set; }
        event Action<InfoCollision> OnApplyDamageChange;
    }
}