namespace Code.Data.Unit{
    public class Attack{
        public float Damage{ get; }
        public DamageType DamageType{ get; }

        public Attack(float attackValue, DamageType type){
            Damage = attackValue;
            DamageType = type;
        }
    }
}