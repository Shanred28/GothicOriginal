using UnityEngine;

namespace CodeBase.Services.AssetManager
{
    public class AssetProvider : IAssetProvider
    {
        public T GetPrefab<T>(string prefabPath) where T : Object
        {
            return Resources.Load<T>(prefabPath);
        }

        public T Instatiate<T>(string prefabPath) where T : Object
        {
            T obj = Resources.Load<T>(prefabPath);
            return Object.Instantiate(obj);
        }
    }
}