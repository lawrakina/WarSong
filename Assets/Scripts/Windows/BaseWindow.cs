using Interface;
using UnityEngine;


namespace Windows
{
    public abstract class BaseWindow : MonoBehaviour, IWindow, IInit, ICleanup
    {
        #region Fields

        [SerializeField]
        private Camera _camera;

        [SerializeField]
        protected GameObject _content;

        #endregion


        #region Properties

        public delegate Transform GetCharacterSpawn();

        public GetCharacterSpawn CharacterSpawn;
        public Camera Camera => _camera;

        #endregion


        public virtual void Ctor()
        {
        }

        public void Cleanup()
        {
        }

        public virtual void Init()
        {
        }

        public virtual void Show()
        {
            _content.SetActive(true);
            _camera.enabled = true;
        }

        public virtual void Hide()
        {
            _content.SetActive(false);
            _camera.enabled = false;
        }
    }
}