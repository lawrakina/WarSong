using System;


namespace Model
{
    public class IntNotifyPropertyChange : IIntNotifyPropertyChange
    {
        #region Fields

        private int _value;

        #endregion


        #region ClassLiveCycles

        protected IntNotifyPropertyChange(int countCoins)
        {
            _value = countCoins;
        }

        #endregion


        public int Value
        {
            get => _value;
            set
            {
                _value = value;
                OnValueChange.Invoke(value);
            }
        }

        public event Action<int> OnValueChange = delegate { };
    }
}