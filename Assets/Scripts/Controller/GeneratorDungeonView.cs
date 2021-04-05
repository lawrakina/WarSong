using Windows;
using UnityEngine;


namespace Controller
{
    public class GeneratorDungeonView 
    {
        private readonly Transform _rootParent;
        private readonly BattleWindow _window;
        public Transform Root => _rootParent;

        public GeneratorDungeonView(BattleWindow window)
        {
            _window = window;
            _rootParent = _window._sceneRootObject.transform;
        }
    }
}