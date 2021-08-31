using System;


namespace Code
{
    public interface IController
    {
        Guid Id { get; }
        bool IsOn { get; }
    }
}