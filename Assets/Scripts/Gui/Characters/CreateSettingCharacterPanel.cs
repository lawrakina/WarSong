using Windows;
using Controller;
using UnityEngine;
using UnityEngine.UI;


namespace Gui.Characters
{
    public sealed class CreateSettingCharacterPanel : UiWindow
    {
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

    }
}