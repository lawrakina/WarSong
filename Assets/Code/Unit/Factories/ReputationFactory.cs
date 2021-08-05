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
    }
}