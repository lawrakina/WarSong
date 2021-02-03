using Enums;


namespace Unit
{
    public sealed class CharAttributes : ICharAttributes
    {
        public string Name { get; set; }
        public CharacterGender CharacterGender { get; set; }
        public CharacterRace CharacterRace { get; set; }
        public float Speed { get; set; }
        public float AgroDistance { get; set; }
        public float RotateSpeedPlayer { get; set; }
    }
}