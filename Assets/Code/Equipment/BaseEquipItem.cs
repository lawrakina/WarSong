using Code.Data;
using UnityEngine;

namespace Code.Equipment
{
    public abstract class BaseEquipItem : MonoBehaviour
    {
        public abstract InventoryItemType ItemType { get; }
        public abstract int ItemLevel { get; }
    }
}