using System;
using UnityEngine;


namespace ContentDataSource.AbilityItems
{
    [CreateAssetMenu(fileName = "AbilitiesConfigData", menuName = "Abilities/AbilitiesConfigData", order = 0)]
    public class AbilitiesConfigData : ScriptableObject
    {
        [SerializeField]
        public AbilityItemConfig[] _listAbilitiesForWarrior;
        [SerializeField]
        public AbilityItemConfig[] _listAbilitiesForRoque;
        [SerializeField]
        public AbilityItemConfig[] _listAbilitiesForHunter;
        [SerializeField]
        public AbilityItemConfig[] _listAbilitiesForMage;
    }
}