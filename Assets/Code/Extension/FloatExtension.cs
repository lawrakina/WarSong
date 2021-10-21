using System;
using Code.Data.Unit;


namespace Code.Extension{
    public static class FloatExtension{
        public static float ArithOperation(this float value, ModificationOfObjectOfParam modificator){
            return modificator.TypeOfOperation switch{
                ArithmeticOperation.Addition => value + modificator.Value,
                ArithmeticOperation.Multiplication => value * modificator.Value,
                _ => 0
            };
        }
    }
}