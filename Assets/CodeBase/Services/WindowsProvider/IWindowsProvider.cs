using CodeBase.UI.MainUI;

namespace CodeBase.Services.WindowsProvider
{
    public interface IWindowsProvider : IService
    {
        void OpenWindow(WindowMainUIId windowId);
    }
}