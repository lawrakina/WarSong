using System;


namespace Code.Fight.BuildingDungeon
{
    public interface IVerifiable
    {
        BuildStatus Status { get; set; }
        event Action<IVerifiable> Complete;
    }
}