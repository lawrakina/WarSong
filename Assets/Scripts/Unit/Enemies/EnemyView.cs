﻿using Data;
using UnityEngine;
using VIew;


namespace Unit.Enemies
{
    public sealed class EnemyView : MonoBehaviour, IEnemyView
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
        public ICharAttributes CharAttributes { get; set; }
        public UnitVision UnitVision { get; set; }
        public UnitBattle UnitBattle { get; set; }
        public float CurrentHp { get; set; }
        public float BaseHp { get; set; }
        public float MaxHp { get; set; }
        public BaseEnemyClass UnitClass { get; set; }
        public HealthBarView HealthBar { get; set; }

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

            UnitBattle = new UnitBattle();
            UnitVision = new UnitVision();
            CharAttributes = new CharAttributes();
        }

        #endregion


        public void Init(EnemySettings item)
        {
            UnitClass = new SimplyEnemyClass();
            UnitVision = item.unitVisionComponent;
        }
    }
}