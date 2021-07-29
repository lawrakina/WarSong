using Data;
using UnityEngine;


namespace Extension
{
    public static class ResourceLoader
    {
        public static Sprite LoadSprite(string resourcePath)
        {
            return Resources.Load<Sprite>(resourcePath);
        }

        public static GameObject LoadPrefab(string resourcePath)
        {
            return Resources.Load<GameObject>(resourcePath);
        }
        
        public static T LoadObject<T>(string resourcePath) where T : Object
        {
            return Resources.Load<T>(resourcePath);
        }
        
        public static T InstantiateObject<T>(T prefab, Transform parent, bool worldPositionStays) where T : Object
        {
            return Object.Instantiate(prefab, parent, worldPositionStays);
        }
        
        public static T LoadAndInstantiateObject<T>(string resourcePath, Transform parent, bool worldPositionStays) where T : Object
        {
            var prefab = LoadObject<T>(resourcePath);
            return InstantiateObject(prefab, parent, worldPositionStays);
        }
    }
}