using System.Collections.Generic;
using UnityEngine;


namespace Code.Data.Dungeon
{
    [CreateAssetMenu(fileName = nameof(DungeonGeneratorData), menuName = "Configs/" + nameof(DungeonGeneratorData))]
    public class DungeonGeneratorData : ScriptableObject
    {
        [SerializeField]
        public GameObject StorageGenerator;

        [SerializeField]
        public GameObject StorageNavMash;

        [SerializeField]
        public List<DungeonParams> BdLevels;
    }
}