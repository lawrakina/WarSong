using Unit;
using UnityEngine;


namespace Data
{
    [CreateAssetMenu(fileName = "EnemyClassesData", menuName = "Data/Enemy Classes Data")]
    public sealed class EnemyClassesData : ScriptableObject
    {
        [Header("Absolute value")]
        public float HealthPedStamina = 10.0f;

        public float ManaPointsPerIntellect = 15.0f;
        public float MaxRageValue = 100.0f;
        public float MaxEnergyValue = 100.0f;
        public float MaxConcentrationValue = 100.0f;

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