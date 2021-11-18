namespace Code.Data.Unit{
    public class Attack{
        private readonly float _attackValue;
        private readonly DamageType _type;
        public float Damage => _attackValue;

        public Attack(float attackValue, DamageType type){
            _attackValue = attackValue;
            _type = type;
        }
    }
}