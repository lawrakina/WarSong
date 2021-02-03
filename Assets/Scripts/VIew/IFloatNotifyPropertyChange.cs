using System;


namespace Model
{
    public interface IFloatNotifyPropertyChange
    {
        float Value { get; set; }
        event Action<float> OnValueChange;
    }
}