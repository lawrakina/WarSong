using UnityEngine;


namespace Data
{
    [CreateAssetMenu(fileName = "BattleSettingsData", menuName = "Data/Battle Settings Data")]
    public sealed class BattleSettingsData : ScriptableObject
    {
        public float _maxTimeForReward = 120.0f;
    }
}