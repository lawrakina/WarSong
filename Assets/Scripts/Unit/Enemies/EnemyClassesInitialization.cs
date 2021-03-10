using Data;
using Extension;


namespace Unit.Enemies
{
    public class EnemyClassesInitialization
    {
        // private readonly EnemyClassesData _data;
        private readonly UnitLevel _currentPlayerLevel;

        public EnemyClassesInitialization(/*EnemyClassesData data,*/ UnitLevel currentPlayerLevel)
        {
            // _data = data;
            _currentPlayerLevel = currentPlayerLevel;
        }

        public void Initialization(IEnemyView view, EnemySettings enemySettings)
        {
            // view.UnitClass = new SimplyEnemyClass();
            view.UnitVision = enemySettings.unitVisionComponent;
            view.UnitClass = new SimplyEnemyClass();
            view.MaxHp = enemySettings.MaxHp;
            view.CurrentHp = enemySettings.MaxHp;

            view.Attributes = enemySettings.unitAttributes;
            
            view.UnitBattle = enemySettings.unitBattle;
            
            
            //ToDo сделать полноценную систему Свой-чужой
            view.Transform.gameObject.layer = LayerManager.EnemyLayer;
            view.UnitReputation = new UnitReputation();
            view.UnitReputation.EnemyLayer = LayerManager.PlayerLayer;
            view.UnitReputation.EnemyAttackLayer = LayerManager.PlayerAttackLayer;
            view.UnitReputation.FriendLayer = LayerManager.EnemyLayer;
            view.UnitReputation.FriendAttackLayer = LayerManager.EnemyAttackLayer;

            Dbg.Log($"view.UnitReputation.EnemyLayer:{view.UnitReputation.EnemyLayer})");
        }
    }
}