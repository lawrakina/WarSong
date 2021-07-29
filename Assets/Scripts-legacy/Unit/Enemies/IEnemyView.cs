namespace Unit.Enemies
{
    public interface IEnemyView : IBaseUnitView
    {
        BaseEnemyClass UnitClass { get; set; }
        HealthBarView HealthBar { get; set; }
        UnitEnemyBattle UnitBattle { get; set; }
    }
}