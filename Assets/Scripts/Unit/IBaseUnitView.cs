using System;
using Battle;
using UnityEngine;


namespace Unit
{
    public interface IBaseUnitView: ICollision
    {
        Transform Transform { get; }
        Collider Collider { get; }
        Rigidbody Rigidbody { get; }
        MeshRenderer MeshRenderer { get; }
        Animator Animator { get; }
        AnimatorParameters AnimatorParameters { get; }
        ICharAttributes CharAttributes { get; set; }
        UnitLevel UnitLevel { get; set; }
        UnitVision UnitVision { get; set; }
        UnitBattle UnitBattle { get; set; }
        UnitReputation UnitReputation { get; set; }
        float CurrentHp { get; set; }
        float BaseHp { get; set; }
        float MaxHp { get; set; }
        event Action<InfoCollision> OnApplyDamageChange;
    }
}