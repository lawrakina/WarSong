using Code.Equipment;
using Code.Unit;


namespace Code.Fight.EcsBattle
{
    public struct BattleInfoMainWeaponComponent
    {
        public WeaponBullet _bullet;
        public AttackValue _attackValue;
        public int _attackMaxValueAnimation;
        public int _weaponTypeAnimation;
        public float _attackDistance;
    }
    public struct BattleInfoSecondWeaponComponent
    {
        public WeaponBullet _bullet;
        public AttackValue _attackValue;
        public int _attackMaxValueAnimation;
        public int _weaponTypeAnimation;
        public float _lagBeforeAttack;
        public float _powerFactor;
        public float _attackDistance;
    }

    public struct AwaitTimerForOneStrikeFromMainWeaponComponent
    {
        public float _currentTime;
        public float _maxTime;
    }
    public struct TimerTickForGetPermissionToAttackFromMainWeaponComponent
    {
        public float _currentTime;
        public float _maxTime;
    }
    public struct PermissionForAttackFromMainWeaponAllowedComponent
    {
    }
    public struct TimerForCheckVisionForEnemyComponent
    {
        public float _currentTime;
        public float _maxTime;
    }
    

    public struct TimerTickForGetPermissionToAttackFromSecondWeaponComponent
    {
        public float _currentTime;
        public float _maxTime;
    }

    public struct AwaitTimerForOneStrikeFromSecondWeaponComponent
    {
        public float _currentTime;
        public float _maxTime;
    }

    public struct PermissionForAttackFromSecondWeaponAllowedComponent
    {
    }

    public struct WeaponBulletComponent
    {
        public WeaponBullet _value;
        public InfoCollision _collision;
        public float _bulletTargetSqrtDistance;
    }
    public struct DisableComponent
    {
    }
}