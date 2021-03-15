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

    public struct TransformComponent
    {
        public Transform value;
    }

    public struct MovementSpeed
    {
        public float value;
    }

    public struct RotateSpeed
    {
        public float value;
    }

    public struct DirectionMovementComponent
    {
        public Transform value;
    }

    public struct UnitVisionComponent
    {
        public UnitVision value;
    }

    public struct UnitReputationComponent
    {
        public UnitReputation value;
    }

    public struct RigidbodyComponent
    {
        public Rigidbody value;
    }

    public struct TargetEntityComponent
    {
        public EcsEntity value;
    }

    public struct AnimatorComponent
    {
        public AnimatorParameters value;
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
}