using Data;
using UnityEngine;
using VIew;


namespace Unit
{
    public class HealthBarFactory : IHealthBarFactory
    {
        public HealthBarView CreateHealthBar(UiEnemySettings uiEnemySettings, IBaseUnitView owner)
        {
            var bar =
                Object.Instantiate(
                    uiEnemySettings.UiView,
                    owner.Transform.position + uiEnemySettings.Offset,
                    Quaternion.identity,
                    owner.Transform);
            var healthBarView = bar.GetComponent<HealthBarView>();
            return healthBarView;
        }
    }
}