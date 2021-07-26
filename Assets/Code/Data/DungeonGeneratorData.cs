using UnityEngine;


namespace Code.Data
{
    [CreateAssetMenu(fileName = "Config_DungeonData", menuName = "Data/Config Dungeon Data")]
    public class DungeonGeneratorData : ScriptableObject
    {
        [SerializeField]
        public GameObject StorageGenerator;

        [SerializeField]
        public GameObject StorageNavMash;
    }
}