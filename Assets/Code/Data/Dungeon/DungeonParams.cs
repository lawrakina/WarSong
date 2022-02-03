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
        [SerializeField]
        private float _maxTimeForReward = 120.0f;
        [SerializeField]
        private float _durationOfAggreState = 5.0f;
        
        public string Name => _name;
        public DungeonParamsType Type => _type;
        public float MaxTimeForReward => _maxTimeForReward;
        public float DurationOfAggreState => _durationOfAggreState;
    }
}