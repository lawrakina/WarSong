using UnityEngine;


namespace Code.Data
{
    [CreateAssetMenu(fileName = "Config_BattleSettingsData", menuName = "Data/Config Battle Settings Data")]
    public sealed class BattleSettingsData : ScriptableObject
    {
        [SerializeField]
        public float _maxTimeForReward = 120.0f;
        [SerializeField]
        public GoalLevelView[] _storageGoalLevel;
    }
}