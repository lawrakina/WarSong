﻿using System;
using Code.Data;
using UnityEngine;


namespace Code.Equipment
{
    [Serializable] public class BaseWeapon : BaseEquipItem
    {
        [SerializeField]
        public WeaponBullet StandardBullet;

        [SerializeField]
        public WeaponItemType itemType;

        [SerializeField]
        public float AttackDistance;
        
        [SerializeField]
        public float AttackDistanceOffset = 1.0f;

        [SerializeField]
        public AttackValue AttackValue;

        public override InventoryItemType ItemType => InventoryItemType.Weapon;
    }
}