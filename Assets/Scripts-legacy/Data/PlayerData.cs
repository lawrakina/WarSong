using UnityEngine;


namespace Data
{
    [CreateAssetMenu(fileName = "PlayerData", menuName = "Data/Player Data")]
    public sealed class PlayerData : ScriptableObject
    {
        [SerializeField]
        public Characters _characters;

        [SerializeField]
        public int _numberActiveCharacter;

        [SerializeField]
        public PresetCharacters _presetCharacters;
    }
}