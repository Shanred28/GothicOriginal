using CodeBase.UI.MainUI;
using UnityEngine;

namespace CodeBase.Configs.WindowsConfig
{
    [CreateAssetMenu(fileName = "WindowConfig", menuName = "Configs/WindowConfig", order = 0)]
    public class WindowConfig : ScriptableObject
    {
        public WindowMainUIId windowId;
        public string title;
        public GameObject prefab;
    }
}