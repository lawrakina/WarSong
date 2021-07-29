using Code.Data;
using Code.Data.Dungeon;
using UnityEngine;


namespace Code.Profile
{
    [CreateAssetMenu(fileName = nameof(DungeonGeneratorModel), menuName = "Models/" + nameof(DungeonGeneratorModel))]
    public class DungeonGeneratorModel : ScriptableObject
    {
        [SerializeField]
        private DungeonParams _activeLevel;
        public DungeonParams ActiveLevel
        {
            get => _activeLevel;
            set => _activeLevel = value;
        }
    }
}