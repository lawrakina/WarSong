using System;


namespace Models
{
    public class BattlePlayerModel
    {
        public float CurrentHp
        {
            get => _curHp;
            set
            {
                _curHp = value;
                ChangeCurrentHp?.Invoke(value);
            }
        }

        public float MaxHp
        {
            get => _maxHp;
            set
            {
                _maxHp = value;
                ChangeMaxHp?.Invoke(value);
            }
        }


        public float CurrentResource;
        public float MaxResource;

        public Action<float> ChangeMaxHp;
        public Action<float> ChangeCurrentHp;
        private float _curHp;
        private float _maxHp;
    }
}