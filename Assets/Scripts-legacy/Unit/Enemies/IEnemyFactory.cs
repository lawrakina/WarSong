using Data;


namespace Unit.Enemies
{
    public interface IEnemyFactory
    {
        IEnemyView CreateEnemy(EnemySettings item);
    }
}