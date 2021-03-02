using Weapons;


namespace EcsBattle.Components
{
    public struct BattleInfoComponent
    {
        public BaseWeapon Value;
        public WeaponBullet Bullet;
        public AttackValue AttackValue;
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
    internal struct PermissionForAttackAllowedComponent
    {
    }
}