using Code.Extension;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;


namespace Code.UI.CharacterList
{
    public class CreatePrototypeView : UiWindow
    {
        [Header("Selecting a class")]
        [SerializeField]
        private Button _prevClassButton;

        [SerializeField]
        private Button _nextClassButton;

        [SerializeField]
        private Button _gotoSettingChar;

        [Header("Classes Icons")]
        [SerializeField]
        public Transform _placeForBadge;

        public void Init(UnityAction movePrev, UnityAction moveNext, UnityAction goToSettingChar)
        {
            _prevClassButton.onClick.AddListener(movePrev);
            _nextClassButton.onClick.AddListener(moveNext);
            _gotoSettingChar.onClick.AddListener(goToSettingChar);
        }

        public override void OnDestroy()
        {
            _prevClassButton.RemoveAllListeners();
            _nextClassButton.RemoveAllListeners();
            _gotoSettingChar.RemoveAllListeners();
            base.OnDestroy();
        }
    }
}