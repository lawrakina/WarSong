using System;
using Code.Data.Unit.Player;
using UnityEngine;


namespace Code.Data.Unit{
    [Serializable] public sealed class CharacteristicsModifier{
        public CharacterClass Owner;
        public TargetParam TargetParam;
        [SerializeField]
        private string _formula = $"AssetsExternal/MathParser/Math parser Documentation.pdf";
        public KvpValueRatio Str;
        public KvpValueRatio Agi;
        public KvpValueRatio Int;
        /// <summary>
        /// Value - MaxLevel, Coeef - Ratio
        /// </summary>
        public float LvlRatio;

        public float Const1;
        public float Const2;
        public float Const3;

        [SerializeField]
        private ArithmeticOperation _typeOfModification = ArithmeticOperation.Addition;
        public string Formula{
            get => _formula;
            set => _formula = value;
        }
        public ArithmeticOperation TypeOfModification => _typeOfModification;
    }

    public enum ArithmeticOperation{
        Addition,
        Multiplication
    }

    public enum TargetParam{
        AttackModifier,
        SpeedAttackModifier,
        LagBeforeAttackModifier,
        DistanceModifier,
        CritChanceModifier,
        DodgeChanceModifier,
        ArmorModifier
    }
    
    [Serializable] public struct KvpValueRatio{
        public float V;
        public float R;
    }
    
    public sealed class ModificationOfObjectOfParam{
        private readonly float _value;
        private readonly ArithmeticOperation _modifierTypeOfModification;

        public ArithmeticOperation TypeOfOperation => _modifierTypeOfModification;
        public float Value => _value;

        public ModificationOfObjectOfParam(float value, ArithmeticOperation modifierTypeOfModification){
            _value = value;
            _modifierTypeOfModification = modifierTypeOfModification;
        }

        public override string ToString(){
            return $"TypeOfOperation:{_modifierTypeOfModification}; Value:{_value}";
        }
    }
}