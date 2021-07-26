using System;
using UnityEngine;


namespace Code.Data.Unit
{
    [Serializable] public class UnitCharacteristics : BasicCharacteristics
    {
        [SerializeField] [Range(0f,1f)]
        private float _minCritChance = 0.05f;
        [SerializeField] [Range(0f,1f)]
        private float _maxCritChance = 0.05f;
        [SerializeField] [Range(0f,1f)]
        private float _critChance = 0.1f;
        [SerializeField] [Range(0f,1f)]
        private float _minDodgeChance = 0.05f;
        [SerializeField] [Range(0f,1f)]
        private float _maxDodgeChance = 0.05f;
        [SerializeField] [Range(0f,1f)]
        private float _dodgeChance = 0.1f;

        public float DodgeChance
        {
            get
            {
                if (_dodgeChance > _maxDodgeChance)
                    return _maxDodgeChance;
                if (_dodgeChance < _minDodgeChance)
                    return _minDodgeChance;
                return _dodgeChance;
            }
            set => _dodgeChance = value;
        }

        public float CritChance
        {
            get
            {
                if (_critChance > _maxCritChance)
                    return _maxCritChance;
                if (_critChance < _minCritChance)
                    return _minCritChance;
                return _critChance;
            }
            set => _critChance = value;
        }
    }
}