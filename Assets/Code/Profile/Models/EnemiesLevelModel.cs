using System;
using System.Collections.Generic;
using Code.Extension;
using Code.Unit;
using Unit;
using Unit.Enemies;
using UnityEngine;
using AnimatorParameters = Code.Unit.AnimatorParameters;
using HealthBarView = Code.Unit.HealthBarView;
using UnitHealth = Code.Data.Unit.UnitHealth;
using UnitLevel = Code.Data.Unit.UnitLevel;
using UnitVision = Code.Data.Unit.UnitVision;


namespace Code.Profile.Models
{
    [CreateAssetMenu(fileName = nameof(EnemiesLevelModel), menuName = "Models/" + nameof(EnemiesLevelModel))]
    public class EnemiesLevelModel : ScriptableObject
    {
        [SerializeField] private List<EnemyView> _enemies;
        public List<EnemyView> Enemies { get; set; }
    }
    
    public class EnemyView : MonoBehaviour
    {
        #region Properties

        public Transform Transform { get; set; }
        public Transform TransformModel { get; set; }
        public Collider Collider { get; set; }
        public Rigidbody Rigidbody { get; set; }
        public MeshRenderer MeshRenderer { get; set; }
        public UnitHealth UnitHealth { get; set; }
        public Animator Animator { get; set; }
        public AnimatorParameters AnimatorParameters { get; set; }
        public UnitAttributes Attributes { get; set; }
        public UnitVision UnitVision { get; set; }
        public UnitEnemyBattle UnitBattle { get; set; }
        public UnitReputation UnitReputation { get; set; }
        public UnitLevel UnitLevel { get; set; }
        public UnitAbilities UnitAbilities { get; set; }
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