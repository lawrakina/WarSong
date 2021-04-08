using Enums;
using Weapons;


namespace EcsBattle.Components
{
    public struct BattleInfoMainWeaponComponent
    {
        public BaseWeapon Value;
        public WeaponBullet Bullet;
        public AttackValue AttackValue;
        public int AttackMaxValueAnimation;
        public int WeaponTypeAnimation;
    }
    public struct BattleInfoSecondWeaponComponent
    {
        public BaseWeapon Value;
        public WeaponBullet Bullet;
        public AttackValue AttackValue;
        public int AttackMaxValueAnimation;
        public int WeaponTypeAnimation;
        public float lagBeforeAttack;
        public float powerFactor;
    }

    public struct AwaitTimerForOneStrikeFromMainWeaponComponent
    {
        public float CurrentTime;
        public float MaxTime;
    }
    public struct TimerTickForGetPermissionToAttackFromMainWeaponComponent
    {
        public float CurrentTime;
        public float MaxTime;
    }
    public struct PermissionForAttackFromMainWeaponAllowedComponent
    {
    }
    public struct TimerForCheckVisionForEnemyComponent
    {
        public float CurrentTime;
        public float MaxTime;
    }
    

    public struct TimerTickForGetPermissionToAttackFromSecondWeaponComponent
    {
        public float CurrentTime;
        public float MaxTime;
    }

    public struct AwaitTimerForOneStrikeFromSecondWeaponComponent
    {
        public float CurrentTime;
        public float MaxTime;
    }

    public struct PermissionForAttackFromSecondWeaponAllowedComponent
    {
    }
}