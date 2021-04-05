using Windows;
using UnityEngine;
using UnityEngine.UI;


namespace Controller
{
    public class BottomNavigationWindow : UiWindow
    {
        [SerializeField]
        private Toggle _battleToggle;
        
        [SerializeField]
        private Toggle _charToggle;

        [SerializeField]
        private Toggle _tavernToggle;

        [SerializeField]
        private Toggle _shopToggle;

        public UiCommand _battleAction = new UiCommand();
        public UiCommand _charAction = new UiCommand();
        public UiCommand _tavernAction = new UiCommand();
        public UiCommand _shopAction  = new UiCommand();

        public override void Init()
        {
            base.Init();
            
            _battleToggle.onValueChanged.AddListener((value) => { if (value) _battleAction.OnAction(); });
            _charToggle.onValueChanged.AddListener((value) => { if (value) _charAction.OnAction(); });
            _tavernToggle.onValueChanged.AddListener((value) => { if (value) _tavernAction.OnAction(); });
            _shopToggle.onValueChanged.AddListener((value) => { if (value) _shopAction.OnAction(); });
        }
    }
}