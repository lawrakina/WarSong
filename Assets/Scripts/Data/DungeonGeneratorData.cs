using UnityEngine;


namespace Data
{
    [CreateAssetMenu(fileName = "DungeonData", menuName = "Data/Dungeon Data")]
    public class DungeonGeneratorData : ScriptableObject
    {
        [SerializeField]
        public GameObject StorageGenerator;

        [SerializeField]
        public GameObject StorageNavMash;
    }
}