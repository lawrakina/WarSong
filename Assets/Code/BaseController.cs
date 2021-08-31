using System;
using System.Collections.Generic;
using Code.Extension;
using UniRx;
using UnityEngine;
using Object = UnityEngine.Object;


namespace Code
{
    public abstract class BaseController : IController, IDisposable
    {
        // Activated
        // Created
        // Deactivated
        private Guid _id = Guid.NewGuid();
        private bool _isDisposed = false;
        private bool _isEnabled = false;
        public event Action<Guid> Create;
        public event Action<Guid> Activate;
        public event Action<Guid> Deactivate;
        // public event Action<Guid> Disposable;
        public bool IsOn => _isEnabled;
        public Guid Id => _id;
        
        protected CompositeDisposable _subscriptions = new CompositeDisposable();
        private List<(bool, GameObject)> _enabledGameObjects;
        private List<(bool, BaseController)> _enabledControllers;
        private List<BaseController> _baseControllers;
        private List<GameObject> _gameObjects;
        
        /// <summary>
        /// It`s Toggle group
        /// </summary>
        private bool _toggleIs;
        private bool _toggleIsImRoot = false;
        private BaseController _toggleRootController;
        private readonly List<BaseController> _listToggle = new List<BaseController>();

        protected BaseController(bool activate = false)
        {
            Create?.Invoke(Id);
            if (activate)
            {
                _isEnabled = false;
                OnActivate();
            }
            else
            {
                _isEnabled = true;
                OnDeactivate();
            }
        }
        
        protected void Init(bool activate)
        {
            if (activate)
            {
                _isEnabled = false;
                OnActivate();
            }
            else
            {
                _isEnabled = true;
                OnDeactivate();
            }
            Init();
        }

        private void Init()
        {
            if (_toggleIs)
            {
                foreach (var controller in _listToggle)
                {
                    controller.Activate += OnActivate;
                    controller.Deactivate += OnDeactivate;
                    if (_toggleRootController != null)
                    {
                        if (controller.Id != _toggleRootController.Id)
                        {
                            controller.OnDeactivate();
                        }
                    }
                }
            }
        }
        
        protected void AddController(BaseController baseController, bool isToggle = false, bool isRoot = false)
        {
            if(baseController == null)
                return;
            _baseControllers ??= new List<BaseController>();
            _baseControllers.Add(baseController);
            if (isToggle)
            {
                _toggleIs = true;
                _listToggle.Add(baseController);
                if (isRoot)
                {
                    if(_toggleIsImRoot)
                        Dbg.Error($"WARNING! Reinstallation toggle root controller!");
                    _toggleIsImRoot = true;
                    _toggleRootController = baseController;
                    // Init();
                }
            }
        }

        protected void AddGameObjects(GameObject gameObject)
        {
            if (_gameObjects == null)
                _gameObjects = new List<GameObject>();
            _gameObjects.Add(gameObject);
        }

        private void OnActivate(Guid id)
        {
            foreach (var controller in _listToggle)
            {
                if (controller.Id != id)
                {
                    controller.OnDeactivate();
                }
            }
        }
        
        public void OnActivate()
        {
            if (!_isEnabled)
            {
                Activate?.Invoke(Id);
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
                            controller.OnActivate();
                    }

                    _enabledControllers.Clear();
                }
            }
        }

        private void OnDeactivate(Guid id)
        {
            if (_toggleIsImRoot)
            {
                OnDeactivate();
            }
            if (_toggleRootController != null)
            {
                if (_toggleRootController.Id != id)
                {
                    _toggleRootController.OnActivate();
                }
            }
        }
        
        public void OnDeactivate()
        {
            if (_isEnabled)
            {
                _isEnabled = false;
                Deactivate?.Invoke(_id);

                _enabledControllers = new List<(bool, BaseController)>();
                if (_baseControllers != null)
                {
                    foreach (BaseController baseController in _baseControllers)
                    {
                        _enabledControllers.Add((baseController._isEnabled, baseController));
                        baseController.OnDeactivate();
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

        public virtual void Dispose()
        {
            Dispose(true);
            // GC.SuppressFinalize(this);
        }
        
        protected void Dispose(bool disposing)
        {
            if (!_isDisposed)
            {
                if (disposing)
                {
                    // Deactivate?.Invoke(Id);
                    // Освобождаем управляемые ресурсы
                    if (_baseControllers != null)
                    {
                        foreach (BaseController baseController in _baseControllers)
                        {
                            if(this != baseController)
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
                    
                    
                    // Disposable?.Invoke(Id);
                    _subscriptions?.Dispose();
                    Create = null;
                    // Disposable = null;
                    Activate = null;
                    Deactivate = null;
                }
                // освобождаем неуправляемые объекты
                _isDisposed = true;
                
            }
        }
        
        // ~BaseController()
        // {
        //     Dispose(false);
        // }
    }
}