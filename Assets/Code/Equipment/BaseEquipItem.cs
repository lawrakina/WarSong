using Code.Data;
using Code.Data.Unit;
using UnityEngine;

namespace Code.Equipment
{
    public abstract class BaseEquipItem : MonoBehaviour
    {
        public abstract InventoryItemType ItemType { get; }
        public abstract int ItemLevel { get; }
        public abstract Characteristics Characteristics { get; }
    }
}