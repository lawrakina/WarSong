using UnityEngine;
using UnityEngine.UI;


namespace Windows
{
    public class BottomNavigationUiWindow : UiWindow
    {
        [SerializeField]
        public Toggle _battleToggle;
        
        [SerializeField]
        public Toggle _charToggle;

        [SerializeField]
        public Toggle _tavernToggle;

        [SerializeField]
        public Toggle _shopToggle;
    }
}