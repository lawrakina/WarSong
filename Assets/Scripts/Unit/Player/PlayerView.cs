using CharacterCustomizing;
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
        public ICharAttributes CharAttributes { get; set; }
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

            CharAttributes = new CharAttributes();
            BasicCharacteristics = new BasicCharacteristics();
        }

        #endregion


        #region Events

        // public event Action<InfoCollision> OnBonusUp;

        #endregion


        #region Methods

        // public void OnCollision(InfoCollision infoCollision)
        // {
        //     OnBonusUp?.Invoke(infoCollision);
        // }

        #endregion
    }
}