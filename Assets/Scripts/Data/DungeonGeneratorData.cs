using UnityEngine;


namespace Data
{
    [CreateAssetMenu(fileName = "DungeonData", menuName = "Data/DungeonData")]
    public class DungeonGeneratorData : ScriptableObject
    {
        [SerializeField]
        public GameObject StorageGenerator;
    }
}