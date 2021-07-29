using Windows;
using Enums;
using UniRx;
using UnityEngine;
using UnityEngine.UI;


namespace Gui.Characters
{
    public sealed class CreateSettingCharacterUiWindow : UiWindow
    {
        [Header("Gender toggle")]
        [SerializeField]
        private Toggle _genderMaleToggle;

        [SerializeField]
        private Toggle _genderFemaleToggle;


        [Header("Race toggle")]
        [SerializeField]
        private Toggle _raceHumanToggle;

        [SerializeField]
        private Toggle _raceNightElfToggle;

        [SerializeField]
        private Toggle _raceBloodElfToggle;

        [SerializeField]
        private Toggle _raceOrcToggle;


        [Header("Navigation")]
        [SerializeField]
        private Button _backtoSelectCharClass;

        [SerializeField]
        private Button _createCharacterButton;

        public ReactiveCommand<CharacterGender> _selectGenderCommand = new ReactiveCommand<CharacterGender>();
        public ReactiveCommand<CharacterRace> _selectRaceCommand = new ReactiveCommand<CharacterRace>();

        public ReactiveCommand _toSelectClassCommand = new ReactiveCommand();
        public ReactiveCommand _createNewCharacterCommand = new ReactiveCommand();


        private void Awake()
        {
            base.Init();
            
            //Gender toggles
            _genderMaleToggle.onValueChanged.AddListener(
                (_) => { _selectGenderCommand.Execute(CharacterGender.Male); });
            _genderFemaleToggle.onValueChanged.AddListener(
                (_) => { _selectGenderCommand.Execute(CharacterGender.Female); });

            //Race toggles
            _raceHumanToggle.onValueChanged.AddListener(
                (_) => { _selectRaceCommand.Execute(CharacterRace.Human); });
            _raceNightElfToggle.onValueChanged.AddListener(
                (_) => { _selectRaceCommand.Execute(CharacterRace.NightElf); });
            _raceBloodElfToggle.onValueChanged.AddListener(
                (_) => { _selectRaceCommand.Execute(CharacterRace.BloodElf); });
            _raceOrcToggle.onValueChanged.AddListener(
                (_) => { _selectRaceCommand.Execute(CharacterRace.Orc); });

            //Navigation buttons
            _toSelectClassCommand.BindTo(_backtoSelectCharClass).AddTo(_subscriptions);
            _createNewCharacterCommand.BindTo(_createCharacterButton).AddTo(_subscriptions);
        }
    }
}