namespace Code.Unit
{
    public interface IEnemyView: IBaseUnitView
    {
        HealthBarView HealthBar { get; set; }
    }
}