using System;
using Battle;
using CharacterCustomizing;
using Code.Extension;
using Guirao.UltimateTextDamage;
using Necromancy.UI;
using UnityEngine;


namespace Unit.Player
{
    public sealed class PlayerView : MonoBehaviour, IPlayerView
    {
        #region Properties

        public Transform Transform => transform;
        public Collider Collider { get; set; }
        public Rigidbody Rigidbody { get; set; }
        public MeshRenderer MeshRenderer { get; set; }
        public UnitHealth UnitHealth { get; set; }
        public Animator Animator { get; set; }
        public AnimatorParameters AnimatorParameters { get; set; }
        public UnitAttributes Attributes { get; set; }
        public UnitVision UnitVision { get; set; }
        public UnitPlayerBattle UnitPlayerBattle { get; set; }
        public UltimateTextDamageManager UiTextManager { get; set; }
        public UnitReputation UnitReputation { get; set; }
        public UnitAbilities UnitAbilities { get; set; }
        public UnitLevel UnitLevel { get; set; }
        public UnitCharacteristics UnitCharacteristics { get; set; }
        public BaseCharacterClass CharacterClass { get; set; }
        public EquipmentPoints EquipmentPoints { get; set; }
        public EquipmentItems EquipmentItems { get; set; }
        public Transform TransformModel { get; set; }
        public UnitModifier Modifier { get; set; }

        #endregion

        public event Action<InfoCollision> OnApplyDamageChange;
        

        public void OnCollision(InfoCollision info)
        {
            Dbg.Log($"{gameObject.name} Attacked");
            OnApplyDamageChange?.Invoke(info);
        }
    }
}