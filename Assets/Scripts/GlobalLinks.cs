using Controller;
using Unit.Player;


public static class GlobalLinks
{
    private static MainController _mainController;
    public static IPlayerView Player => _mainController._player;

    public static void SetLinkToRoot(MainController mainController)
    {
        _mainController = mainController;
    }
}