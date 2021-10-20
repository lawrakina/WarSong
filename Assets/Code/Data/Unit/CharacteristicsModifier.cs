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

        public string Formula{
            get => _formula;
            set => _formula = value;
        }
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
}