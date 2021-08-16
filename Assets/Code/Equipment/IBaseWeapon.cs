using Code.Data;
using UnityEngine;


namespace Code.Equipment
{
    public interface IBaseWeapon
    {
        GameObject GameObject { get; }
        WeaponBullet StandardBullet { get; }

        WeaponItemType WeaponType { get; }

        float AttackDistanceOffset { get; }

        AttackValue AttackValue { get; }
    }
}