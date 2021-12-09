using Code.Data;
using Code.Data.Unit;
using Code.Equipment;
using Code.Unit;
using Leopotam.Ecs;


namespace Code.Fight.EcsFight.Battle{
    public struct WeaponBulletC{
        public WeaponBullet Value;
        public InfoCollision Collision;
    }

    // public struct SecondWeaponC{
    //     public float LagBefAttack;
    //     public float Distance;
    //     public float Speed;
    //     public Weapon Value;
    // }
    //
    // public struct MainWeaponC{
    //     public Weapon Value;
    //     public float Distance;
    //     public float LagBefAttack;
    //     public float Speed;
    // }
    public struct Weapon<T>{
        public Weapon Value;
        public float Distance;
        public float LagBefAttack;
        public float Speed;
    }
    public struct MainHand{}
    public struct SecondHand{}
    public struct AttackCollisionC{
        public InfoCollision Value;
    }
    public struct DeathEventC{
        public EcsEntity Killer;
    }

    public struct NeedShowUiEventC{
        public DamageType DamageType;
        public float PointsDamage;
    }
    public struct AttackEventWeapon<T>{
    }

    public struct StartAttackCommand{
    }

    public struct PermisAttackWeapon<T>{
    }
}