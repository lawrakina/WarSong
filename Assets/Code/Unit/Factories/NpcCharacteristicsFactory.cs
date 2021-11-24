using Code.Data.Unit;
using Code.Data.Unit.Enemy;
using Code.Equipment;


namespace Code.Unit.Factories{
    public class NpcCharacteristicsFactory{
        private readonly EnemiesData _data;

        public NpcCharacteristicsFactory(EnemiesData data){
            _data = data;
        }

        public UnitCharacteristics GenerateCharacteristics(EnemySettings settings, int baseLevel){
            var characteristics = new UnitCharacteristics();
            characteristics.Values = new Characteristics(1F, 1F, 1F, 1F, 1F, 0.05F, 0.05F);
            characteristics.MoveSpeed = _data._baseMoveSpeed * settings.SpeedModifier;

            var weapon = settings.Weapon;
            if (!weapon){
                settings.Weapon = _data.RandomWeapon();
            }

            characteristics.Distance = settings.Weapon.AttackValue.GetAttackDistance();
            characteristics.MinAttack = (int) (_data._baseAttackValue.GetAttack().x * baseLevel);
            characteristics.MaxAttack = (int) (_data._baseAttackValue.GetAttack().y * baseLevel);

            //Modifiers
            characteristics.AttackModifier = 
                new ModificationOfObjectOfParam(1f, ArithmeticOperation.Multiplication);
            characteristics.SpeedAttackModifier =
                new ModificationOfObjectOfParam(1f, ArithmeticOperation.Multiplication);
            characteristics.LagBeforeAttackModifier =
                new ModificationOfObjectOfParam(1f, ArithmeticOperation.Multiplication);
            characteristics.DistanceModifier = 
                new ModificationOfObjectOfParam(1f, ArithmeticOperation.Multiplication);
            characteristics.CritChanceModifier =
                new ModificationOfObjectOfParam(1f, ArithmeticOperation.Multiplication);
            characteristics.DodgeChanceModifier =
                new ModificationOfObjectOfParam(1f, ArithmeticOperation.Multiplication);
            characteristics.ArmorModifier = 
                new ModificationOfObjectOfParam(1f, ArithmeticOperation.Multiplication);


            return characteristics;
        }
    }
}