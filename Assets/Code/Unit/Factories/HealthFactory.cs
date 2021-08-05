using Code.Data.Unit;
using Code.Data.Unit.Player;


namespace Code.Unit.Factories
{
    public sealed class HealthFactory
    {
        private readonly PlayerClassesData _settings;

        public HealthFactory(PlayerClassesData settings)
        {
            _settings = settings;
        }

        public UnitHealth GenerateHealth(UnitHealth health, UnitCharacteristics characteristics)
        {
            if(health == null)
                health = new UnitHealth(characteristics.Values.Stamina * _settings.HealthPerStamina);
            health.MaxHp = characteristics.Values.Stamina * _settings.HealthPerStamina;

            return health;
        }
    }
}