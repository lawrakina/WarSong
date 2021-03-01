using System;
using UnityEngine;
using Random = UnityEngine.Random;


namespace Weapons
{
    [Serializable] public sealed class AttackValue
    {
        [SerializeField]
        private float _maxAttackValue;

        [SerializeField]
        private float _minAttackValue;

        [SerializeField]
        private float _timeDelayBeforeAttack = 0.5f;

        public float GetAttack()
        {
            return Random.Range(_minAttackValue, _maxAttackValue);
        }

        public float GetTimeLag()
        {
            return _timeDelayBeforeAttack;
        }
    }
}