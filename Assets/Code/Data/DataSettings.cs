using Code.Data.Unit.Player;
using CharacterData = Code.Data.Unit.CharacterData;
using DungeonGeneratorData = Code.Data.Dungeon.DungeonGeneratorData;
using PlayerClassesData = Code.Data.Unit.Player.PlayerClassesData;
using PlayerData = Code.Data.Unit.Player.PlayerData;
using EnemiesData = Data; 


namespace Code.Data
{
    public struct DataSettings
    {
        public DungeonGeneratorData DungeonGeneratorData { get; set; }
        public UiViewsData UiViews { get; set; }
        public CameraSettings CameraSettings { get; set; }
        public CharacterData CharacterData { get; set; }
        public UnitLevelData UnitLevelData { get; set; }
        public PlayerClassesData PlayerClassesData { get; set; }
        public PlayerData PlayerData { get; set; }
        public EcsBattleData EcsBattleData { get; set; }
        
        public EnemiesData EnemiesData { get; set; }
    }
}