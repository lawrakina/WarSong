namespace Unit
{
    public class UnitHealth
    {
        private float _currentHp;

        public float CurrentHp
        {
            get => _currentHp;
            set
            {
                // Dbg.Log($"--------------------- value:{value}");
                // _currentHp = (value+0.8f) > MaxHp ? MaxHp : value;
                _currentHp = value > MaxHp ? MaxHp : value;
            } }
        public float MaxHp { get; set; }

        public UnitHealth(float hp = 0)
        {
            MaxHp = hp;
            _currentHp = hp;
        }
        
        // todo add new class //UnitHealth
        // {
        //     CurrentHp
        //         MaxHp
        //     ChangeCurrentHp
        //         ChangeMaxHp
        //     CurrentResource
        //         MaxResource
        //     ChangeCurrentResource
        //         ChangeMaxResource
        //     List<Buff> - все положительные эффекты
        //     List<Debuff> - все отрицательные эффекты
        // }
    }
}