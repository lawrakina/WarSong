using System;
using Data;
using Enums;
using UnityEngine;


namespace Weapons
{
    [Serializable] public class BaseWeapon : MonoBehaviour
    {
        [SerializeField]
        public WeaponBullet StandardBullet;

        [SerializeField]
        public WeaponType Type;

        [SerializeField]
        public float AttackDistance;
        
        [SerializeField]
        public float AttackDistanceOffset = 1.0f;

        [SerializeField]
        public AttackValue AttackValue;

        [SerializeField]
        public int ItemLevel = 0;
    }
}