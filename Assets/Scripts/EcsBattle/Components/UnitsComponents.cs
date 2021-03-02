using Battle;
using Leopotam.Ecs;
using Unit;
using UnityEngine;
using VIew;


namespace EcsBattle.Components
{
    public struct PlayerComponent
    {
    }

    public struct AwaitTimerForVisionComponent
    {
        public float Value;
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
        public Transform Value;
        public Vector3 OffsetHead;
    }

    public struct DirectionMoving
    {
        public Vector3 Value;
    }

    public struct MovementSpeed
    {
        public float Value;
    }

    public struct RotateSpeed
    {
        public float Value;
    }

    public struct GoTargetComponent
    {
        public EcsEntity Value;
    }

    public struct AnimatorComponent
    {
        public AnimatorParameters Value;
    }

    public struct NeedUpdateMaxHpFromPlayerComponent
    {
        public float Value;
    }

    public struct AutoBattleDisableComponent
    {
    }

    public struct NeedUpdateCurrentHpFromPlayerComponent
    {
        public float Value;
    }

    public struct CurrentTargetComponent
    {
        public Transform Target;
        public float Distance;
    }
    
    public struct LayerMaskEnemiesComponent
    {
        public int Value;
    }

    public struct DetectionDistanceEnemyComponent
    {
        public float Value;
    }
}