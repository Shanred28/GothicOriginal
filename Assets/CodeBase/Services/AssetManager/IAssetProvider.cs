using UnityEngine;

namespace CodeBase.Services.AssetManager
{
    public interface IAssetProvider : IService
    {
        T GetPrefab<T>(string prefabPath) where T : Object;
        T Instatiate<T>(string prefabPath) where T : Object;
    }
}