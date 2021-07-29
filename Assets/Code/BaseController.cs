using System;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using Object = UnityEngine.Object;


namespace Code
{
    public abstract class BaseController :IController, IDisposable
    {
        #region Fields

        protected CompositeDisposable _subscriptions = new CompositeDisposable();
        private Guid _id = Guid.Empty;
        private List<BaseController> _baseControllers;
        private List<GameObject> _gameObjects;
        private bool _isDisposed = false;

        private bool _isEnabled = true;
        private List<(bool, GameObject)> _enabledGameObjects = null;
        private List<(bool, BaseController)> _enabledControllers = null;

        public Action<Guid> On;

        #endregion


        #region Properties

        public Guid Id
        {
            get
            {
                if (_id == Guid.Empty) _id = Guid.NewGuid();
                return _id;
            }
        }

        #endregion


        #region IDisposable

        protected virtual void OnDispose()
        {
            _subscriptions?.Dispose();
            On = null;
        }

        public void Dispose()
        {
            if (!_isDisposed)
            {
                _isDisposed = true;
                if (_baseControllers != null)
                {
                    foreach (BaseController baseController in _baseControllers)
                    {
                        baseController?.Dispose();
                    }

                    _baseControllers.Clear();
                }

                if (_gameObjects != null)
                {
                    foreach (GameObject cachedGameObject in _gameObjects)
                    {
                        Object.Destroy(cachedGameObject);
                    }

                    _gameObjects.Clear();
                }

                if (_enabledControllers != null)
                {
                    _enabledControllers.Clear();
                    _enabledControllers = null;
                }

                if (_enabledGameObjects != null)
                {
                    _enabledGameObjects.Clear();
                    _enabledGameObjects = null;
                }

                OnDispose();
            }
        }

        #endregion


        #region Methods

        protected void AddController(BaseController baseController)
        {
            if (_baseControllers == null)
                _baseControllers = new List<BaseController>();
            _baseControllers.Add(baseController);
        }

        protected void AddGameObjects(GameObject gameObject)
        {
            if (_gameObjects == null)
                _gameObjects = new List<GameObject>();
            _gameObjects.Add(gameObject);
        }

        public void OnExecute()
        {
            On?.Invoke(Id);
            if (!_isEnabled)
            {
                _isEnabled = true;
                if (_enabledGameObjects != null)
                {
                    foreach (var (state, gameObject) in _enabledGameObjects)
                    {
                        gameObject.SetActive(state);
                    }

                    _enabledGameObjects.Clear();
                }

                if (_enabledControllers != null)
                {
                    foreach (var (state, controller) in _enabledControllers)
                    {
                        if (state)
                            controller.OnExecute();
                    }

                    _enabledControllers.Clear();
                }
            }
        }

        public void OffExecute()
        {
            if (_isEnabled)
            {
                _isEnabled = false;

                _enabledControllers = new List<(bool, BaseController)>();
                if (_baseControllers != null)
                {
                    foreach (BaseController baseController in _baseControllers)
                    {
                        _enabledControllers.Add((baseController._isEnabled, baseController));
                        baseController.OffExecute();
                    }
                }

                _enabledGameObjects = new List<(bool, GameObject)>();
                if (_gameObjects != null)
                {
                    foreach (GameObject cachedGameObject in _gameObjects)
                    {
                        _enabledGameObjects.Add((cachedGameObject.activeSelf, cachedGameObject));
                        cachedGameObject.SetActive(false);
                    }
                }
            }
        }

        public bool IsOn => _isEnabled;
        

        #endregion
    }
}