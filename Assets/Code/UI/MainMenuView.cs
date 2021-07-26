using System;
using Code.Extension;
using UniRx;
using UnityEngine;


namespace Code.UI
{
    public sealed class MainMenuView : UiWindow
    {
        [SerializeField]
        public Transform _topNavPanel;
        [SerializeField]
        public Transform _contentPanel;
        [SerializeField]
        public Transform _bottomNavPanel;
    }

    public interface IInitialization
    {
        void Init();
    }

    public abstract class UiWindow : MonoBehaviour, IInitialization
    {
        #region Fields

        // private Guid _id = Guid.NewGuid();
        protected CompositeDisposable _subscriptions;
        public Action OnShow;
        public Action OnHide;
        // public Guid Id => _id;

        #endregion


        #region ClassLiveCycles

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