﻿using System;
using Code.CharacterCustomizing;
using Code.Data;
using Code.Data.Unit;
using Code.Extension;
using UnityEngine;


namespace Code.Unit
{
    public sealed class PlayerView : MonoBehaviour, IPlayerView
    {
        public Transform Transform { get; }
        public Transform TransformModel { get; set; }
        public Collider Collider { get; set; }
        public Rigidbody Rigidbody { get; set; }
        public MeshRenderer MeshRenderer { get; set; }
        public Animator Animator { get; set; }
        public AnimatorParameters AnimatorParameters { get; set; }
        public UnitCharacteristics UnitCharacteristics { get; set; }
        public BaseCharacterClass CharacterClass { get; set; }
        public PersonCharacter PersonCharacter { get; set; }
        public UnitLevel UnitLevel { get; set; }
        public UnitEquipment UnitEquipment { get; set; }

        public event Action<InfoCollision> OnApplyDamageChange;
        
        public void OnCollision(InfoCollision info)
        {
            Dbg.Log($"{gameObject.name} Attacked");
            OnApplyDamageChange?.Invoke(info);
        }
    }
}