using Data;


namespace Unit.Enemies
{
    public interface IEnemyView : IBaseUnitView
    {
        BaseEnemyClass UnitClass { get; set; }
        void Init(EnemySettings item);
    }
}