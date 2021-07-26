using System;
using System.Collections.Generic;
using Code.Extension;


namespace Code
{
    public class ToggleControllerGroup : BaseController
    {
        #region Fields

        private readonly List<BaseController> _listToggle = new List<BaseController>();

        #endregion


        #region ClassLiveCycles

        public void Init()
        {
            foreach (var controller in _listToggle)
            {
                controller.On += On;
            }
        }

        protected override void OnDispose()
        {
            base.OnDispose();

            foreach (var controller in _listToggle)
            {
                controller.On -= On;
            }

            _listToggle.Clear();
        }

        #endregion


        #region Methods

        public void Add(BaseController controller)
        {
            _listToggle.Add(controller);
        }
        
        private void On(Guid id)
        {
            foreach (var controller in _listToggle)
            {
                if (controller.Id != id)
                    controller.OffExecute();
            }
        }

        #endregion
    }
}