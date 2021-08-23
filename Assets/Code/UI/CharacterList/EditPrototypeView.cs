using Code.Data;
using Code.Extension;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;


namespace Code.UI.CharacterList
{
    public sealed class EditPrototypeView : UiWindow
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
        private Button _backtoSelectCharClassButton;

        [SerializeField]
        private Button _createCharacterButton;
        
        public void Init(
            UnityAction<CharacterGender> changeGender,
            UnityAction<CharacterRace> changeRace,
            UnityAction backToSelectClass,
            UnityAction createCharacter)
        {
            _genderMaleToggle.onValueChanged.AddListener(value =>
            {
                if (value) changeGender(CharacterGender.Male);
            });
            _genderFemaleToggle.onValueChanged.AddListener(value =>
            {
                if (value) changeGender(CharacterGender.Female);
            });
            
            _raceHumanToggle.onValueChanged.AddListener(value =>
            {
                if (value) changeRace(CharacterRace.Human);
            });
            _raceNightElfToggle.onValueChanged.AddListener(value =>
            {
                if (value) changeRace(CharacterRace.Elf);
            });
            _raceBloodElfToggle.onValueChanged.AddListener(value =>
            {
                if (value) changeRace(CharacterRace.Gnome);
            });
            _raceOrcToggle.onValueChanged.AddListener(value =>
            {
                if (value) changeRace(CharacterRace.Orc);
            });
            
            _backtoSelectCharClassButton.onClick.AddListener(backToSelectClass);
            _createCharacterButton.onClick.AddListener(createCharacter);
        }

        public override void OnDestroy()
        {
            _genderMaleToggle.RemoveAllListeners();
            _genderFemaleToggle.RemoveAllListeners();
            _raceHumanToggle.RemoveAllListeners();
            _raceNightElfToggle.RemoveAllListeners();
            _raceBloodElfToggle.RemoveAllListeners();
            _raceOrcToggle.RemoveAllListeners();
            
            _backtoSelectCharClassButton.RemoveAllListeners();
            _createCharacterButton.RemoveAllListeners();
            base.OnDestroy();
        }
    }
}