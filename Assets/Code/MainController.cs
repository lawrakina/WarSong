using Code.Data;
using Code.Extension;
using Code.Profile;
using Code.UI.CharacterList;
using UniRx;
using UnityEngine;


namespace Code
{
    public class MainController : BaseController
    {
        private readonly Transform _placeForUi;
        private readonly ProfilePlayer _profilePlayer;

        private CharacterListController _characterListController;
        private MainMenuController _mainMenuController;
        // private FightController _gameController;

        private readonly CommandManager _commandManager;
        private GameState _oldState = GameState.None;

        public MainController(Transform placeForUi, ProfilePlayer profilePlayer )
        {
            _profilePlayer = profilePlayer;
            _placeForUi = placeForUi;
            OnChangeGameState(_profilePlayer.CurrentState.Value);
            profilePlayer.CurrentState.Subscribe(OnChangeGameState).AddTo(_subscriptions);
        }

        private void OnChangeGameState(GameState state)
        {
            if (state == _oldState) return;
            Dbg.Log($"OnChangeGameState: {state}");

            switch (state)
            {
                case GameState.ListOfCharacter:
                    _characterListController = new CharacterListController(_placeForUi, _profilePlayer);
                    _mainMenuController?.Dispose();
                    // _gameController?.Dispose();
                    
                    break;
                case GameState.Menu:
                    _mainMenuController = new MainMenuController(_placeForUi, _profilePlayer);
                    // _gameController?.Dispose();
                    _characterListController?.Dispose();
                    break;

                case GameState.Fight:
                    // _gameController = new FightController(_placeForUi, _profilePlayer);
                    _mainMenuController?.Dispose();
                    _characterListController?.Dispose();
                    break;

                default:
                    _mainMenuController?.Dispose();
                    // _gameController?.Dispose();
                    _characterListController?.Dispose();
                    break;
            }

            _oldState = state;
        }

        protected override void OnDispose()
        {
            _commandManager?.Dispose();
            _mainMenuController?.Dispose();
            // _gameController?.Dispose();
            base.OnDispose();
        }
    }
}