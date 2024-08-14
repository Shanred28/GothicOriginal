using System;

namespace CodeBase.Services.Scene
{
    public interface ISceneLoader : IService
    {
        public void Load(string sceneName, Action onLoaded = null);
    }
}

