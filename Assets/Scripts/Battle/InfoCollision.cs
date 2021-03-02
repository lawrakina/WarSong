namespace Battle
{
    public struct InfoCollision
    {
        #region Fields

        private readonly float _damage;

        // private readonly ContactPoint _contact;
        // private readonly Transform _objCollision;
        // private readonly Vector3 _direction;

        #endregion


        public InfoCollision(float damage, float timeDelay)
        {
            _damage = damage;
            MaxTime = timeDelay;
            CurrentTime = 0;
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
        public float MaxTime { get; set; }
        public float CurrentTime { get; set; }

        // public ContactPoint Contact => _contact;
        //
        // public Transform ObjCollision => _objCollision;

        #endregion
    }
}