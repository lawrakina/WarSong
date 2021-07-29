using Controller;
using Unit.Player;
using UnityEngine;


public static class GlobalLinks
{
    private static MainController _mainController;
    public static IPlayerView Player => _mainController._player;
    public static Transform Root => _mainController.transform.root;

    public static void SetLinkToRoot(MainController mainController)
    {
        _mainController = mainController;
    }
}