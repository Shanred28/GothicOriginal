using CodeBase.Configs.Interface;
using CodeBase.Services.StateMachine.GameStateMachine.GameState;
using CodeBase.Services.StateMachine.GameStateMachine.Interface;
using CodeBase.UI.Common;
using CodeBase.UI.MainUI.Windows;

namespace CodeBase.UI.MainUI
{
    public class MainMenuPresenter : WindowsPresenterBase<MainMenuWindows>
    {
        private readonly IGameStateSwitcher _gameStateSwitcher;
        private readonly IConfigsProvider _configsProvider;
        
        private MainMenuWindows _mainMenuWindowsWindows ;

        public MainMenuPresenter(IGameStateSwitcher gameStateSwitcher, IConfigsProvider configsProvider)
        {
            _gameStateSwitcher = gameStateSwitcher;
            _configsProvider = configsProvider;
        }

        public override void SetWindow(MainMenuWindows window)
        {
            _mainMenuWindowsWindows = window;
            
            _mainMenuWindowsWindows.OnPlayButtonClicked += OnPlayButtonClicked;
            _mainMenuWindowsWindows.Cleanuped += OnCleanup;
        }

        private void OnCleanup()
        {
            _mainMenuWindowsWindows.OnPlayButtonClicked -= OnPlayButtonClicked;
            _mainMenuWindowsWindows.Cleanuped -= OnCleanup;
        }

        private void OnPlayButtonClicked()
        {
            _gameStateSwitcher.EnterState<LoadNextLevelGameState>();
        }
    }
}