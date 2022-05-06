using System;


namespace Code.Profile.Models{
    public class PlayerStatsModel{
        private int _curHp = 1;
        private int _maxHp = 1;
        private int _curRes = 1;
        private int _maxRes = 1;

        public Action<int> ChangeCurrentResource{ get; set; }
        public Action<int> ChangeMaxResource{ get; set; }
        public Action<int> ChangeMaxHp{ get; set; }
        public Action<int> ChangeCurrentHp{ get; set; }

        public int CurrentHp{
            get => _curHp;
            set{
                _curHp = value;
                ChangeCurrentHp?.Invoke(value);
            }
        }

        public int MaxHp{
            get => _maxHp;
            set{
                _maxHp = value;
                ChangeMaxHp?.Invoke(value);
            }
        }

        public int CurrentResource{
            get => _curRes;
            set{
                _curRes = value;
                ChangeCurrentResource?.Invoke(value);
            }
        }
        public int MaxResource{
            get => _maxRes;
            set{
                _maxRes = value;
                ChangeMaxResource?.Invoke(value);
            }
        }
    }
}