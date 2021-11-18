using Code.Data;
using Code.Data.Unit;
using Code.Equipment;
using Code.Unit;


namespace Code.Fight.EcsFight.Battle{
    public struct WeaponBulletC{
        public WeaponBullet Value;
        public InfoCollision Collision;
    }

    public struct AutoAttackTag{
    }
    public struct SecondWeaponC{
        public float LagBefAttack;
        public float Distance;
        public float Speed;
        public Weapon Value;
    }

    public struct MainWeaponC{
        public Weapon Value;
        public float Distance;
        public float LagBefAttack;
        public float Speed;
    }
    public struct AttackBannedWeapon1Tag{
    }
}