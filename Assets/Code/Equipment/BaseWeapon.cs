using System;
using Code.Data;
using Code.Data.Unit;
using UnityEngine;
using UnityEngine.UIElements;


namespace Code.Equipment
{
    [Serializable]
    public class BaseWeapon : BaseEquipItem, IBaseWeapon
    {
        [SerializeField] private WeaponBullet _standardBullet;
        [SerializeField] private WeaponItemType _weaponType;
        [SerializeField] private float _attackDistanceOffset = 1.0f;
        [SerializeField] private AttackValue _attackValue;
        [SerializeField] private int _itemLevel = 1;
        [SerializeField] private Characteristics _characteristics; 
        public GameObject GameObject => gameObject;
        public WeaponBullet StandardBullet => _standardBullet;
        public WeaponItemType WeaponType => _weaponType;
        public float AttackDistanceOffset => _attackDistanceOffset;
        public AttackValue AttackValue => _attackValue;
        public override InventoryItemType ItemType => InventoryItemType.Weapon;
        public override int ItemLevel => _itemLevel;
        public override Characteristics Characteristics => _characteristics;
    }
}