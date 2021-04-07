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
    public sealed class CreateNewCharacterUiWindow : UiWindow
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

        public ReactiveCommand<CharacterClass> selectCharacterClassCommand = new ReactiveCommand<CharacterClass>();
        public ReactiveCommand gotoSettingsCharacterClassCommand = new ReactiveCommand();
        
        
        #endregion
        public void Awake()
        {
            base.Init();
            
            _listClasses = new GameObjectLinkedList<CharacterClass>(new[]
            {
                new LinkedListItem<CharacterClass>(CharacterClass.Warrior, _warriorIcon),
                new LinkedListItem<CharacterClass>(CharacterClass.Rogue, _rogueIcon),
                new LinkedListItem<CharacterClass>(CharacterClass.Hunter, _hunterIcon),
                new LinkedListItem<CharacterClass>(CharacterClass.Mage, _mageIcon)
            });
        
            //prev class
            _prevClassButton.OnPointerClickAsObservable().Subscribe(_ =>
            {
                if (_listClasses.MovePrev())
                    selectCharacterClassCommand.ForceExecute(_listClasses.Current.Key);
            }).AddTo(_subscriptions);
            //next class
            _nextClassButton.OnPointerClickAsObservable().Subscribe(_ =>
            {
                if (_listClasses.MoveNext())
                    selectCharacterClassCommand.ForceExecute(_listClasses.Current.Key);
            }).AddTo(_subscriptions);

            gotoSettingsCharacterClassCommand.BindTo(_gotoSettingChar).AddTo(_subscriptions);
            
            _warriorIcon.SetActive(true);
            _rogueIcon.SetActive(false);
            _hunterIcon.SetActive(false);
            _mageIcon.SetActive(false);
        }
    }
}