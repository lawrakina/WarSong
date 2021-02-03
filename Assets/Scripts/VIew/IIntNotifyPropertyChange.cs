using System;


namespace Model
{
    public interface IIntNotifyPropertyChange
    {
        int Value { get; set; }
        event Action<int> OnValueChange;
    }
}