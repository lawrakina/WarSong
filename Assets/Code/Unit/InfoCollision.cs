using Leopotam.Ecs;


namespace Code.Unit
{
    public struct InfoCollision
    {
        #region Fields

        private readonly float _damage;
        public EcsEntity _attacker;

        // private readonly ContactPoint _contact;
        // private readonly Transform _objCollision;
        // private readonly Vector3 _direction;

        #endregion


        public InfoCollision(float damage, EcsEntity attacker)
        {
            _damage = damage;
            _attacker = attacker;
        }
        // public InfoCollision(float damage, ContactPoint contact, Transform objCollision, Vector3 direction = default)
        // {
        //     _damage = damage;
        //     _direction = direction;
        //     _contact = contact;
        //     _objCollision = objCollision;
        // }


        #region Properties

        // public Vector3 Direction => _direction;

        public float Damage => _damage;

        // public ContactPoint Contact => _contact;
        //
        // public Transform ObjCollision => _objCollision;

        #endregion
    }
}