﻿using System;
using System.Collections.Generic;
using Code.Equipment;
using UnityEngine;


namespace Code.Data.Unit
{
    [Serializable]
    public sealed class CharacterEquipment
    {
        [Serializable]
        public class PermissionToCarryEquipment
        {
            [Header("One Handed")]
            public bool OneHandWeapon;
            [Header("Two One-Handed")]
            public bool TwoOneHandWeapon;
            [Header("Two-Handed Sword")]
            public bool TwoHandSwordWeapon;
            [Header("Two-HandedSpear")]
            public bool TwoHandSpearWeapon;
            [Header("Two-HandedStaff")]
            public bool TwoHandStaffWeapon;
            [Header("Range Two-Handed Bow")]
            public bool RangeTwoHandBowWeapon;
            [Header("Range Two-Handed Crossbow")]
            public bool RangeTwoHandCrossbowWeapon;
            [Header("Shield in Left hand")]
            public bool Shield;
            [Header("Heavy armor")]
            public bool HeavyArmor;
            [Header("MediumArmor")]
            public bool MediumArmor;
            [Header("Leght Armor")]
            public bool LightArmor;
        }

        [SerializeField] public PermissionToCarryEquipment permissionForEquipment;
        [SerializeField] public List<BaseEquipItem> Equipment;
        [SerializeField] public List<BaseEquipItem> Inventory;
    }
}