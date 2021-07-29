using System.Collections.Generic;
using UnityEngine;


namespace Code.Data.Unit.Player
{
    [CreateAssetMenu(fileName = nameof(PlayerData), menuName = "Configs/" + nameof(PlayerData))]
    public sealed class PlayerData : ScriptableObject
    {
        [SerializeField]
        public List<CharacterSettings> ListCharacters;

        [SerializeField]
        public int _numberActiveCharacter;
    }
}