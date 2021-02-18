using Data;
using VIew;


namespace Unit.Enemies
{
    public interface IEnemyView : IBaseUnitView
    {
        BaseEnemyClass UnitClass { get; set; }
        HealthBarView HealthBar { get; set; }
        void Init(EnemySettings item);
    }
}