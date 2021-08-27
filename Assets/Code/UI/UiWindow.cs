using System;
using UniRx;
using UnityEngine;


namespace Code.UI
{
    public abstract class UiWindow : MonoBehaviour, IInitialization
    {
        #region Fields

        private GameObject _gameObject;

        private Transform _transform;

        // private Guid _id = Guid.NewGuid();
        protected CompositeDisposable _subscriptions;
        public Action OnShow;

        public Action OnHide;
        // public Guid Id => _id;

        #endregion


        #region Properties

        public GameObject GameObject => _gameObject;
        public Transform Transform => _transform;

        #endregion


        #region ClassLiveCycles

        private void Start()
        {
            _gameObject = GetComponent<GameObject>();
            _transform = GetComponent<Transform>();
        }

        public virtual void Init()
        {
            _subscriptions = new CompositeDisposable();
            // Dbg.Log($"{gameObject.name}.Id:{Id}");
        }

        public virtual void OnDestroy()
        {
            _subscriptions?.Dispose();
        }

        #endregion


        #region Methods

        public void SetActive(bool value)
        {
            gameObject.SetActive(value);
        }

        public virtual void Show()
        {
            OnShow?.Invoke();
            gameObject.SetActive(true);
        }

        public virtual void Hide()
        {
            OnHide?.Invoke();
            gameObject.SetActive(false);
        }

        #endregion
    }
}