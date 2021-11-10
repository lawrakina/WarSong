using System;
using Code.Data.Unit;
using Code.Extension;
using Code.Unit.Factories;
using KinematicCharacterController;
using UnityEngine;


namespace Code.Unit
{
    public sealed class PlayerView : MonoBehaviour, IPlayerView
    {
        public KinematicCharacterMotor Motor{ get; set; }
        public UnitMovement UnitMovement{ get; set; }
        public Transform Transform { get; set; }
        public Transform TransformModel{ get; set; }
        public CapsuleCollider Collider { get; set; }
        public MeshRenderer MeshRenderer { get; set; }
        public Animator Animator { get; set; }
        public AnimatorParameters AnimatorParameters { get; set; }
        public UnitReputation UnitReputation { get; set; }
        public UnitResource UnitResource { get; set; }
        public UnitCharacteristics UnitCharacteristics { get; set; }
        public BaseCharacterClass CharacterClass { get; set; }
        public UnitLevel UnitLevel { get; set; }
        public UnitHealth UnitHealth { get; set; }
        public UnitVision UnitVision { get; set; }
        public UnitInventory UnitInventory { get; set; }
        public UnitPerson UnitPerson { get; set; }
        public UnitEquipment UnitEquipment { get; set; }
        public UnitBattle UnitBattle { get; set; }
        public UnitAbilities UnitAbilities{ get; set; }
        public event Action<InfoCollision> OnApplyDamageChange;
        
        public void OnCollision(InfoCollision info)
        {
            Dbg.Log($"{gameObject.name} Attacked");
            OnApplyDamageChange?.Invoke(info);
        }
    }
}