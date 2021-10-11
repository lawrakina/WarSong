using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;


namespace Code.Data.Equipment.old
{
    public class EquipSyntyItem : MonoBehaviour, IDressable
    {
        public void PutOn()
        {
            
        }
    }
    
    
    
    public class EquipmentItem : MonoBehaviour, IDressable
    {
        [SerializeField] private string _name;
        [FormerlySerializedAs("_equipItemType")] [SerializeField] private EquipCellType equipCellType;
        [SerializeField] private EquipmentItemMesh[] _part;
        [SerializeField] private Material _material;

        public List<EquipmentItemMesh> Parts => _part.ToList();
        public Material Material => _material;
        public EquipCellType Type => equipCellType;

        public void PutOn()
        {
            ExecuteEvents.ExecuteHierarchy<IPutOnEquip>(gameObject, null, (x, y) => x.PuOnEquip(this));
        }
    }

    public interface IDressable
    {
        void PutOn();
    }

    public interface IPutOnEquip: IEventSystemHandler
    {
        void PuOnEquip(EquipmentItem item);
    }

    [Serializable]
    public class EquipmentItemMesh
    {
        [SerializeField] private TargetEquipCell _partType;
        [SerializeField] private Mesh _mesh;
        public TargetEquipCell PartType => _partType;
        public Mesh Mesh => _mesh;
    }
}