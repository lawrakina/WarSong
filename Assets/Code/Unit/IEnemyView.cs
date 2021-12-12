using Pathfinding;


namespace Code.Unit
{
    public interface IEnemyView: IBaseUnitView
    {
        AIPath AIPath { get; set; }
        AIDestinationSetter AIDestinationSetter { get; set; }
        HealthBarView HealthBar { get; set; }
    }
}