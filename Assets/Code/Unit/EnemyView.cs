﻿using System;
using Code.Data.Unit;
using Code.Extension;
using KinematicCharacterController;
using UnityEngine;


namespace Code.Unit
{
    public class EnemyView : MonoBehaviour, IEnemyView
    {
        public KinematicCharacterMotor Motor{ get; set; }
        public UnitMovement UnitMovement{ get; set; }
        public Transform Transform { get; set; }
        public Transform TransformModel{ get; set; }
        public CapsuleCollider Collider { get; set; }
        public MeshRenderer MeshRenderer { get; set; }
        public Animator Animator { get; set; }
        public AnimatorParameters AnimatorParameters { get; set; }
        public UnitHealth UnitHealth { get; set; }
        public UnitVision UnitVision { get; set; }
        public UnitReputation UnitReputation { get; set; }
        public UnitResource UnitResource { get; set; }
        public UnitLevel UnitLevel { get; set; }
        public UnitCharacteristics UnitCharacteristics { get; set; }
        public UnitBattle UnitBattle { get; set; }

        // public UnitEquipmentOld unitEquipmentOld { get; set; }
        public HealthBarView HealthBar { get; set; }

        public event Action<InfoCollision> OnApplyDamageChange;
        public event Action<InfoResource> OnApplyResourceChange;

        public void OnCollision(InfoCollision info)
        {
            Dbg.Log($"{gameObject.name} Attacked");
            OnApplyDamageChange?.Invoke(info);
        }

        public void OnCollision(InfoResource collision){
            Dbg.Log($"Change resource: {collision.Value}, sender: {collision.Sender}");
            OnApplyResourceChange?.Invoke(collision);
        }
    }
}