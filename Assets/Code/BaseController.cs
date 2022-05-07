using System;
using System.Collections.Generic;
using Code.Extension;
using UniRx;
using UnityEngine;
using Object = UnityEngine.Object;


namespace Code
{
    /// <summary>
    /// Базовый класс для всех контроллеров.
    /// </summary>
    public abstract class BaseController : IController, IDisposable
    {
        // Activated
        // Created
        // Deactivated
        private Guid _id = Guid.NewGuid();
        private bool _isDisposed = false;
        private bool _isEnabled = false;
        
        /// <summary>
        /// публичные события, можно подписаться
        /// </summary>
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

        /// <summary>
        /// Родительский конструктор, вызывается самым первым, до создания всех объектов.
        /// Вызывает всех подписавшихся на event Create
        /// </summary>
        /// <param name="activate">Показать после создания зависимые вьюшки?</param>
        protected BaseController(bool activate = false)
        {
            Controllers.Add(this);
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
        
        /// <summary>
        /// Вызывать в конструкторе после добавления всех дочерних контроллеров и GameObject`ов
        /// </summary>
        /// <param name="activate">Показать дочерние объекты?</param>
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

        
        /// <summary>
        /// Добавление контроллера в список дочерних,
        /// если isToggle то активен только один из детей
        /// если isRoot то при отключении всех детей активируется rootController
        /// rootController может быть только один
        /// можно добавить самого себя AddController(this,true,true);
        /// </summary>
        /// <param name="baseController">добавляемый контроллер</param>
        /// <param name="isToggle">включение и добавление к Toggle Group</param>
        /// <param name="isRoot">корневой контроллер, включается когда выключаются все дочерние</param>
        protected void AddAsManagedController(BaseController baseController, bool isToggle = false, bool isRoot = false)
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

        /// <summary>
        /// Добавление GameObject`s, оспользуется "в основном" для View
        /// </summary>
        /// <param name="gameObject">зависивый GameObject</param>
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
        
        /// <summary>
        /// Активировать себя и ранее включенное дерево объектов и дерево контроллеров
        /// </summary>
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
        
        /// <summary>
        /// Отключить себя, дерево дочерних элементов и дерево зависимых контроллеров
        /// </summary>
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

        /// <summary>
        /// Выпилиться из памяти
        /// </summary>
        public virtual void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
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
                Controllers.Remove(this);
            }
        }
        
        ~BaseController()
        {
            Dispose(false);
        }
    }
}