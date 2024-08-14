using CodeBase.Services.Scene;
using CodeBase.Services.StateMachine.Common.Interface;
using CodeBase.Services.StateMachine.GameStateMachine.Interface;
using CodeBase.Services.WindowsProvider;
using CodeBase.UI.MainUI;

namespace CodeBase.Services.StateMachine.GameStateMachine.GameState
{
    public class LoadMainMenuGameState : IEnterableState, IService
    {
        private readonly ISceneLoader _sceneLoader;
        private readonly IWindowsProvider _windowsProvider;

        public LoadMainMenuGameState(ISceneLoader sceneLoader,IWindowsProvider windowsProvider)
        {
            _sceneLoader = sceneLoader;
            _windowsProvider = windowsProvider;
        }
        
        public void Enter()
        {
            _sceneLoader.Load(Constant.MainMenuSceneName, onLoaded: () => _windowsProvider.OpenWindow(WindowMainUIId.MainMenuWindow));
        }
    }
}

