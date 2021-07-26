using System;
using System.Collections.Generic;
using Windows;


namespace Gui
{
    public class ToggleWindowGroup
    {
        #region Fields

        private readonly List<UiWindow> _listToggle = new List<UiWindow>();

        #endregion


        #region ClassLiveCycles

        public void Init()
        {
            foreach (var window in _listToggle)
            {
                window.Init();

                window.OnShow += OnShow;
            }
        }

        ~ToggleWindowGroup()
        {
            foreach (var window in _listToggle)
            {
                window.OnShow -= OnShow;
            }
        }

        private void OnShow(Guid id)
        {
            foreach (var uiWindow in _listToggle)
            {
                if (uiWindow.Id != id)
                    uiWindow.Hide();
            }
        }

        #endregion


        #region Methods

        public void Add(UiWindow window, bool active = false)
        {
            _listToggle.Add(window);
            // window._showBeforeStart = active;
        }

        #endregion
    }
}