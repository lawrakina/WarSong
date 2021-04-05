using System.Collections.Generic;
using Windows;
using Interface;


namespace Controller
{
    public sealed class WindowManager : IInitialization
    {
        private readonly UiWindows _windows;
        private readonly List<UiWindow> _list = new List<UiWindow>();
        private readonly List<ToggleWindowGroup> _listToggleGroup = new List<ToggleWindowGroup>();

        public WindowManager(UiWindows windows)
        {
            _windows = windows;
        }

        public void Add(ToggleWindowGroup toggleWindowGroup)
        {
            _listToggleGroup.Add(toggleWindowGroup);
        }

        public void Add(UiWindow window, bool active = false)
        {
            window._showBeforeStart = active;
            _list.Add(window);
        }

        public void Initialization()
        {
            _windows.Init();

            foreach (var group in _listToggleGroup)
            {
                group.Init();
            }

            foreach (var window in _list)
            {
                window.Init();
            }
        }
    }
}