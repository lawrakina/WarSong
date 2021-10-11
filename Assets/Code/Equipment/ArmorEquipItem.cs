using Code.Data;
using UnityEngine;

namespace Code.Equipment
{
    public class ArmorEquipItem : BaseEquipItem
    {
        [Header("Set FALSE if need SetActive(true)\nSet TRUE if need Object.Instantiate(Prefab)")]
        [SerializeField] private bool _isNeedInstantiate = false;
        [HideInInspector] private EquipItemType _equipType = EquipItemType.Armor;
        [SerializeField] private HeavyLightMedium _heavyLightMedium;

        public override bool IsNeedInstantiate => _isNeedInstantiate;
        public HeavyLightMedium HeavyLightMedium => _heavyLightMedium;
        public override EquipItemType EquipType => _equipType;
        public GameObject GameObject => gameObject;
    }
}