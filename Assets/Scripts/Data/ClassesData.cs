using System;
using Unit;
using UnityEngine;


namespace Data
{
    [CreateAssetMenu(fileName = "ClassesData", menuName = "Data/Classes Data")]
    public sealed class ClassesData : ScriptableObject
    {
        [SerializeField]
        public BasicCharacteristics Warrior;
        [SerializeField]
        public BasicCharacteristics Rogue;
        [SerializeField]
        public BasicCharacteristics Hunter;
        [SerializeField]
        public BasicCharacteristics Mage;
    }
}