namespace Unit.Player
{
    public sealed class CharacterClassWarrior : BaseCharacterClass
    {
        public override string Name => "Воин";

        public override string Description =>
            "Воины - класс, сосредоточенный на оружии ближнего боя. Они сильные и выносливые, а так же мастера оружия и тактики. Специальные способности воина ориентированы на бой";

        public override int Hp { get; }
        public override int MaxHp { get; }
    }
}