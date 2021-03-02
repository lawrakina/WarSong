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
        public UnitBattle UnitBattle { get; set; }
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

            // UnitBattle = new UnitBattle();
            // UnitVision = new UnitVision();
            // UnitReputation = new UnitReputation();
            // UnitLevel = new UnitLevel();
            // Attributes = new UnitAttributes();
            // BasicCharacteristics = new BasicCharacteristics();
        }

        private void OnEnable()
        {
            OnApplyDamageChange += SetDamage;
        }

        private void SetDamage(InfoCollision obj)
        {
            Dbg.Log($"I`m Attacked");
        }

        private void OnDisable()
        {
            OnApplyDamageChange -= SetDamage;
        }
        
        #endregion


        public void OnCollision(InfoCollision info)
        {
            OnApplyDamageChange?.Invoke(info);
        }
    }
}