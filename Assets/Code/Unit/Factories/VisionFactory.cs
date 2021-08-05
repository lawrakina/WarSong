using Code.Data.Unit;
using Code.Data.Unit.Player;


namespace Code.Unit.Factories
{
    public sealed class VisionFactory
    {
        private readonly PlayerClassesData _settings;

        public VisionFactory(PlayerClassesData settings)
        {
            _settings = settings;
        }

        public UnitVision GenerateVision()
        {
            return _settings.UnitVision;
        }
    }
}