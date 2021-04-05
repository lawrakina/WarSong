using System;
using UnityEngine;


namespace Windows
{
    public sealed class BattleWindow : UiWindow
    {
        #region Fields

        // [Header("UI")]
        
        [Header("Window")]
        [SerializeField]
        public GameObject _sceneRootObject;
        
        [SerializeField]
        private Camera _forTextureRenderCamera;

        [SerializeField]
        public Camera _camera;

        [SerializeField]
        private GameObject _environment;

        #endregion


        #region Properties

        #endregion


        #region ClassLiveCycles

        public override void Init()
        {
            base.Init();

            OnShow += OnShowExternalContent;
            OnHide += OnHideExternalContent;
        }

        ~BattleWindow()
        {
            OnShow -= OnShowExternalContent;
            OnHide -= OnHideExternalContent;
        }

        #endregion


        #region Methods

        private void OnShowExternalContent(Guid id)
        {
            if(_sceneRootObject) _sceneRootObject.SetActive(true);
            if (_camera) _camera.enabled = true;
            if (_environment) _environment.SetActive(true);
            if (_forTextureRenderCamera) _forTextureRenderCamera.enabled = true;
        }

        private void OnHideExternalContent(Guid id)
        {
            if(_sceneRootObject) _sceneRootObject.SetActive(false);
            if (_camera) _camera.enabled = false;
            if (_environment) _environment.SetActive(false);
            if (_forTextureRenderCamera) _forTextureRenderCamera.enabled = false;
        }

        #endregion
    }
}