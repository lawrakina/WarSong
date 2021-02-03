using Controller;
using Enums;
using UniRx;
using UniRx.Triggers;
using Unit.Player;
using UnityEngine;
using UnityEngine.UI;


namespace Gui.Characters
{
    public sealed class CreateSettingCharacterPanel : BasePanel
    {
        private IReactiveProperty<EnumCharacterWindow> _activeCharacterWindow;

        [SerializeField]
        private Button _backtoSelectCharClass;

        [Header("Navigation")]
        [SerializeField]
        private Button _createCharacterButton;

        [Header("Gender toggle")]
        [SerializeField]
        private Toggle _genderManToggle;

        [SerializeField]
        private Toggle _genderWomanToggle;

        private ListOfCharactersController _listCharactersManager;

        [SerializeField]
        private Toggle _raceBloodElfToggle;

        [Header("Race toggle")]
        [SerializeField]
        private Toggle _raceHumanToggle;

        [SerializeField]
        private Toggle _raceNightElfToggle;

        [SerializeField]
        private Toggle _raceOrcToggle;

        public void Ctor(IReactiveProperty<EnumCharacterWindow> activeCharacterWindow,
            ListOfCharactersController listCharactersManager)
        {
            base.Ctor();
            _listCharactersManager = listCharactersManager;
            _activeCharacterWindow = activeCharacterWindow;


            //complete creation 
            _createCharacterButton.OnPointerClickAsObservable().Subscribe(_ =>
            {
                _listCharactersManager.SaveNewCharacter();
                _activeCharacterWindow.Value = EnumCharacterWindow.ListCharacters;
            }).AddTo(_subscriptions);

            //goto selecting class
            _backtoSelectCharClass.OnPointerClickAsObservable().Subscribe(_ =>
            {
                _activeCharacterWindow.Value = EnumCharacterWindow.NewSelectClass;
            }).AddTo(_subscriptions);

            // //gender
            // _genderManToggle.OnValueChangedAsObservable().Subscribe(_ =>
            // {
            //     _listCharactersManager.PrototypePlayer.CharacterGender.Value =
            //         CharacterGender.Male;
            // }).AddTo(_subscriptions);
            // _genderWomanToggle.OnValueChangedAsObservable().Subscribe(_ =>
            // {
            //     _listCharactersManager.PrototypePlayer.CharacterGender.Value =
            //         CharacterGender.Female;
            // }).AddTo(_subscriptions);
            //
            // //race
            // _raceHumanToggle.OnValueChangedAsObservable().Subscribe(_ =>
            // {
            //     _listCharactersManager.PrototypePlayer.CharacterRace.Value = CharacterRace.Human;
            // }).AddTo(_subscriptions);
            // _raceNightElfToggle.OnValueChangedAsObservable().Subscribe(_ =>
            // {
            //     _listCharactersManager.PrototypePlayer.CharacterRace.Value =
            //         CharacterRace.NightElf;
            // }).AddTo(_subscriptions);
            // _raceBloodElfToggle.OnValueChangedAsObservable().Subscribe(_ =>
            // {
            //     _listCharactersManager.PrototypePlayer.CharacterRace.Value =
            //         CharacterRace.BloodElf;
            // }).AddTo(_subscriptions);
            // _raceOrcToggle.OnValueChangedAsObservable().Subscribe(_ =>
            // {
            //     _listCharactersManager.PrototypePlayer.CharacterRace.Value = CharacterRace.Orc;
            // }).AddTo(_subscriptions);
        }
    }
}