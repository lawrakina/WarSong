using Gui.Characters;
using UnityEngine;


namespace Windows
{
    public sealed class CharacterUiWindow : UiWindow
    {
        #region Fields

        [Header("UI")]
        [SerializeField]
        public ListCharacterUiWindow listCharacterUiWindow;

        [SerializeField]
        public CreateNewCharacterUiWindow createNewCharacterUiWindow;

        [SerializeField]
        public AboutActiveCharacterUiWindow aboutActiveCharacterUiWindow;

        #endregion
    }
}