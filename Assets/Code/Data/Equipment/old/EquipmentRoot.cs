using System;
using System.Collections.Generic;
using UnityEngine;

namespace Code.Data.Equipment.old
{
    public class EquipmentRoot : MonoBehaviour
    {
        [SerializeField] private List<KvpBodyPartTypeAndTransform> _listOfBodyParts;
        // [SerializeField] private List<KvpAttachPointTypeAndTransform> _listOfAttachPoints;
    }

    [Serializable]
    public class KvpBodyPartTypeAndTransform
    {
        
    }
    
    public enum HumanoidBodyParts
    {
        
    }
}