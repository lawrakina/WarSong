using System;
using Code.Data;
using Code.Data.Unit;
using UnityEngine;


namespace Code.Equipment
{
    [Serializable]
    public abstract class BaseEquipItem : MonoBehaviour, IUiEquipItem
    {
        [SerializeField]
        private UiInfo _uiInfo;
        public abstract InventoryItemType ItemType { get; }
        public abstract int SubItemType { get; }
        public abstract int ItemLevel { get; }
        public abstract Characteristics Characteristics { get; }
        public UiInfo UiInfo => _uiInfo;
    }
}