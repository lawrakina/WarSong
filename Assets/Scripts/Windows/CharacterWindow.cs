using System;
using Extension;
using Gui.Characters;
using UnityEngine;


namespace Windows
{
    public sealed class CharacterWindow : UiWindow
    {
        #region Fields

        [Header("UI")]
        [SerializeField]
        public ListCharacterPanel _listCharacterPanel;

        [SerializeField]
        public CreateNewCharacterPanel _createNewCharacterPanel;

        [Header("Window")]
        [SerializeField]
        public GameObject _sceneRootObject;
        
        [SerializeField]
        private Transform _characterPositionSpawn;

        [SerializeField]
        private Camera _camera;

        [SerializeField]
        private GameObject _environment;

        public Action ShowListCharacter;
        public Action ShowSelectClass;

        #endregion


        #region Properties

        public Transform GetPositionCharacter()
        {
            return _characterPositionSpawn;
        }

        #endregion


        #region ClassLiveCycles

        public override void Init()
        {
            base.Init();
            _listCharacterPanel.Init();
            _createNewCharacterPanel.Init();
            
            CharacterSpawn = GetPositionCharacter;

            OnShow += OnShowExternalContent;
            OnHide += OnHideExternalContent;

            ShowListCharacter += () => { _listCharacterPanel.Show(); };
            ShowSelectClass += () => { _createNewCharacterPanel.Show(); };
        }

        ~CharacterWindow()
        {
            OnShow -= OnShowExternalContent;
            OnHide -= OnHideExternalContent;
            ShowSelectClass = null;
            ShowListCharacter = null;
        }

        #endregion


        #region Methods

        private void OnShowExternalContent(Guid id)
        {
            if(_sceneRootObject) _sceneRootObject.SetActive(true);
            if (_camera) _camera.enabled = true;
            if (_environment) _environment.SetActive(true);
        }

        private void OnHideExternalContent(Guid id)
        {
            if(_sceneRootObject) _sceneRootObject.SetActive(false);
            if (_camera) _camera.enabled = false;
            if (_environment) _environment.SetActive(false);
        }

        #endregion
    }
}