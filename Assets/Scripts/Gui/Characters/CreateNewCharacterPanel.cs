using Windows;
using Controller;
using Enums;
using Extension.Collections;
using UniRx;
using UniRx.Triggers;
using Unit.Player;
using UnityEngine;
using UnityEngine.UI;


namespace Gui.Characters
{
    public sealed class CreateNewCharacterPanel : UiWindow
    {
        #region Fields

        [Header("Selecting a class")]
        [SerializeField]
        private Button _prevClassButton;

        [SerializeField]
        private Button _nextClassButton;

        [SerializeField]
        private Button _gotoSettingChar;

        [Header("Classes Icons")]
        [SerializeField]
        private GameObject _warriorIcon;

        [SerializeField]
        private GameObject _rogueIcon;

        [SerializeField]
        private GameObject _hunterIcon;

        [SerializeField]
        private GameObject _mageIcon;

        private GameObjectLinkedList<CharacterClass> _listClasses;
        private IReactiveProperty<EnumCharacterWindow> _charWindow;
        private ListOfCharactersController _listCharactersManager;

        #endregion
        // public void Ctor(IReactiveProperty<EnumCharacterWindow> charWindow, ListOfCharactersController listCharactersManager)
        // {
        //     base.Ctor();
        //     _listCharactersManager = listCharactersManager;
        //     _charWindow = charWindow;
        //
        //     _listClasses = new GameObjectLinkedList<CharacterClass>(new[]
        //     {
        //         new LinkedListItem<CharacterClass>(CharacterClass.Warrior, _warriorIcon),
        //         new LinkedListItem<CharacterClass>(CharacterClass.Rogue, _rogueIcon),
        //         new LinkedListItem<CharacterClass>(CharacterClass.Hunter, _hunterIcon),
        //         new LinkedListItem<CharacterClass>(CharacterClass.Mage, _mageIcon)
        //     });
        //
        //     // //prev class
        //     // _prevClassButton.OnPointerClickAsObservable().Subscribe(_ =>
        //     // {
        //     //     if (_listClasses.MovePrev())
        //     //         _listCharactersManager.PrototypePlayer.CharacterClass.Value = _listClasses.Current.Key;
        //     // }).AddTo(_subscriptions);
        //     // //next class
        //     // _nextClassButton.OnPointerClickAsObservable().Subscribe(_ =>
        //     // {
        //     //     if (_listClasses.MoveNext())
        //     //         _listCharactersManager.PrototypePlayer.CharacterClass.Value = _listClasses.Current.Key;
        //     // }).AddTo(_subscriptions);
        //
        //     //goto settings
        //     _gotoSettingChar.OnPointerClickAsObservable().Subscribe(_ =>
        //     {
        //         _charWindow.Value = EnumCharacterWindow.NewSettingsCharacter;
        //     }).AddTo(_subscriptions);
        // }
    }
}