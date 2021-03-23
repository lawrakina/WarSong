using System;
using Battle;
using Extension;
using UnityEngine;
using VIew;


namespace Unit.Enemies
{
    public sealed class EnemyView : MonoBehaviour, IEnemyView
    {
        #region Properties

        public Transform Transform { get; set; }
        public Transform TransformModel { get; set; }
        public Collider Collider { get; set; }
        public Rigidbody Rigidbody { get; set; }
        public MeshRenderer MeshRenderer { get; set; }
        public Animator Animator { get; set; }
        public AnimatorParameters AnimatorParameters { get; set; }
        public UnitAttributes Attributes { get; set; }
        public UnitVision UnitVision { get; set; }
        public UnitEnemyBattle UnitBattle { get; set; }
        public UnitReputation UnitReputation { get; set; }
        public UnitLevel UnitLevel { get; set; }
        public float CurrentHp { get; set; }
        public float BaseHp { get; set; }
        public float MaxHp { get; set; }
        public event Action<InfoCollision> OnApplyDamageChange;
        public BaseEnemyClass UnitClass { get; set; }
        public HealthBarView HealthBar { get; set; }

        #endregion

        
        public void OnCollision(InfoCollision info)
        {
            Dbg.Log($"I`m Attacked :{gameObject.name}");
            OnApplyDamageChange?.Invoke(info);
        }
    }
}