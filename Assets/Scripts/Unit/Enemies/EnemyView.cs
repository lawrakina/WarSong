using Data;
using Unit.Player;
using UnityEngine;


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
        public BaseEnemyClass UnitClass { get; set; }

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

            CharAttributes = new CharAttributes();
        }

        #endregion


        public void Init(EnemySettings item)
        {
            UnitClass = new SimplyEnemyClass();
            
        }
    }
}