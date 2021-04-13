using Interface;
using UniRx;
using UnityEngine;


namespace Windows
{
    public abstract class UiWindow : MonoBehaviour, IInitialization
    {
        #region Fields

        protected CompositeDisposable _subscriptions;

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
            gameObject.SetActive(true);
        }

        public virtual void Hide()
        {
            gameObject.SetActive(false);
        }

        #endregion
    }
}