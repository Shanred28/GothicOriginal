using System;
using System.Collections;
using CodeBase.Services.CoroutineRunner;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CodeBase.Services.Scene
{
    public class SceneLoader : ISceneLoader
    {
        private readonly ICoroutineRunner _coroutineRunner;

        public SceneLoader(ICoroutineRunner coroutineRunner)
        {
            _coroutineRunner = coroutineRunner;
        }

        public void Load(string sceneName, Action onLoaded = null)
        {
            _coroutineRunner.StartCoroutine(LoadAsync(sceneName, onLoaded));
        }

        private IEnumerator LoadAsync(string sceneName, Action onLoaded)
        {
            if (SceneManager.GetActiveScene().name == sceneName)
            {
                yield return null;
                onLoaded?.Invoke();
                yield break;
            }
        
            AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneName);

            while (asyncOperation.isDone == false)
            {
                yield return null;
            }
        
            onLoaded?.Invoke();
        }
    }
}

