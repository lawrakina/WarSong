using System;
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
        private float _critAttackMultiplier = 2.0f;

        [SerializeField]
        [Range(0f, 1f)]
        private float _minDodgeChance = 0.05f;
        [SerializeField]
        [Range(0f, 1f)]
        private float _maxDodgeChance = 0.05f;

        private int _minAttack;
        private int _maxAttack;
        private float _distance;

        [SerializeField]
        private float _minArmorValue = 0.0f;
        [SerializeField]
        private float _maxArmorValue = 0.75f;

        public float DodgeChance{
            get{
                if (Values.DodgeChance > _maxDodgeChance)
                    return _maxDodgeChance;
                if (Values.DodgeChance < _minDodgeChance)
                    return _minDodgeChance;
                return Values.DodgeChance;
            }
            set => Values.DodgeChance = value;
        }

        public float CritChance{
            get{
                if (Values.CritChance > _maxCritChance)
                    return _maxCritChance;
                if (Values.CritChance < _minCritChance)
                    return _minCritChance;
                return Values.CritChance;
            }
            set => Values.CritChance = value;
        }
        public float CritAttackMultiplier => _critAttackMultiplier;

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
        public ModificationOfObjectOfParam AttackModifier{ get; set; }
        public ModificationOfObjectOfParam SpeedAttackModifier{ get; set; }
        public ModificationOfObjectOfParam LagBeforeAttackModifier{ get; set; }
        public ModificationOfObjectOfParam DistanceModifier{ get; set; }
        public ModificationOfObjectOfParam CritChanceModifier{ get; set; }
        public ModificationOfObjectOfParam DodgeChanceModifier{ get; set; }
        public ModificationOfObjectOfParam ArmorModifier{ get; set; }
    }
}