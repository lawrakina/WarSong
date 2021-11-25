namespace Code.Data.Unit{
    public class UnitHealth{
        private float _currentHp;
        private float _currentResource;

        public float CurrentHp{
            get => _currentHp;
            set{ _currentHp = value > MaxHp ? MaxHp : value; }
        }

        public float MaxHp{ get; set; }

        public UnitHealth(float hp = 0){
            MaxHp = hp;
            _currentHp = hp;
        }

        public override string ToString(){
            return $"Health points: {CurrentHp}/{MaxHp}";
        }
    }
}