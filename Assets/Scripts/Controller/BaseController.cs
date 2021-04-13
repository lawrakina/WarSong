using Interface;
using UniRx;


namespace Controller
{
    public abstract class BaseController : IInitialization, ICleanup
    {
        #region Fields

        // protected CompositeDisposable _subscriptions;
        protected bool _isEnable;

        #endregion


        #region ClassLiveCycles

        public BaseController()
        {
            // _subscriptions = new CompositeDisposable();
        }

        #endregion


        #region ICleanUp

        public virtual void Cleanup()
        {
            // _subscriptions?.Dispose();
        }

        #endregion


        #region IController

        public virtual void On()
        {
            _isEnable = true;
        }

        public virtual void Off()
        {
            _isEnable = false;
        }

        #endregion


        #region IInitialization

        public virtual void Init()
        {
            _isEnable = true;
        }

        #endregion
    }
}