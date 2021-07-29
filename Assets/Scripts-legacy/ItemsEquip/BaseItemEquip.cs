using System;
using Enums;
using UnityEngine;


namespace ItemsEquip
{
    [Serializable] 
    public class BaseItemEquip : MonoBehaviour
    {
        [SerializeField]
        public EquipmentType Type;

        [SerializeField]
        public int ItemLevel = 0;
    }
}