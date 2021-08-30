using Code.Data.Unit;
using Code.Extension;


namespace Code.Unit.Factories
{
    public sealed class ReputationFactory
    {
        public UnitReputation GeneratePlayerReputation()
        {
            var result = new UnitReputation();
            result.FriendLayer = LayerManager.PlayerLayer;
            result.EnemyLayer = LayerManager.EnemyLayer;
            result.FriendAttackLayer = LayerManager.PlayerAttackLayer;
            result.EnemyAttackLayer = LayerManager.EnemyAttackLayer;
            return result;
        }

        public UnitReputation GenerateEnemyReputation()
        {
            var result = new UnitReputation();
            result.FriendLayer = LayerManager.EnemyLayer;
            result.EnemyLayer = LayerManager.PlayerLayer;
            result.FriendAttackLayer = LayerManager.EnemyAttackLayer;
            result.EnemyAttackLayer = LayerManager.PlayerAttackLayer;
            return result;
        }
    }
}