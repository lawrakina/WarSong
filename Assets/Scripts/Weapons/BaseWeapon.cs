using System;
using Data;
using UnityEngine;
using Object = UnityEngine.Object;


namespace Weapons
{
    [Serializable]
    public class BaseWeapon : MonoBehaviour
    {
        [SerializeField]
        public WeaponBullet StandardBullet;

        [SerializeField]
        public WeaponType Type;
    }
}