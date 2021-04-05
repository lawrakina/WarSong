using Windows;


namespace Controller
{
    public sealed class ListCharacterView
    {
        private readonly CharacterWindow _window;
        public CharacterWindow CharacterWindow => _window;

        public ListCharacterView(CharacterWindow window)
        {
            _window = window;
        }
    }
}