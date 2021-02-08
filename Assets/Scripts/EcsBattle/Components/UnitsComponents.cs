﻿using Leopotam.Ecs;
using UnityEngine;


namespace EcsBattle.Components
{
    public struct PlayerComponent
    {
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
}