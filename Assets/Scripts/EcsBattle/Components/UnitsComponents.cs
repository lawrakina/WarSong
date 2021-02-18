﻿using Leopotam.Ecs;
using Unit;
using UnityEngine;
using VIew;


namespace EcsBattle.Components
{
    public struct PlayerComponent
    {
    }
    public struct EnemyComponent
    {
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
    public struct RigidBodyComponent
    {
        public Rigidbody Value;
    }

    public struct TransformComponent
    {
        public Transform Value;
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

    public struct NeedUpdateCurrentHpFromPlayerComponent
    {
        public float Value;
    }
}