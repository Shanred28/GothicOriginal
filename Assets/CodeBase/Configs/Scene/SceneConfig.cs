using UnityEngine;

namespace CodeBase.Configs.Scene
{
    [CreateAssetMenu(fileName = "SceneConfig", menuName = "Configs/SceneConfig", order = 2)]
    public class SceneConfig : ScriptableObject
    {
        public string sceneName;
        public SceneTypes sceneType;
    }
    
    public enum SceneTypes
    {
        MainMenu,
        MainScene,
        Loading,
        LoadingComplete
    }
}

