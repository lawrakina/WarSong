using Code.Extension;
using Code.Fight;
using Code.GameCamera;
using Code.Profile;
using Code.UI.CharacterList;
using UniRx;
using UnityEngine;


namespace Code
{
    public class MainController : BaseController
    {
        private readonly Transform _placeForUi;
        private readonly Controllers _controllers;
        private readonly ProfilePlayer _profilePlayer;

        private CharacterListController _characterListController;
        private MainMenuController _mainMenuController;
        private FightController _fightController;

        private readonly CommandManager _commandManager;
        private GameState _oldState = GameState.None;
        private CameraController _cameraController;

        public MainController(Controllers controllers, Transform placeForUi, ProfilePlayer profilePlayer)
        {
            _controllers = controllers;
            _profilePlayer = profilePlayer;
            _placeForUi = placeForUi;
            _cameraController = new CameraController(_profilePlayer);
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
                    _characterListController = new CharacterListController(_placeForUi, _profilePlayer,_cameraController);
                    _mainMenuController?.Dispose();
                    _fightController?.Dispose();
                    
                    break;
                case GameState.Menu:
                    _characterListController?.Dispose();
                    _mainMenuController = new MainMenuController(_placeForUi, _profilePlayer,_cameraController);
                    _fightController?.Dispose();
                    break;

                case GameState.Fight:
                    _characterListController?.Dispose();
                    _mainMenuController?.Dispose();
                    _fightController = new FightController(_controllers, _placeForUi, _profilePlayer,_cameraController);
                    break;

                default:
                    _mainMenuController?.Dispose();
                    _fightController?.Dispose();
                    _characterListController?.Dispose();
                    break;
            }

            _oldState = state;
        }

        public override void Dispose()
        {
            _commandManager?.Dispose();
            _mainMenuController?.Dispose();
            _fightController?.Dispose();
            base.Dispose();
        }
    }
}