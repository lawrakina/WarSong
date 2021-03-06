﻿using System.Collections.Generic;
using Battle;
using Enums;
using Guirao.UltimateTextDamage;
using Leopotam.Ecs;
using Necromancy.UI;
using Unit;
using Unit.Enemies;
using Unit.Player;
using UnityEngine;


namespace EcsBattle.Components
{
    public struct PlayerComponent
    {
        public CharacterClass _unitClass;
        // public IPlayerView _view;
    }
    public struct EnemyComponent
    {
    }

    public struct AwaitTimerForVisionComponent
    {
        public float _value;
    }

    public struct TimerStopFollowingInPlayerComponent
    {
        public float _currentTime;
        public float _maxTime;
    }

    public struct TimerTickedForCheckVisionComponent
    {
    }


    public struct UnitComponent
    {
        public IBaseUnitView _view;
        public Transform _rootTransform;
        public Transform _modelTransform;
        public Rigidbody _rigidBody;
        public Collider _collider;
        public AnimatorParameters _animator;
        public UnitReputation _reputation;
        public UnitAttributes _attributes;
        public UnitVision _vision;
        public UnitLevel _level;
        public UnitHealth _health;
        public UnitCharacteristics _characteristics;
    }

    public struct ListRigidBAndCollidersComponent
    {
        public List<Rigidbody> _rigidBodies;
        public List<Collider> _colliders;
    }

    public struct AttackCollisionComponent
    {
        public InfoCollision _value;
    }

    public struct UiEnemyHealthBarComponent
    {
        public HealthBarView _value;
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
        public Transform _value;
    }

    public struct TargetEntityComponent
    {
        public EcsEntity _value;
    }

    public struct NeedStepComponent
    {
        public Vector3 _value;
    }

    public struct NeedRotateComponent
    {
        public Transform _value;
    }

    public struct CurrentTargetComponent
    {
        public IBaseUnitView _baseUnitView;
        public float _sqrDistance;
    }


    public struct NeedStartAnimationAttackFromMainWeaponComponent
    {
    }

    public struct NeedAttackFromMainWeaponComponent
    {
    }

    public struct NeedAttackFromSecondWeaponComponent
    {
    }

    public struct NeedStartAnimationAttackFromSecondWeaponComponent
    {
        public float _currentTimeForLag;
        public float _maxTimeForLag;
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

    public struct FinalAttackFromMainWeaponComponent
    {
    }

    public struct FinalAttackFromSecondWeaponComponent
    {
    }
}