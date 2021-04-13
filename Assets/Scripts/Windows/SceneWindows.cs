using System;
using UnityEngine;


namespace Windows
{
    [Serializable] public class SceneWindows
    {
        [Header("Battle window")]
        [SerializeField]
        private GameObject _rootBattleWindow;
        [SerializeField]
        private Camera _rootBattleCamera;

        [SerializeField]
        private Transform _rootDungeonTransform;
        
        [Header("Character window")]
        [SerializeField]
        private GameObject _rootCharacterWindow;

        [SerializeField]
        private Camera _rootCharCamera;

        [SerializeField]
        private Transform _rootCharSpawnPoint;

        [SerializeField]
        private GameObject _rootCharEnvironment;
        
        [Header("Tavern window")]
        [SerializeField]
        private GameObject _rootTavernWindow;

        [Header("Shop window")]
        [SerializeField]
        private GameObject _rootShopWindow;



        public GameObject RootBattleWindow => _rootBattleWindow;
        public Camera RootBattleCamera => _rootBattleCamera;

        public GameObject RootCharacterWindow => _rootCharacterWindow;
        public Camera RootCharacterCamera => _rootCharCamera;
        public Transform RootCharacterSpawnPoint => _rootCharSpawnPoint;
        public GameObject RootCharacterEnvironment => _rootCharEnvironment;

        public GameObject RootTavernWindow => _rootTavernWindow;
        public GameObject RootShopWindow => _rootShopWindow;
        public Transform RootDungeonTransform => _rootDungeonTransform;
    }
}