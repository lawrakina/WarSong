using Code.Data;
using Code.Extension;
using Code.GameCamera;
using Code.Profile;
using UniRx;
using UnityEngine;


namespace Code.UI.CharacterList{
    public sealed class CharacterListController : BaseController{
        private readonly Transform _placeForUi;
        private readonly ProfilePlayer _profilePlayer;
        private readonly CameraController _cameraController;
        private CharacterPrototypeController _createPrototypeController;
        private CharacterListView _view;

        private int Position{
            get => _settings.PlayerData._numberActiveCharacter;
            set => _settings.PlayerData._numberActiveCharacter = value;
        }

        private DataSettings _settings;

        public CharacterListController(Transform placeForUi, ProfilePlayer profilePlayer,
            CameraController cameraController) : base(true){
            _placeForUi = placeForUi;
            _profilePlayer = profilePlayer;
            _cameraController = cameraController;
            _settings = profilePlayer.Settings; ;

            _createPrototypeController =
                new CharacterPrototypeController(false, _placeForUi, _profilePlayer, _cameraController);
            _createPrototypeController.OnPrototypeChange += _profilePlayer.RebuildCharacter.Invoke;
            _createPrototypeController.OnCreateCharacter += OnCreateCharacter;
            AddAsManagedController(_createPrototypeController, true);
            AddAsManagedController(this, true, true);

            _view = ResourceLoader.InstantiateObject(
                _profilePlayer.Settings.UiViews.Create_CharacterList, placeForUi, false);
            AddGameObjects(_view.gameObject);
            _profilePlayer.InfoAboutCurrentPlayer
                .Subscribe(info => _view.InfoFormatted = info.FullInfo).AddTo(_subscriptions);

            _view.Init(MovePrev, MoveNext, SelectCurrentCharacter, CreateNewPrototype);
            OnDeactivate();

            if (_settings.PlayerData.ListCharacters.Count <= 0){
                CreateNewPrototype();
            } else{
                OnActivate();
                if (_profilePlayer.CurrentPlayer == null){
                    _profilePlayer.BuildCharacter.Invoke();
                }
            }
            
            _cameraController.UpdatePosition(CameraAngles.BeforePlayer);
        }

        private void CreateNewPrototype(){
            OnDeactivate();
            _createPrototypeController.Init();
            _createPrototypeController.CreateNewPrototype();
        }

        private void MoveNext(){
            if (Position < _settings.PlayerData.ListCharacters.Count - 1){
                Position++;
                _profilePlayer.RebuildCurrentCharacter.Invoke();
            }
        }

        private void SelectCurrentCharacter(){
            _profilePlayer.CurrentState.Value = GameState.Menu;
        }

        private void MovePrev(){
            if (Position > 0){
                Position--;
                _profilePlayer.RebuildCurrentCharacter.Invoke();
            }
        }

        private void OnCreateCharacter(){
            _createPrototypeController.OnDeactivate();
            OnActivate();
        }

        public override void Dispose(){
            _createPrototypeController.OnPrototypeChange -= _profilePlayer.RebuildCharacter.Invoke;
            _createPrototypeController.OnCreateCharacter -= OnCreateCharacter;
            base.Dispose();
        }
    }
}