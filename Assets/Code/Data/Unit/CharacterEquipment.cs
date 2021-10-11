using System;
using System.Collections.Generic;
using Code.Equipment;
using UnityEngine;


namespace Code.Data.Unit
{
    [Serializable]
    public sealed class CharacterEquipment
    {
        [SerializeField] public PermissionToCarryEquipment permissionForEquipment;
        [SerializeField] public List<BaseEquipItem> Equipment;
        [SerializeField] public List<BaseEquipItem> Inventory;
    }
}