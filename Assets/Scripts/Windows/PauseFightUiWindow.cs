using System;
using UniRx;
using UnityEngine;
using UnityEngine.UI;


namespace Windows
{
    public class PauseFightUiWindow : UiWindow
    {
        [SerializeField]
        private Button _gotoMainMenuButton;
        [SerializeField]
        private Button _continueButton;
        
        public ReactiveCommand _continueFightCommand = new ReactiveCommand();
        public ReactiveCommand _gotoMainMenuCommand = new ReactiveCommand();

        private void Awake()
        {
            _continueFightCommand.BindTo(_continueButton);
            _gotoMainMenuCommand.BindTo(_gotoMainMenuButton);
        }
    }
}