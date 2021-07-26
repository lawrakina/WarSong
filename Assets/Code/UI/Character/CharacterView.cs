using UnityEngine;
using UnityEngine.UI;

namespace Code.UI.Character
{
    public sealed class CharacterView : UiWindow
    {
        [SerializeField] private Text _info;
        [SerializeField] private GameObject _mainSlot;
        [SerializeField] private GameObject _secondSlot;

        public string InfoFormatted
        {
            set => _info.text = value;
        }
    }
}