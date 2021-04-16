using System;
using UniRx;
using UnityEngine;
using UnityEngine.UI;


namespace Windows
{
    public class TopNavigationUiWindow : UiWindow
    {
        [SerializeField]
        private Button _toListCharacter;
        
        
        public ReactiveCommand _toListCharacterCommand = new ReactiveCommand();

        private void Awake()
        {
            _toListCharacterCommand.BindTo(_toListCharacter);
        }
    }
}