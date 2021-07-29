using System;
using Enums;
using UnityEngine;


namespace Unit
{
    public class GoalLevelView :  MonoBehaviour
    {
        public TypeGoalExit TypeGoalExit;
        public event Action<GameObject> GoalTriggerEnter;

        private void OnTriggerEnter(Collider other)
        {
            GoalTriggerEnter?.Invoke(other.gameObject);
        }
    }
}