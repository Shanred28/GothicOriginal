using CodeBase.UI.MainUI;
using UnityEngine;

namespace CodeBase.Configs.WindowsConfig
{
    [CreateAssetMenu(fileName = "WindowConfig", menuName = "CodeBase/WindowConfig", order = 0)]
    public class WindowConfig : ScriptableObject
    {
        public WindowMainUIId windowId;
        public string title;
        public GameObject prefab;
    }
}