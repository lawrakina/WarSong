using System;


namespace Code
{
    public interface IController
    {
        Guid Id { get; }
        void OnExecute();
        void OffExecute();
        bool IsOn { get; }
    }
}