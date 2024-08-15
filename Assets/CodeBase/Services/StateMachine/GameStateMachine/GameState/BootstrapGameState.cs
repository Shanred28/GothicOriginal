using CodeBase.Configs.Interface;
using CodeBase.Services.StateMachine.Common.Interface;
using CodeBase.Services.StateMachine.GameStateMachine.Interface;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CodeBase.Services.StateMachine.GameStateMachine.GameState
{
    public class BootstrapGameState : IEnterableState, IService
    {
        private readonly IGameStateSwitcher _gameStateSwitcher;
        private readonly IConfigsProvider _configsProvider;

        public BootstrapGameState(IGameStateSwitcher gameStateSwitcher, IConfigsProvider configsProvider)
        {
            _configsProvider = configsProvider;
            _gameStateSwitcher = gameStateSwitcher;
        }

        public void Enter()
        {
            Debug.Log("GLOBAL: Init");
            
            _configsProvider.Load();
            
            Application.targetFrameRate = 60;
            
            if (SceneManager.GetActiveScene().name == Constant.BootStrapSceneName || SceneManager.GetActiveScene().name == Constant.MainMenuSceneName)
            {
                Debug.Log("GLOBAL: LoadMainMenuGameState");
                _gameStateSwitcher.EnterState<LoadMainMenuGameState>();
            }
            
        }
    }
}