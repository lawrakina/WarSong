using System;
using UnityEngine;
using Random = UnityEngine.Random;


namespace Code.Equipment
{
    [Serializable] public sealed class AttackValue
    {
        #region Fields

        [SerializeField]
        private float _maxAttackValue = 1.0f;

        [SerializeField]
        private float _minAttackValue = 2.0f;

        [SerializeField]
        private float _timeDelayBeforeAttack = 0.5f;

        [SerializeField]
        private float _attackSpeed = 2.0f;

        [SerializeField] 
        private float _attackDistance = 1.0f;
        
        public AttackValue(float min, float max, float attackDistance)
        {
            _minAttackValue = min;
            _maxAttackValue = max;
            _attackDistance = attackDistance;
        }

        #endregion


        #region Properties

        public float GetAttack()
        {
            return Random.Range(_minAttackValue, _maxAttackValue);
        }

        public float GetTimeLag()
        {
            return _timeDelayBeforeAttack;
        }

        public float GetAttackSpeed()
        {
            return _attackSpeed;
        }

        public float GetAttackDistance()
        {
            return _attackDistance;
        }

        #endregion
    }
}