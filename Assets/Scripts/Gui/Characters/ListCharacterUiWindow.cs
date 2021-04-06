using Windows;
using UniRx;
using UnityEngine;
using UnityEngine.UI;


namespace Gui.Characters
{
    public sealed class ListCharacterUiWindow : UiWindow
    {
        #region Fields

        [SerializeField]
        private Button _prevCharButton;

        [SerializeField]
        private Button _nextCharButton;

        [SerializeField]
        private Button _createCharacterButton;

        [SerializeField]
        private Button _selectingCharacterButton;

        [SerializeField]
        public Text _info;

        public ReactiveCommand _prevCharCommand = new ReactiveCommand();
        public ReactiveCommand _nextCharCommand = new ReactiveCommand();
        public ReactiveCommand _selectCharCommand = new ReactiveCommand();
        public ReactiveCommand _createCharCommand = new ReactiveCommand();

        #endregion


        private void Awake()
        {
            _prevCharCommand.BindTo(_prevCharButton);
            _nextCharCommand.BindTo(_nextCharButton);
            _selectCharCommand.BindTo(_selectingCharacterButton);
            _createCharCommand.BindTo(_createCharacterButton);
        }
    }
}