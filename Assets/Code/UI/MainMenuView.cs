using UnityEngine;


namespace Code.UI
{
    public sealed class MainMenuView : UiWindow
    {
        [SerializeField] public Transform _topNavPanel;
        [SerializeField] public Transform _contentPanel;
        [SerializeField] public Transform _bottomNavPanel;
    }
}