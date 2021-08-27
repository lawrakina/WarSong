using System;
using System.Collections.Generic;
using Code.Data;
using Code.Data.Unit;
using UnityEngine;


namespace Code.Equipment
{
    [Serializable]
    public abstract class BaseArmorItem : BaseEquipItem, IBaseArmor
    {
        [SerializeField]
        private int _itemLevel = 1;

        [SerializeField]
        private int _armorValue = 1;

        [SerializeField]
        private Characteristics _characteristics;

        [SerializeField]
        private HeavyLightMedium _hvMdLt;

        [SerializeField]
        private string _nameInHierarchy;

        private List<GameObject> _listOfGameObjectsInViews;

        public HeavyLightMedium HvMdLt => _hvMdLt;
        public override InventoryItemType ItemType => InventoryItemType.Armor;
        public override int ItemLevel => _itemLevel;
        public override Characteristics Characteristics => _characteristics;
        public GameObject GameObject => gameObject;
        public int ArmorValue => _armorValue;
        public string NameInHierarchy => _nameInHierarchy;
        public List<GameObject> ListViews
        {
            get => _listOfGameObjectsInViews;
            set => _listOfGameObjectsInViews = value;
        }

        public override int SubItemType { get; }
        // int ArmorItemType { get; }
    }
}