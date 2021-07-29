using System;


namespace Controller.Model
{
    public class BattlePlayerModel
    {
        public int CurrentHp
        {
            get => _curHp;
            set
            {
                _curHp = value;
                ChangeCurrentHp?.Invoke(value);
            }
        }

        public int MaxHp
        {
            get => _maxHp;
            set
            {
                _maxHp = value;
                ChangeMaxHp?.Invoke(value);
            }
        }


        public int CurrentResource;
        public int MaxResource;

        public Action<int> ChangeMaxHp;
        public Action<int> ChangeCurrentHp;
        private int _curHp = 1;
        private int _maxHp = 1;

        // private AbilityItem _ability1;
        // public AbilityItem AbilityItem1
        // {
        //     get => _ability1;
        //     set => _ability1 = value;
        // }
        // private AbilityItem _ability2;
        // public AbilityItem AbilityItem2
        // {
        //     get => _ability2;
        //     set => _ability2 = value;
        // }
        // private AbilityItem _ability3;
        // public AbilityItem AbilityItem3
        // {
        //     get => _ability3;
        //     set => _ability3 = value;
        // }
        //
        // public Action<AbilityItem> _spell1;
        // public Action<AbilityItem> _spell2;
        // public Action<AbilityItem> _spell3;
    }
}