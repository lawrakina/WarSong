using Enums;
using Weapons;


namespace EcsBattle.Components
{
    public struct BattleInfoComponent
    {
        public BaseWeapon Value;
        public WeaponBullet Bullet;
        public AttackValue AttackValue;
        public int AttackMaxValueAnimation;
        public int WeaponTypeAnimation;
    }

    public struct AwaitTimerForOneStrikeComponent
    {
        public float CurrentTime;
        public float MaxTime;
    }
    public struct TimerTickForGetPermissionToAttackComponent
    {
        public float CurrentTime;
        public float MaxTime;
    }
    public struct PermissionForAttackAllowedComponent
    {
    }
    public struct TimerForCheckVisionForEnemyComponent
    {
        public float CurrentTime;
        public float MaxTime;
    }
}