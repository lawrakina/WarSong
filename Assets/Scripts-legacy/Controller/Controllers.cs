using System.Collections.Generic;
using Interface;


namespace Controller
{
    public sealed class Controllers : IInitialization, IExecute, IFixedExecute, ILateExecute, ICleanup
    {
        #region ctor

        public Controllers()
        {
            _initializeControllers = new List<IInitialization>();
            _executeControllers = new List<IExecute>();
            _lateControllers = new List<ILateExecute>();
            _fixedControllers = new List<IFixedExecute>();
            _cleanupControllers = new List<ICleanup>();
        }

        #endregion


        #region Fields

        private readonly List<IInitialization> _initializeControllers;
        private readonly List<IExecute> _executeControllers;
        private readonly List<IFixedExecute> _fixedControllers;
        private readonly List<ILateExecute> _lateControllers;
        private readonly List<ICleanup> _cleanupControllers;

        #endregion


        #region Methods

        public Controllers Add(IController controller)
        {
            if (controller is IInitialization initializeController) _initializeControllers.Add(initializeController);

            if (controller is IExecute executeController) _executeControllers.Add(executeController);

            if (controller is IFixedExecute fixedExecuteController) _fixedControllers.Add(fixedExecuteController);

            if (controller is ILateExecute lateExecuteController) _lateControllers.Add(lateExecuteController);

            if (controller is ICleanup cleanupController) _cleanupControllers.Add(cleanupController);

            return this;
        }

        public void Init()
        {
            for (var index = 0; index < _initializeControllers.Count; ++index)
                _initializeControllers[index].Init();
        }

        public void Execute(float deltaTime)
        {
            for (var index = 0; index < _executeControllers.Count; ++index)
                _executeControllers[index].Execute(deltaTime);
        }

        public void FixedExecute(float deltaTime)
        {
            for (var index = 0; index < _fixedControllers.Count; ++index)
                _fixedControllers[index].FixedExecute(deltaTime);
        }

        public void LateExecute(float deltaTime)
        {
            for (var index = 0; index < _lateControllers.Count; ++index) 
                _lateControllers[index].LateExecute(deltaTime);
        }

        public void Cleanup()
        {
            for (var index = 0; index < _cleanupControllers.Count; ++index) 
                _cleanupControllers[index].Cleanup();
        }

        #endregion
    }
}