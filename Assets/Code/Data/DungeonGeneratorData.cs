using System;
using System.Collections.Generic;
using Code.Data.Dungeon;
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

        [SerializeField]
        public List<DungeonParams> BdLevels;
    }

    [Serializable]
    public class DungeonParams
    {
        [SerializeField]
        private string _name = "Demo";
        [SerializeField]
        private DungeonParamsType _type = DungeonParamsType.Demo;

        public string Name => _name;
        public DungeonParamsType Type => _type;
    }
}