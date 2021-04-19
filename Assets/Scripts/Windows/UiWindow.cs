using System;
using Interface;
using UniRx;
using UnityEngine;


namespace Windows
{
    public abstract class UiWindow : MonoBehaviour, IInitialization
    {
        #region Fields

        private Guid _id = Guid.NewGuid();
        protected CompositeDisposable _subscriptions;
        public Action<Guid> OnShow;
        public Action<Guid> OnHide;
        public Guid Id => _id;

        #endregion


        #region ClassLiveCycles

        public virtual void Init()
        {
            _subscriptions = new CompositeDisposable();
        }
        
        ~UiWindow()
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
            OnShow?.Invoke(_id);
            gameObject.SetActive(true);
        }

        public virtual void Hide()
        {
            OnHide?.Invoke(_id);
            gameObject.SetActive(false);
        }

        #endregion
    }
}