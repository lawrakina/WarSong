using System;
using Windows;


namespace Controller
{
    [Serializable] public class UiWindows
    {
        public TopNavigationWindow TopNavigationWindow;
        public CharacterWindow CharacterWindow;
        public BattleWindow BattleWindow;
        public TavernWindow TavernWindow;
        public ShopWindow ShopWindow;
        public BottomNavigationWindow BottomNavigationWindow;

        public void Init()
        {
            BottomNavigationWindow._charAction.OnAction += () => CharacterWindow.Show();
            BottomNavigationWindow._battleAction.OnAction += () =>BattleWindow.Show();
            BottomNavigationWindow._tavernAction.OnAction += () => TavernWindow.Show();
            BottomNavigationWindow._shopAction.OnAction += () =>ShopWindow.Show();
        }
    }
}