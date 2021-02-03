using System;


namespace Model
{
    public class FloatNotifyPropertyChange : IFloatNotifyPropertyChange
    {
        #region Fields

        private float _value;

        #endregion


        #region ClassLiveCycles

        public FloatNotifyPropertyChange(float value)
        {
            _value = value;
        }

        #endregion


        public float Value
        {
            get => _value;
            set
            {
                _value = value;
                OnValueChange.Invoke(value);
            }
        }

        public event Action<float> OnValueChange = delegate { };
    }
}