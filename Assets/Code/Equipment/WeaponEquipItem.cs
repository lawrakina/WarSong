using Code.Data;
using UnityEngine;

namespace Code.Equipment
{
    public class WeaponEquipItem : BaseEquipItem
    {
        [Header("Set FALSE if need SetActive(true)\nSet TRUE if need Object.Instantiate(Prefab)")]
        [SerializeField] private bool _isNeedInstantiate = true;
        [HideInInspector] private EquipItemType _equipType = EquipItemType.Weapon;
        [SerializeField] private WeaponBullet _standardBullet;
        [SerializeField] private WeaponItemType _weaponType;
        [SerializeField] private float attackDistance = 1.0f;
        [SerializeField] private AttackValue _attackValue;

        public GameObject GameObject => gameObject;
        public WeaponBullet StandardBullet => _standardBullet;
        public WeaponItemType WeaponType => _weaponType;
        public float AttackDistance => attackDistance;
        public AttackValue AttackValue => _attackValue;
        public override EquipItemType EquipType => _equipType;
        public override bool IsNeedInstantiate => _isNeedInstantiate;
    }
}