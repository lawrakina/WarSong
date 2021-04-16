using System;


namespace Models
{
    public class BattleProgressModel
    {
        public Action<int> ChangeMaxTimer;
        public Action<float> ChangeCurrentTimer;
        public Action<int> ChangeMaxEnemy;
        public Action<int> ChangeCurrentEnemy;
        public Action<int> ChangeMaxBag;
        public Action<int> ChangeCurrentBag;
        public Action<int> ChangeMaxRareEnemy;
        public Action<int> ChangeCurrentRareEnemy;
        private int _maxTimer = 0;
        private float _currentTimer = 0.0f;
        private int _maxEnemy = 0;
        private int _currentEnemy = 0;
        private int _maxBag = 0;
        private int _currentBag = 0;
        private int _maxRareEnemy = 0;
        private int _currentRareEnemy = 0;

        public int MaxEnemy
        {
            get => _maxEnemy;
            set
            {
                _maxEnemy = value;
                ChangeMaxEnemy?.Invoke(value);
            }
        }

        public int CurrentEnemy
        {
            get => _currentEnemy;
            set
            {
                _currentEnemy = value;
                ChangeCurrentEnemy?.Invoke(value);
            }
        }

        public float CurrentTimer
        {
            get => _currentTimer;
            set
            {
                _currentTimer = value;
                ChangeCurrentTimer?.Invoke(value);
            }
        }

        public int MaxTimer
        {
            get => _maxTimer;
            set
            {
                _maxTimer = value;
                ChangeMaxTimer?.Invoke(value);
            }
        }

        public int MaxBag
        {
            get => _maxBag;
            set
            {
                _maxBag = value;
                ChangeMaxBag?.Invoke(value);
            } }
        public int CurrentBag
        {
            get => _currentBag;
            set
            {
                _currentBag = value;
                ChangeCurrentBag?.Invoke(value);
            }
        }

        public int MaxRareEnemy
        {
            get => _maxRareEnemy;
            set
            {
                _maxRareEnemy = value;
                ChangeMaxRareEnemy?.Invoke(value);
            } }
        public int CurrentRareEnemy
        {
            get => _currentRareEnemy;
            set
            {
                _currentRareEnemy = value;
                ChangeCurrentRareEnemy?.Invoke(value);
            } }
    }
}