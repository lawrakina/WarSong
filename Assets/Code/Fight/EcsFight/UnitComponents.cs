using System.Collections.Generic;
using Code.Data.Unit;
using Code.Unit;
using Leopotam.Ecs;
using UnityEngine;


namespace Code.Fight.EcsFight{
    public struct NeedRotateC{
        public Transform Value;
    }

    public struct NeedStepC{
        public Vector3 Value;
    }

    public struct MovementEventC{
        public Vector3 Value;
    }

    public struct SwipeEventC{
        public Vector3 Value;
    }

    public struct ClickEventC{
    }

    public struct UnpressJoystickC{
        public Vector3 LastValueVector;
        public float PressTime;
    }

    public struct InputControlC{
        public UltimateJoystick joystick;
        public float Time;
        public float TimeToClick;
        public Vector3 MaxOffsetForClick;
        public Vector3 MaxOffsetForMovement;
        public Vector3 LastPosition;
    }

    public struct TargetC{
        public EcsEntity Value;
    }

    public struct DirectionMovementC{
        public Transform Value;
    }

    public struct UnitC{
        public Transform RootTransform;
        public Transform ModelTransform;
        public Rigidbody Rigidbody;
        public AnimatorParameters Animator;
        public UnitCharacteristics Characteristics;
        public UnitHealth Health;
        public UnitResource Resource;
        public UnitVision Vision;
        public UnitReputation Reputation;
    }

    public struct PlayerTag{
    }
}