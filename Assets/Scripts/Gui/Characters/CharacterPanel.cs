using System;
using Controller;
using Enums;
using UniRx;
using UnityEngine;


namespace Gui.Characters
{
    //есть стойкое подозрение на то что тут нужен MVVM))))
    [Serializable] public sealed class CharacterPanel : BasePanel
    {
        #region Fields

        [Header("Panels")]
        [SerializeField]
        private CreateNewCharacterPanel _newCharPanel;

        [SerializeField]
        private CreateSettingCharacterPanel _settingCharPanel;

        [SerializeField]
        private ListCharacterPanel _listCharPanel;

        private IReactiveProperty<EnumCharacterWindow> _activeCharacterWindow;
        private IReactiveProperty<EnumMainWindow> _activeWindow;

        private ListOfCharactersController _listOfCharactersController;

        #endregion


        public void Ctor(IReactiveProperty<EnumMainWindow> activeWindow,
            IReactiveProperty<EnumCharacterWindow> activeCharacterWindow,
            ListOfCharactersController listCharactersManager)
        {
            base.Ctor();
            _activeWindow = activeWindow;
            _activeCharacterWindow = activeCharacterWindow;
            _listOfCharactersController = listCharactersManager;

            _newCharPanel.Ctor(_activeCharacterWindow, _listOfCharactersController);
            _settingCharPanel.Ctor(_activeCharacterWindow, _listOfCharactersController);
            _listCharPanel.Ctor(_activeWindow, _activeCharacterWindow, _listOfCharactersController);

            //переключение между дочерними окнами
            _activeCharacterWindow.Subscribe(_ =>
            {
                if (_activeCharacterWindow.Value == EnumCharacterWindow.ListCharacters)
                    _listCharPanel.Show();
                else _listCharPanel.Hide();
                if (_activeCharacterWindow.Value == EnumCharacterWindow.NewSelectClass)
                    _newCharPanel.Show();
                else _newCharPanel.Hide();
                if (_activeCharacterWindow.Value == EnumCharacterWindow.NewSettingsCharacter)
                    _settingCharPanel.Show();
                else _settingCharPanel.Hide();
            }).AddTo(_subscriptions);
        }
    }
}