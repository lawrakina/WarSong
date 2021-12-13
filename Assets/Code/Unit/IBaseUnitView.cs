using System;
using Code.Data.Unit;
using KinematicCharacterController;
using UnityEngine;
using UnitCharacteristics = Code.Data.Unit.UnitCharacteristics;
using UnitHealth = Code.Data.Unit.UnitHealth;
using UnitLevel = Code.Data.Unit.UnitLevel;
using UnitReputation = Code.Data.Unit.UnitReputation;


namespace Code.Unit
{
    public interface IBaseUnitView: ICollision
    {
        KinematicCharacterMotor Motor{ get; set; }
        UnitMovement UnitMovement{ get; set; }
        Transform Transform{ get; set; }
        Transform TransformModel{ get; set; }
        CapsuleCollider Collider { get; set; }
        MeshRenderer MeshRenderer { get; set; }
        Animator Animator { get; set; }
        AnimatorParameters AnimatorParameters { get; set; }
        UnitHealth UnitHealth { get; set; }
        UnitVision UnitVision { get; set; }
        UnitReputation UnitReputation { get; set; }
        UnitResource UnitResource { get; set; }
        UnitLevel UnitLevel { get; set; }
        UnitCharacteristics UnitCharacteristics { get; set; }
        UnitBattle UnitBattle { get; set; }
        event Action<InfoCollision> OnApplyDamageChange;
    }
}