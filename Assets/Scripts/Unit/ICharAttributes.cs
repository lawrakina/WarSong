using Enums;


namespace Unit
{
    public interface ICharAttributes
    {
        string Name { get; set; }
        CharacterGender CharacterGender { get; set; }
        CharacterRace CharacterRace { get; set; }
        float Speed { get; set; }
        float AgroDistance { get; set; }
        float RotateSpeedPlayer { get; set; }
    }
}