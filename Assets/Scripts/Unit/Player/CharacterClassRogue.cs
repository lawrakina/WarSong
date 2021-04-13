using Enums;


namespace Unit.Player
{
    public sealed class CharacterClassWarrior : BaseCharacterClass
    {
        public override CharacterClass Class => CharacterClass.Warrior;
        public override string Name => "Воин";
        public override string Description =>
            "Воины - класс, сосредоточенный на оружии ближнего боя. Бьет по нескольким противникам. Они сильные и выносливые, а так же мастера оружия и тактики.";
        public override ResourceEnum ResourceType => ResourceEnum.Rage;
    }

    public sealed class CharacterClassRogue : BaseCharacterClass
    {
        public override CharacterClass Class => CharacterClass.Rogue;
        public override string Name => "Разбойник";

        public override string Description =>
            "Будучи умелыми убийцами и мастерами маскировки, они способны подкрасться к цели сзади, нанести смертельный удар и исчезнуть, прежде чем тело упадет на землю.";

        public override ResourceEnum ResourceType => ResourceEnum.Energy;
    }

    public sealed class CharacterClassHunter : BaseCharacterClass
    {
        public override CharacterClass Class => CharacterClass.Hunter;
        public override string Name => "Охотник";

        public override string Description =>
            "Охотники бьют врага на расстоянии, приказывая питомцам атаковать. Пока сами натягивают тетиву или заряжают ружье.";

        public override ResourceEnum ResourceType => ResourceEnum.Concentration;
    }

    public sealed class CharacterClassMage : BaseCharacterClass
    {
        public override CharacterClass Class => CharacterClass.Mage;
        public override string Name => "Маг";

        public override string Description =>
            "Маги уничтожают врагов тайными заклинаниями. Несмотря на магическую силу, маги хрупки, не носят тяжелых доспехов, поэтому уязвимы в ближнем бою. ";

        public override ResourceEnum ResourceType => ResourceEnum.Mana;
    }
}