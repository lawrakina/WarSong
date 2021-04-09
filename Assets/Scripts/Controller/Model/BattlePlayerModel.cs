﻿using System;


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
    }
}