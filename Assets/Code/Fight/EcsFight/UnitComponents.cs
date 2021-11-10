using System;
using System.Collections.Generic;
using Code.Data.Unit;
using Code.Fight.EcsFight.Create;
using Code.Fight.EcsFight.Input;
using Code.GameCamera;
using Code.Unit;
using Leopotam.Ecs;
using SensorToolkit;
using UnityEngine;


namespace Code.Fight.EcsFight{
    public struct NeedAttackTargetC{
    }

    public struct NeedFindTargetTag{
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

    public struct TargetUnitC{
        public EcsEntity Value;
    }

    public struct AnimatorTag{
    }

    public struct UnitC{
        public UnitMovement UnitMovement;
        public Transform Transform => UnitMovement.transform;
        public AnimatorParameters Animator;
        public UnitCharacteristics Characteristics;
        public UnitHealth Health;
        public UnitResource Resource;
        public UnitVision UnitVision;
        public UnitReputation Reputation;
        public ListWeapons Weapons;
        public UnitLevel UnitLevel;
    }

    public struct CameraC{
        public BattleCamera Value;
    }

    public struct ManualMoveEventC{
        public Vector3 Vector;
        public Quaternion CameraRotation;
        public ControlType ControlType;
    }

    public struct AutoMoveEventC{
        public Vector3 Vector;
        public Quaternion CameraRotation;
        public ControlType ControlType;
    }

    public enum ControlType{
        Manual,
        AutoAttack
    }

    public struct TargetListC{
        private GameObject _target;
        public GameObject Current{
            set{
                IsExist = !Equals(value, null);
                _target = value;
            }
            get => _target;
        }
        public List<GameObject> List;
        public bool IsExist;
        public float SqrDistance;
        // public Action<GameObject, Sensor> OnDetected;
        // public Action<GameObject, Sensor> OnLostDetection;
    }
}