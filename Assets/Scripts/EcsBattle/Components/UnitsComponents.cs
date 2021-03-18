using System.Collections.Generic;
using Battle;
using Leopotam.Ecs;
using Unit;
using UnityEngine;
using VIew;


namespace EcsBattle.Components
{
    public struct PlayerComponent
    {
        public Transform rootTransform;
        public Transform modelTransform;
        public Rigidbody rigidbody;
        public UnitReputation unitReputation;
        public UnitVision unitVision;
        public UnitAttributes attributes;
        public AnimatorParameters animator;
    }

    public struct AwaitTimerForVisionComponent
    {
        public float Value;
    }

    public struct TimerStopFollowingInPlayerComponent
    {
        public float currentTime;
        public float maxTime;
    }

    public struct TimerTickedForVisionComponent
    {
    }

    public struct EnemyComponent
    {
    }

    public struct UnitComponent
    {
        public Transform transform;
        public Rigidbody rigidbody;
        public Collider collider;
        public AnimatorParameters animator;
        public UnitReputation reputation;
        public UnitAttributes attributes;
        public UnitVision vision;
    }

    public struct ListRigidBAndCollidersComponent
    {
        public List<Rigidbody> rigidbodies;
        public List<Collider> colliders;
    }

    public struct AttackCollisionComponent
    {
        public InfoCollision Value;
    }

    public struct UiEnemyHealthBarComponent
    {
        public HealthBarView Value;
    }

    public struct UnitHpComponent
    {
        public float CurrentValue;
        public float MaxValue;
    }

    public struct DeathEventComponent
    {
        public EcsEntity _killer;
    }

    public struct TransformComponent
    {
        public Transform value;
    }

    public struct DirectionMovementComponent
    {
        public Transform value;
    }

    public struct TargetEntityComponent
    {
        public EcsEntity value;
    }

    public struct NeedStepComponent
    {
        public Vector3 value;
    }

    public struct NeedRotateComponent
    {
        public Transform value;
    }

    public struct CurrentTargetComponent
    {
        public Transform Target;
        public float sqrDistance;
    }


    public struct NeedStartAnimationComponent
    {
    }

    public struct NeedAttackComponent
    {
    }

    public struct NeedMoveToTargetAndAttackComponent
    {
    }
    public struct NeedFindTargetComponent
    {
    }

    public struct NeedLookAtTargetComponent
    {
    }

    public struct FinalAttackComponent
    {
    }
}