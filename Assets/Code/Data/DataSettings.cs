using Code.Data.Unit;
using Code.Data.Unit.Player;


namespace Code.Data
{
    public struct DataSettings
    {
        public UiViewsData UiViews { get; set; }
        public CameraSettings CameraSettings { get; set; }
        public CharacterData CharacterData { get; set; }
        public UnitLevelData UnitLevelData { get; set; }
        public PlayerClassesData PlayerClassesData { get; set; }
        public PlayerData PlayerData { get; set; }
    }
}