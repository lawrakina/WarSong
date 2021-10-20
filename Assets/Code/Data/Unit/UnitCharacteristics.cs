using System;
using Code.Equipment;
using UnityEngine;


namespace Code.Data.Unit{
    [Serializable] public class UnitCharacteristics : BasicCharacteristics{
        [SerializeField]
        public float Speed = 8.0f;
        [SerializeField]
        public float RotateSpeedPlayer = 120.0f;
        [SerializeField]
        [Range(0f, 1f)]
        private float _minCritChance = 0.05f;
        [SerializeField]
        [Range(0f, 1f)]
        private float _maxCritChance = 0.05f;
        [SerializeField]
        [Range(0f, 1f)]
        private float _critChance = 0.1f;

        [SerializeField]
        [Range(0f, 1f)]
        private float _minDodgeChance = 0.05f;
        [SerializeField]
        [Range(0f, 1f)]
        private float _maxDodgeChance = 0.05f;
        [SerializeField]
        [Range(0f, 1f)]
        private float _dodgeChance = 0.1f;

        private int _minAttack;
        private int _maxAttack;
        private float _distance;

        [SerializeField]
        private float _minArmorValue = 0.0f;
        [SerializeField]
        private float _maxArmorValue = 0.75f;

        public float DodgeChance{
            get{
                if (_dodgeChance > _maxDodgeChance)
                    return _maxDodgeChance;
                if (_dodgeChance < _minDodgeChance)
                    return _minDodgeChance;
                return _dodgeChance;
            }
            set => _dodgeChance = value;
        }

        public float CritChance{
            get{
                if (_critChance > _maxCritChance)
                    return _maxCritChance;
                if (_critChance < _minCritChance)
                    return _minCritChance;
                return _critChance;
            }
            set => _critChance = value;
        }

        public int MinAttack{
            get => _minAttack;
            set => _minAttack = value;
        }

        public int MaxAttack{
            get => _maxAttack;
            set => _maxAttack = value;
        }

        public float ArmorValue{
            get{
                var result = 0.1f;
                //ToDo нужен рассчет показателя брони в процентах 

                if (result > _maxArmorValue)
                    result = _maxArmorValue;
                if (result < _minArmorValue)
                    result = _minArmorValue;
                return result;
            }
        }

        public float Distance{
            get => _distance;
            set => _distance = value;
        }
        public float AttackModifier{ get; set; }
        public float SpeedAttackModifier{ get; set; }
        public float LagBeforeAttackModifier{ get; set; }
        public float DistanceModifier{ get; set; }
        public float CritChanceModifier{ get; set; }
        public float DodgeChanceModifier{ get; set; }
        public float ArmorModifier{ get; set; }
    }
}