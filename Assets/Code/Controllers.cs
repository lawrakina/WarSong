using System;
using System.Collections;
using System.Collections.Generic;
using Code.Extension;
using Code.TimeRemaining;
using Code.UI;


namespace Code
{
    public static class Controllers{
        static Controllers(){
            _initializeControllers = new List<IInitialization>();
            _executeControllers = new List<IExecute>{new TimeRemainingController()};
            _lateControllers = new List<ILateExecute>();
            _fixedControllers = new List<IFixedExecute>();
            _disposablesControllers = new List<IDisposable>();
        }

        private static readonly List<IInitialization> _initializeControllers;
        private static readonly List<IExecute> _executeControllers;
        private static readonly List<IFixedExecute> _fixedControllers;
        private static readonly List<ILateExecute> _lateControllers;
        private static readonly List<IDisposable> _disposablesControllers;
        
        public static Root RootMonoBehaviour{ get; set; }

        public static void Add(IController controller){
            if (controller is IInitialization initializeController){
                Dbg.Log($"Controller |{controller}| added, is type: IInitialisation");
                _initializeControllers.Add(initializeController);
            }

            if (controller is IExecute executeController){
                Dbg.Log($"Controller |{controller}| added, is type: IExecute");
                _executeControllers.Add(executeController);
            }

            if (controller is IFixedExecute fixedExecuteController){
                Dbg.Log($"Controller |{controller}| added, is type: IFixedExecute");
                _fixedControllers.Add(fixedExecuteController);
            }

            if (controller is ILateExecute lateExecuteController){
                Dbg.Log($"Controller |{controller}| added, is type: ILateExecute");
                _lateControllers.Add(lateExecuteController);
            }
            
            if (controller is IDisposable disposableController){
                Dbg.Log($"Controller |{controller}| added, is type: IDisposable");
                _disposablesControllers.Add(disposableController);
            }
        }

        public static void Init(){
            for (var index = 0; index < _initializeControllers.Count; ++index)
                _initializeControllers[index].Init();
        }

        public static void Execute(float deltaTime){
            for (var index = 0; index < _executeControllers.Count; ++index){
                if (_executeControllers[index].IsOn)
                    _executeControllers[index].Execute(deltaTime);
            }
        }

        public static void FixedExecute(float deltaTime){
            for (var index = 0; index < _fixedControllers.Count; ++index)
                _fixedControllers[index].FixedExecute(deltaTime);
        }

        public static void LateExecute(float deltaTime){
            for (var index = 0; index < _lateControllers.Count; ++index)
                _lateControllers[index].LateExecute(deltaTime);
        }

        public static void Dispose(){
            for (var index = 0; index < _disposablesControllers.Count; ++index)
                _disposablesControllers[index].Dispose();
        }

        public static void Remove(IController controller){
            for (var index = 0; index < _executeControllers.Count; ++index){
                if (_executeControllers[index].Id == controller.Id)
                    _executeControllers.RemoveAt(index);
            }

            for (var index = 0; index < _fixedControllers.Count; ++index){
                if (_fixedControllers[index].Id == controller.Id)
                    _fixedControllers.RemoveAt(index);
            }

            for (var index = 0; index < _lateControllers.Count; ++index){
                if (_lateControllers[index].Id == controller.Id)
                    _lateControllers.RemoveAt(index);
            }
        }

        public static void StartCoroutine(IEnumerator obj){
            RootMonoBehaviour.StartCoroutine(obj);
        }

        public static void StopAllCoroutines(){
            RootMonoBehaviour.StopAllCoroutines();
        }
    }
}