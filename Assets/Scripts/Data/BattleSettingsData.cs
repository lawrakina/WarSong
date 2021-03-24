using Battle;
using UnityEngine;
using VIew;


namespace Data
{
    [CreateAssetMenu(fileName = "BattleSettingsData", menuName = "Data/Battle Settings Data")]
    public sealed class BattleSettingsData : ScriptableObject
    {
        [SerializeField]
        public float _maxTimeForReward = 120.0f;
        [SerializeField]
        public GoalLevelView[] _storageGoalLevel;
    }
}