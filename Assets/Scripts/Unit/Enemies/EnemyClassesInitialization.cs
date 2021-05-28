using Data;
using Extension;


namespace Unit.Enemies
{
    public class EnemyClassesInitialization
    {
        public void Initialization(IEnemyView view, EnemyClassesData staticSettings, EnemySettings enemySettings)
        {
            // view.UnitClass = new SimplyEnemyClass();
            view.UnitVision = enemySettings.unitVisionComponent;
            view.UnitClass = new SimplyEnemyClass();
            view.UnitHealth = new UnitHealth
            {
                MaxHp = enemySettings.MaxHp, 
                CurrentHp = enemySettings.MaxHp
            };

            view.Attributes = enemySettings.unitAttributes;
            
            view.UnitBattle = enemySettings.unitBattle;

            view.UnitLevel = enemySettings.unitLevel;
            
            // view.UnitCHaracteristics = staticSettings.
            
            //ToDo сделать полноценную систему Свой-чужой
            view.Transform.gameObject.layer = LayerManager.EnemyLayer;
            view.UnitReputation = new UnitReputation();
            view.UnitReputation.EnemyLayer = LayerManager.PlayerLayer;
            view.UnitReputation.EnemyAttackLayer = LayerManager.PlayerAttackLayer;
            view.UnitReputation.FriendLayer = LayerManager.EnemyLayer;
            view.UnitReputation.FriendAttackLayer = LayerManager.EnemyAttackLayer;
        }
    }
}