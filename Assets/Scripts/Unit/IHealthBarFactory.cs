using Data;


namespace Unit
{
    public interface IHealthBarFactory
    {
        HealthBarView CreateHealthBar(UiEnemySettings uiEnemySettings, IBaseUnitView owner);
    }
}