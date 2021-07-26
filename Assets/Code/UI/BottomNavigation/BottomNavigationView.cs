using Code.Extension;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;


namespace Code.UI.BottomNavigation
{
    public class BottomNavigationView : UiWindow
    {
        [SerializeField]
        public Toggle _adventureToggle;
        
        [SerializeField]
        public Toggle _charToggle;
        
        [SerializeField]
        public Toggle _inventoryToggle;
        
        [SerializeField]
        public Toggle _tavernToggle;
        
        [SerializeField]
        public Toggle _shopToggle;


        public override void OnDestroy()
        {
            _adventureToggle.RemoveAllListeners();
            _charToggle.RemoveAllListeners();
            _tavernToggle.RemoveAllListeners();
            _shopToggle.RemoveAllListeners();
            base.OnDestroy();
        }

        public void Init(UnityAction showAdventure, UnityAction showCharacter, UnityAction showInventory,
            UnityAction showTavern, UnityAction showShop)
        {
            base.Init();
            _adventureToggle.onValueChanged.AddListener(_ => { if (_adventureToggle.isOn) showAdventure(); });
            _charToggle.onValueChanged.AddListener(_ => { if (_charToggle.isOn) showCharacter(); });
            _inventoryToggle.onValueChanged.AddListener(_ => { if (_inventoryToggle.isOn) showInventory(); });
            _tavernToggle.onValueChanged.AddListener(_ => { if (_tavernToggle.isOn) showTavern(); });
            _shopToggle.onValueChanged.AddListener(_ => { if (_shopToggle.isOn) showShop(); });
        }
    }
}