using System.Collections.Generic;
using UnityEngine;


namespace Code.Data.Unit.Player
{
    [CreateAssetMenu(fileName = "Config_PlayerData", menuName = "Data/Config Player Data")]
    public sealed class PlayerData : ScriptableObject
    {
        [SerializeField]
        public List<CharacterSettings> ListCharacters;

        [SerializeField]
        public int _numberActiveCharacter;
    }
}