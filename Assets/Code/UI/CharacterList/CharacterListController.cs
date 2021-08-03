using Code.Data;
using Code.Extension;
using Code.GameCamera;
using Code.Profile;
using UniRx;
using UnityEngine;


namespace Code.UI.CharacterList
{
    public sealed class CharacterListController : BaseController
    {
        private readonly Transform _placeForUi;
        private readonly ProfilePlayer _profilePlayer;
        private CharacterPrototypeController _createPrototypeController;
        private CharacterListView _view;

        private int Position
        {
            get => _settings.PlayerData._numberActiveCharacter;
            set => _settings.PlayerData._numberActiveCharacter = value;
        }

        private DataSettings _settings;

        public CharacterListController(Transform placeForUi, ProfilePlayer profilePlayer,
            CameraController cameraController)
        {
            _placeForUi = placeForUi;
            _profilePlayer = profilePlayer;
            _settings = profilePlayer.Settings;

            _createPrototypeController = new CharacterPrototypeController(_placeForUi, _profilePlayer);
            _createPrototypeController.OnPrototypeChange += _profilePlayer.RebuildCharacter;
            _createPrototypeController.OnCreateCharacter += OnCreateCharacter;
            AddController(_createPrototypeController);

            _view = ResourceLoader.InstantiateObject(
                _profilePlayer.Settings.UiViews.Create_CharacterList, placeForUi, false);
            AddGameObjects(_view.gameObject);
            _profilePlayer.InfoAboutCurrentPlayer
                .Subscribe(info => _view.InfoFormatted = info.FullInfo).AddTo(_subscriptions);

            _view.Init(MovePrev, MoveNext, SelectCurrentCharacter, CreateNewPrototype);
            OffExecute();

            if (_settings.PlayerData.ListCharacters.Count <= 0)
            {
                CreateNewPrototype();
            }
            else
            {
                OnExecute();
                if (_profilePlayer.CurrentPlayer == null)
                {
                    _profilePlayer.BuildPlayer();
                }
            }
        }

        private void CreateNewPrototype()
        {
            OffExecute();
            _createPrototypeController.Init();
            _createPrototypeController.CreateNewPrototype();
        }

        private void MoveNext()
        {
            if (Position < _settings.PlayerData.ListCharacters.Count - 1)
            {
                Position++;
                _profilePlayer.RebuildCurrentCharacter();
            }
        }

        private void SelectCurrentCharacter()
        {
            _profilePlayer.CurrentState.Value = GameState.Menu;
        }

        private void MovePrev()
        {
            if (Position > 0)
            {
                Position--;
                _profilePlayer.RebuildCurrentCharacter();
            }
        }

        private void OnCreateCharacter()
        {
            _createPrototypeController.OffExecute();
            OnExecute();
        }

        protected override void OnDispose()
        {
            _createPrototypeController.OnPrototypeChange -= _profilePlayer.RebuildCharacter;
            _createPrototypeController.OnCreateCharacter -= OnCreateCharacter;
            base.OnDispose();
        }
    }
}