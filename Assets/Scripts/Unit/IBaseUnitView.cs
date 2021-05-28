using System;
using Windows;
using Battle;
using UnityEngine;


namespace Unit
{
    public interface IBaseUnitView: ICollision
    {
        Transform Transform { get; }
        Transform TransformModel { get; set; }
        Collider Collider { get; }
        Rigidbody Rigidbody { get; set; }
        MeshRenderer MeshRenderer { get; }
        UnitHealth UnitHealth { get; set; }
        Animator Animator { get; set; }
        AnimatorParameters AnimatorParameters { get; }
        UnitAttributes Attributes { get; set; }
        UnitVision UnitVision { get; set; }
        UnitReputation UnitReputation { get; set; }
        UnitLevel UnitLevel { get; set; }
        UnitAbilities UnitAbilities { get; set; }
        event Action<InfoCollision> OnApplyDamageChange;
    }
}