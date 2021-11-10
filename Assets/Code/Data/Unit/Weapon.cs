using Code.Equipment;
using Code.Extension;
using UnityEngine;


namespace Code.Data.Unit{
    public class Weapon{
        private readonly WeaponEquipItem _weaponEquip;
        private readonly EquipCellType _equipCellEquipCellType;
        private readonly UnitCharacteristics _characteristics;
        private AttackValue _attackValue;
        private float _critChance;

        public EquipCellType EquipType => _equipCellEquipCellType;
        public WeaponItemType WeaponType => _weaponEquip.WeaponType;

        public float Speed => _attackValue.GetAttackSpeed();
        public float Distance => _attackValue.GetAttackDistance();
        public WeaponBullet Bullet => _weaponEquip.StandardBullet;

        public Weapon(WeaponEquipItem weaponEquip, EquipCellType equipCellEquipCellType,
            UnitCharacteristics characteristics){
            _weaponEquip = weaponEquip;
            _equipCellEquipCellType = equipCellEquipCellType;
            _characteristics = characteristics;

            _attackValue = new AttackValue(
                _weaponEquip.AttackValue.GetAttack().ArithOperation(characteristics.AttackModifier),
                _weaponEquip.AttackDistance.ArithOperation(characteristics.DistanceModifier),
                _weaponEquip.AttackValue.GetAttackSpeed().ArithOperation(characteristics.SpeedAttackModifier),
                _weaponEquip.AttackValue.GetTimeLag().ArithOperation(characteristics.LagBeforeAttackModifier));

            _critChance = _weaponEquip.Characteristics.CritChance + characteristics.CritChance;
        }

        public float GetAttack(){
            var attackValue = Random.Range(_attackValue.GetAttack().x, _attackValue.GetAttack().y);
            if (Random.Range(0.0f, 1.0f) < _critChance) //critical attack
                attackValue *= _characteristics.CritAttackMultiplier;
            return attackValue;
        }
        
    }
}