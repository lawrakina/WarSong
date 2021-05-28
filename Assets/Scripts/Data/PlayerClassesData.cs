using Unit;
using UnityEngine;


namespace Data
{
    [CreateAssetMenu(fileName = "PlayerClassesData", menuName = "Data/Player Classes Data")]
    public sealed class PlayerClassesData : ScriptableObject
    {
        [Header("Absolute value")]
        public float healthPerStamina = 10.0f;
        public float manaPointsPerIntellect = 15.0f;
        public float maxRageValue = 100.0f;
        public float maxEnergyValue = 100.0f;
        public float maxConcentrationValue = 100.0f;
        public float PlayerMoveSpeed = 8.0f;
        public float RotateSpeedPlayer = 120.0f;

        [Header("Classes start value")]
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