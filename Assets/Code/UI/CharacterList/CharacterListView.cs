using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Code.UI.CharacterList
{
    public class CharacterListView: UiWindow
    {
        [SerializeField]
        private Button _prevCharButton;

        [SerializeField]
        private Button _nextCharButton;

        [SerializeField]
        private Button _createCharacterButton;

        [SerializeField]
        private Button _selectingCharacterButton;

        [SerializeField]
        private Text _info;

        public string InfoFormatted
        {
            set => _info.text = value;
        }
        
        public void Init(
            UnityAction movePrev,
            UnityAction moveNext,
            UnityAction selectCurrentCharacter,
            UnityAction createNewPrototype)
        {
            _prevCharButton.onClick.AddListener(movePrev);
            _nextCharButton.onClick.AddListener(moveNext);
            _selectingCharacterButton.onClick.AddListener(selectCurrentCharacter);
            _createCharacterButton.onClick.AddListener(createNewPrototype);
        }
    }
}