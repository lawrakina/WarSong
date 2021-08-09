using System;
using UnityEngine;


namespace Code.Data.Unit.Player
{
    [Serializable]
    public class LevelValue
    {
        [SerializeField]
        private int _maxPointsExperience;
        [SerializeField]
        private int _number;

        public int Number
        {
            get => _number;
            set => _number = value;
        }

        public int MaxiPointsExperience
        {
            get => _maxPointsExperience;
            set => _maxPointsExperience = value;
        }
    }
}