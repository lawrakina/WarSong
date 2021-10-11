using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;


namespace Code.Data.Equipment.old
{
    public class EquipmentList: MonoBehaviour, IPutOnEquip
    {
        [SerializeField] private List<KvpEquipPartAndTransform> _equipPoints;
        [SerializeField] private List<KvpEquipTypeAndTransform> _equipmentListItems;

        private void Start()
        {
            foreach (var item in _equipmentListItems)
            {
                var obj = Object.Instantiate(item.Value, this.transform);
                obj.GetComponent<IDressable>().PutOn();
            }
        }

        public void PuOnEquip(EquipmentItem item)
        {
            Debug.Log(item);
            foreach (var part in item.Parts)
            {
                var point = _equipPoints.Find(x=>x.Key ==part.PartType);
                var equipItem = Object.Instantiate(item, point.Value);
                equipItem.transform.localPosition = Vector3.zero;
                equipItem.gameObject.AddComponent<MeshFilter>().mesh = part.Mesh;
                equipItem.gameObject.AddComponent<MeshRenderer>().material = item.Material;
            }
            // _equipmentListItems.Add(new KvpEquipTypeAndTransform(){ Key = item.Type, Value = item.transform});
        }
    }

    [Serializable]
    public class KvpEquipPartAndTransform
    {
        public TargetEquipCell Key;
        public Transform Value;
    }
    [Serializable]
    public class KvpEquipTypeAndTransform
    {
        public EquipCellType Key;
        public Transform Value;
    }
}