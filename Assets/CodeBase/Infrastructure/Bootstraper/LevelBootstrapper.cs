using CodeBase.Configs.Interface;
using CodeBase.Configs.Scene;
using CodeBase.Services.StateMachine.LevelStateMachine.Interface;
using CodeBase.Services.StateMachine.LevelStateMachine.LevelState;
using UnityEngine.SceneManagement;
using VContainer;
using VContainer.Unity;

namespace CodeBase.Infrastructure.Bootstraper
{
    public class LevelBootstrapper : MonoBootstrapper,IStartable
    {
        private ILevelStateSwitcher _levelStateSwitcher;
        private IConfigsProvider _configsProvider;
        
        private LevelBootstrapMainMenuState _levelBootstrapMainMenuState;
        private LevelBootstrapMainSceneState _levelBootstrapMainSceneState;
        
        [Inject]
        public void Constructor( ILevelStateSwitcher levelStateSwitcher, LevelBootstrapMainMenuState levelBootstrapMainMenuState, LevelBootstrapMainSceneState levelBootstrapMainSceneState, IConfigsProvider configsProvider)
        {
            _levelStateSwitcher = levelStateSwitcher;
            _levelBootstrapMainMenuState = levelBootstrapMainMenuState;
            _levelBootstrapMainSceneState =levelBootstrapMainSceneState;
            _configsProvider = configsProvider;
        }
        
        void IStartable.Start()
        {
            OnBindResolved();
        }

        public override void OnBindResolved()
        {
            InitLevelStateMachine();
        }

        private void InitLevelStateMachine()
        {
            var configs = _configsProvider.GetSceneConfig(SceneManager.GetActiveScene().name);

            switch (configs.sceneType)
            {
                case SceneTypes.MainMenu:
                    _levelStateSwitcher.AddState(_levelBootstrapMainMenuState);
                    _levelStateSwitcher.EnterState<LevelBootstrapMainMenuState>();
                    break;
                case SceneTypes.MainScene:
                    _levelStateSwitcher.AddState(_levelBootstrapMainSceneState);
                    _levelStateSwitcher.EnterState<LevelBootstrapMainSceneState>();
                    break;
            }
        }
    }
}