using System;
using Battle;
using CharacterCustomizing;
using Extension;
using UnityEngine;


namespace Unit.Player
{
    public sealed class PlayerView : MonoBehaviour, IPlayerView
    {
        #region Fields

        private Animator _animator;

        #endregion


        #region Properties

        public Transform Transform { get; private set; }
        public Collider Collider { get; private set; }
        public Rigidbody Rigidbody { get; private set; }
        public MeshRenderer MeshRenderer { get; private set; }
        public Animator Animator => _animator;
        public AnimatorParameters AnimatorParameters { get; private set; }
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

        #endregion


        #region UnityMethods

        private void Awake()
        {
            Transform = GetComponent<Transform>();
            Rigidbody = GetComponent<Rigidbody>();
            Collider = GetComponent<Collider>();
            MeshRenderer = GetComponent<MeshRenderer>();
            _animator = GetComponent<Animator>();
            AnimatorParameters = new AnimatorParameters(ref _animator);
        }
        //
        // private void OnEnable()
        // {
        //     OnApplyDamageChange += SetDamage;
        // }
        //
        // private void SetDamage(InfoCollision collision)
        // {
        //     Dbg.Log($"{gameObject.name} Attacked");
        //     OnApplyDamageChange?.Invoke(collision);
        // }
        //
        // private void OnDisable()
        // {
        //     OnApplyDamageChange -= SetDamage;
        // }
        
        #endregion

    
        public void OnCollision(InfoCollision info)
        {
            Dbg.Log($"{gameObject.name} Attacked");
            OnApplyDamageChange?.Invoke(info);
        }
    }
}