using Data;
using VIew;


namespace Unit
{
    public interface IHealthBarFactory
    {
        HealthBarView CreateHealthBar(UiEnemySettings uiEnemySettings, IBaseUnitView owner);
    }
}