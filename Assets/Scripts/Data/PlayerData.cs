using UnityEngine;


namespace Data
{
    [CreateAssetMenu(fileName = "PlayerData", menuName = "Data/PlayerData")]
    public sealed class PlayerData : ScriptableObject
    {
        [SerializeField]
        public Characters _characters;

        [SerializeField]
        public int _numberActiveCharacter;
    }
}