using System;
using System.Collections.Generic;
using Code.Data;
using Code.Data.Unit;
using Code.UI.UniversalTemplates;
using UnityEngine;


namespace Code.Equipment
{
    [Serializable]
    public abstract class BaseEquipItem : MonoBehaviour, IUiEquipItem
    {
        private Guid _guid = Guid.NewGuid();
        [HideInInspector] private InventoryItemType _inventoryType = InventoryItemType.EquipItem;
        [SerializeField] private UiInfo _uiInfo;
        [SerializeField] private List<GameObject> _listOfDependentViews;
        [SerializeField] public int ItemLevel = 1;
        [SerializeField] private int _armorValue;
        [SerializeField] private Characteristics _characteristics;
        [SerializeField] private TargetEquipCell _targetEquipCell;

        public Guid Guid => _guid;
        public TargetEquipCell TargetEquipCell => _targetEquipCell;
        public InventoryItemType InventoryType => _inventoryType;
        public Characteristics Characteristics => _characteristics;
        public UiInfo UiInfo => _uiInfo;
        public List<GameObject> Views => _listOfDependentViews;
        public int ArmorValue => _armorValue;
        public abstract EquipItemType EquipType { get; }
        public abstract bool IsNeedInstantiate { get; }
    }
}