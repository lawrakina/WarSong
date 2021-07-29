using System;
using UnityEngine;


namespace Code.Data.Dungeon
{
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