using System.Linq;
using Code.Extension;
using Data;
using Unit;
using UnityEngine;


namespace Battle
{
    public class InteractiveObjectsInitialization
    {
        private readonly BattleSettingsData _battleSettingsData;

        public InteractiveObjectsInitialization(BattleSettingsData battleSettingsData)
        {
            _battleSettingsData = battleSettingsData;
        }
        public GoalLevelView GetGoal(SpawnMarkerGoalInDungeon marker)
        {
            var goalLevel = _battleSettingsData._storageGoalLevel.ToList()
                                               .FirstOrDefault(x => x.TypeGoalExit == marker.TypeGoalExit);
            var component = Object.Instantiate(goalLevel, marker.transform);
            component.gameObject.AddCapsuleCollider(1.5f, true, Vector3.zero, 1.0f);
            return component.GetComponent<GoalLevelView>();
        }
    }
}