using CodeBase.Services.StateMachine.GameStateMachine.GameState;
using CodeBase.Services.StateMachine.GameStateMachine.Interface;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace CodeBase.Infrastructure.Bootstraper
{
    public class GameBootstrapper : MonoBootstrapper,IStartable
    {
        private  IGameStateSwitcher _gameStateSwitcher;
        private  BootstrapGameState _bootstrapGameState;
        private  LoadNextLevelGameState _loadNextLevelGameState;
        private LoadMainMenuGameState _loadMainMenuGameState;
        
        [Inject]
        public void Constructor(IGameStateSwitcher gameStateSwitcher, BootstrapGameState bootstrapGameState, LoadNextLevelGameState loadNextLevelGameState,LoadMainMenuGameState loadMainMenuGameState)
        {
            _gameStateSwitcher = gameStateSwitcher;
            _bootstrapGameState = bootstrapGameState;
            _loadNextLevelGameState = loadNextLevelGameState;
            _loadMainMenuGameState = loadMainMenuGameState;
            Debug.Log("Constructor inject");
        }

        void IStartable.Start()
        {
            Debug.Log("Start bootstrapper");
            OnBindResolved();
        }
        
        public override void OnBindResolved()
        {
            InitGameStateMachine();
        }

        private void InitGameStateMachine()
        {
            _gameStateSwitcher.AddState(_bootstrapGameState);
            _gameStateSwitcher.AddState(_loadNextLevelGameState);
            _gameStateSwitcher.AddState(_loadMainMenuGameState);
            
            _gameStateSwitcher.EnterState<BootstrapGameState>();
        }
    }
}