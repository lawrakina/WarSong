using System;
using Battle;
using CharacterCustomizing;
using Extension;
using UnityEngine;


namespace Unit.Player
{
    public sealed class PlayerView : MonoBehaviour, IPlayerView
    {
        #region Properties

        public Transform Transform { get; set; }
        public Collider Collider { get; set; }
        public Rigidbody Rigidbody { get; set; }
        public MeshRenderer MeshRenderer { get; set; }
        public Animator Animator { get; set; }
        public AnimatorParameters AnimatorParameters { get; set; }
        public UnitAttributes Attributes { get; set; }
        public UnitVision UnitVision { get; set; }
        public UnitPlayerBattle UnitPlayerBattle { get; set; }
        public UnitReputation UnitReputation { get; set; }
        public float CurrentHp { get; set; }
        public float BaseHp { get; set; }
        public float MaxHp { get; set; }
        public event Action<InfoCollision> OnApplyDamageChange;
        public UnitLevel UnitLevel { get; set; }
        public BasicCharacteristics BasicCharacteristics { get; set; }
        public BaseCharacterClass CharacterClass { get; set; }
        public EquipmentPoints EquipmentPoints { get; set; }
        public EquipmentItems EquipmentItems { get; set; }
        public Transform TransformModel { get; set; }

        #endregion
        
        
        public void OnCollision(InfoCollision info)
        {
            Dbg.Log($"{gameObject.name} Attacked");
            OnApplyDamageChange?.Invoke(info);
        }
    }
}