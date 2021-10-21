using System;
using UnityEngine;


namespace Code.Equipment{
    [Serializable] public sealed class AttackValue{
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

        public AttackValue(Vector2 attackValue, float attackDistance, float attackSpeed, float lagBeforyAttack){
            _minAttackValue = attackValue.x;
            _maxAttackValue = attackValue.y;
            _attackDistance = attackDistance;
            _attackSpeed = attackSpeed;
            _timeDelayBeforeAttack = lagBeforyAttack;
        }

        #endregion


        #region Properties

        public Vector2 GetAttack(){
            return new Vector2(_minAttackValue, _maxAttackValue);
        }

        public float GetAttackAvarage(){
            return (_minAttackValue + _maxAttackValue) / 2;
        }

        public float GetTimeLag(){
            return _timeDelayBeforeAttack;
        }

        public float GetAttackSpeed(){
            return _attackSpeed;
        }

        public float GetAttackDistance(){
            return _attackDistance;
        }

        #endregion
    }
}