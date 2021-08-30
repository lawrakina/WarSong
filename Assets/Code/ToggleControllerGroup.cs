using System;
using System.Collections.Generic;


namespace Code
{
    public class ToggleControllerGroup : BaseController
    {
        #region Fields

        private readonly List<BaseController> _listToggle = new List<BaseController>();
        private BaseController _rootController;

        #endregion


        #region ClassLiveCycles

        public void Init()
        {
            foreach (var controller in _listToggle)
            {
                controller.On += On;
                controller.Off += Off;
                // controller.ActionDispose += DisposeExecute;
            }
        }

        protected override void OnDispose()
        {
            base.OnDispose();

            foreach (var controller in _listToggle)
            {
                controller.On -= On;
                controller.Off -= Off;
                // controller.ActionDispose -= DisposeExecute;
            }

            _listToggle.Clear();
        }

        #endregion


        #region Methods

        public void Add(BaseController controller)
        {
            _listToggle.Add(controller);
        }
        
        private new void On(Guid id)
        {
            if (_rootController != null)
            {
                if(_rootController.Id != id)
                    _rootController.OffExecute();
            }

            foreach (var controller in _listToggle)
            {
                if (controller.Id != id)
                    controller.OffExecute();
            }
        }
        
        private new void Off(Guid id)
        {
            if (_rootController != null)
            {
                if(id != _rootController.Id)
                    _rootController.OnExecute();
            }
        }

        private void DisposeExecute(Guid id)
        {
            foreach (var controller in _listToggle)
            {
                if (controller.Id == id)
                    _listToggle.Remove(controller);
            }
        }

        #endregion


        public void SetRoot(BaseController rootController)
        {
            _rootController = rootController;
        }
    }
}