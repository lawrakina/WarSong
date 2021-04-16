using System;
using Gui.Battle;
using UnityEngine;


namespace Windows
{
    [Serializable] public class UiWindows
    {
        [SerializeField]
        private TopNavigationUiWindow topNavigationUiWindow;

        [SerializeField]
        private CharacterUiWindow _characterWindow;

        [SerializeField]
        private BattleUiWindow battleUiWindow;

        [SerializeField]
        private TavernUiWindow tavernUiWindow;

        [SerializeField]
        private ShopUiWindow shopUiWindow;

        [SerializeField]
        private BottomNavigationUiWindow _bottomNavigationWindow;

        [SerializeField]
        private LoadUiWindow loadUiWindow;

        [SerializeField]
        private FightUiWindow _fightWindow;

        [SerializeField]
        private PauseFightUiWindow _pauseFightUiWindow;


        public TopNavigationUiWindow TopNavigationUiWindow => topNavigationUiWindow;
        public CharacterUiWindow CharacterWindow => _characterWindow;
        public BattleUiWindow BattleUiWindow => battleUiWindow;
        public TavernUiWindow TavernUiWindow => tavernUiWindow;
        public ShopUiWindow ShopUiWindow => shopUiWindow;
        public BottomNavigationUiWindow BottomNavigationWindow => _bottomNavigationWindow;
        public LoadUiWindow LoadUiWindow => loadUiWindow;
        public FightUiWindow FightUiWindow => _fightWindow;
        public PauseFightUiWindow PauseFightUiWindow =>_pauseFightUiWindow;
    }
}