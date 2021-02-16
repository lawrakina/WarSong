namespace Unit.Enemies
{
    public sealed class SimplyEnemyClass : BaseEnemyClass
    {
        public override string Name => $"SimplyEnemy";
        public override string Description => $"Start and tests enemy";
        public float CurrentHp { get; set; }
        public float MaxHp { get; set; }

        public SimplyEnemyClass()
        {
            
        }
    }
}