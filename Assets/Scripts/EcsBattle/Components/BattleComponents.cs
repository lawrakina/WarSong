using Enums;
using Weapons;


namespace EcsBattle.Components
{
    public struct BattleInfoMainWeaponComponent
    {
        public BaseWeapon _value;
        public WeaponBullet _bullet;
        public AttackValue _attackValue;
        public int _attackMaxValueAnimation;
        public int _weaponTypeAnimation;
    }
    public struct BattleInfoSecondWeaponComponent
    {
        public BaseWeapon _value;
        public WeaponBullet _bullet;
        public AttackValue _attackValue;
        public int _attackMaxValueAnimation;
        public int _weaponTypeAnimation;
        public float _lagBeforeAttack;
        public float _powerFactor;
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
}