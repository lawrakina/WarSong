using Windows;


namespace Controller.Model
{
    public class ListOfPositionCharInMenuModel
    {
        private readonly SceneWindows _windows;
        public SceneWindows Windows => _windows;

        public ListOfPositionCharInMenuModel( SceneWindows windows)
        {
            _windows = windows;
        }
    }
}