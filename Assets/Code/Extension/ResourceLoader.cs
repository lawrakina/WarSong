using UnityEngine;


namespace Code.Extension
{
    public static class ResourceLoader
    {
        public static GameObject LoadPrefab(string path)
        {
            return Resources.Load<GameObject>(path);
        }
        
        public static T LoadObject<T>(string path) where T : Object
        {
            return Resources.Load<T>(path);
        }
        
        public static T InstantiateObject<T>(T prefab, Transform parent, bool worldPositionStays) where T : Object
        {
            return Object.Instantiate(prefab, parent, worldPositionStays);
        }
        
        public static T LoadAndInstantiateObject<T>(string path, Transform parent, bool worldPositionStays) where T : Object
        {
            var prefab = LoadObject<T>(path);
            return InstantiateObject(prefab, parent, worldPositionStays);
        }
    }
}