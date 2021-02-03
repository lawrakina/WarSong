namespace Unit.Player
{
    public sealed class CharacterClassRogue : BaseCharacterClass
    {
        public override string Name => "Разбойник";

        public override string Description =>
            "Будучи умелыми убийцами и мастерами маскировки, они способны подкрасться к цели сзади, нанести смертельный удар и исчезнуть, прежде чем тело упадет на землю.";

        public override int Hp { get; }
        public override int MaxHp { get; }
    }

    public sealed class CharacterClassHunter : BaseCharacterClass
    {
        public override string Name => "Охотник";

        public override string Description =>
            "Охотники бьют врага на расстоянии или в ближнем бою, приказывая питомцам атаковать, пока сами натягивают тетиву, заряжают ружье или разят древковым оружием.";

        public override int Hp { get; }
        public override int MaxHp { get; }
    }

    public sealed class CharacterClassMage : BaseCharacterClass
    {
        public override string Name => "Маг";

        public override string Description =>
            "Маги уничтожают врагов тайными заклинаниями. Несмотря на магическую силу, маги хрупки, не носят тяжелых доспехов, поэтому уязвимы в ближнем бою. ";

        public override int Hp { get; }
        public override int MaxHp { get; }
    }
}