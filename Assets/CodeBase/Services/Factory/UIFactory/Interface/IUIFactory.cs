using CodeBase.Configs.WindowsConfig;
using CodeBase.UI.MainUI;
using UnityEngine;

namespace CodeBase.Services.Factory.UIFactory.Interface
{
    public interface IUIFactory : IService
    {
        Transform UIRoot { get; set; }
        public MainMenuPresenter CreateMainMenuWindows(WindowConfig windowConfig);
        public void CreateUIRoot();
    }
}