using CodeBase.Configs.Interface;
using CodeBase.Configs.WindowsConfig;
using CodeBase.Services.Factory.UIFactory.Interface;
using CodeBase.UI.MainUI;

namespace CodeBase.Services.WindowsProvider
{
    public class WindowsProvider : IWindowsProvider
    {
        private readonly IUIFactory _uiFactory;
        private readonly IConfigsProvider _configsProvider;

        public WindowsProvider(IUIFactory uiFactory, IConfigsProvider configsProvider)
        {
            _uiFactory = uiFactory;
            _configsProvider = configsProvider;
        }

        public void OpenWindow(WindowMainUIId windowId)
        {
            if (_uiFactory.UIRoot == null)
            {
                _uiFactory.CreateUIRoot();
            }
            
            WindowConfig windowConfig = _configsProvider.GetWindowConfig(windowId);

            switch (windowId)
            {
                /*case WindowId.VictoryWindow or WindowId.LoseWindow:
                    _uiFactory.CreateLevelResultWindows(windowConfig);
                    break;*/
                case WindowMainUIId.MainMenuWindow:
                    _uiFactory.CreateMainMenuWindows(windowConfig);
                    break;
            }
        }
    }
}