using Code.Data.Unit;
using Code.Fight.EcsFight.Settings;
using Leopotam.Ecs;


namespace Code.Unit
{
    public struct InfoCollision
    {
        private readonly Attack _attack;
        private readonly EcsEntity _attacker;


        #region Fields

        // private readonly float _damage;
        // public EcsEntity _attacker;

        // private readonly ContactPoint _contact;
        // private readonly Transform _objCollision;
        // private readonly Vector3 _direction;

        #endregion


        //ToDo Need remove after removing EcsBattle
        public InfoCollision(float damage, EcsEntity attacker){
            _attack = new Attack(damage, DamageType.Default);
            _attacker = attacker;
        }

        public InfoCollision(Attack attack, EcsEntity attacker){
            _attack = attack;
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

        public float Damage => _attack.Damage;
        public DamageType DamageType => _attack.DamageType;
        public EcsEntity Attacker => _attacker;

        // public ContactPoint Contact => _contact;
        //
        // public Transform ObjCollision => _objCollision;

        #endregion
    }
}