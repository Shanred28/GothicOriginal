using CodeBase.Configs;
using CodeBase.Configs.Interface;
using CodeBase.Infrastructure.Bootstraper;
using CodeBase.Services.AssetManager;
using CodeBase.Services.CoroutineRunner;
using CodeBase.Services.Factory.EntityFactory;
using CodeBase.Services.Factory.EntityFactory.Interface;
using CodeBase.Services.Factory.UIFactory;
using CodeBase.Services.Factory.UIFactory.Interface;
using CodeBase.Services.WindowsProvider;
using VContainer;
using VContainer.Unity;
using CodeBase.Services.Scene;
using CodeBase.Services.StateMachine.GameStateMachine;
using CodeBase.Services.StateMachine.GameStateMachine.GameState;
using CodeBase.Services.StateMachine.GameStateMachine.Interface;
using CodeBase.UI.MainUI;

namespace CodeBase.Infrastructure.Installers
{
    public class GamesInstaller : ProjectContext.ProjectContext
    {
        protected override void RegisterGameServices(IContainerBuilder builder)
        {
            builder.RegisterEntryPoint<GameBootstrapper>();
            
            builder.Register<ICoroutineRunner, CoroutineRunner>(Lifetime.Singleton);
            builder.Register<ISceneLoader, SceneLoader>(Lifetime.Singleton);
            builder.Register<IAssetProvider, AssetProvider>(Lifetime.Singleton);
            builder.Register<IConfigsProvider, ConfigsProvider>(Lifetime.Singleton);
            
            builder.Register<IGameFactory, GameFactory>(Lifetime.Singleton);
            builder.Register<IUIFactory, UIFactory>(Lifetime.Singleton);
            
            builder.Register<IWindowsProvider, WindowsProvider>(Lifetime.Singleton);
            
            //TODO
            builder.Register<MainMenuPresenter>(Lifetime.Singleton);
        }

        protected override void RegisterGameStateMachine(IContainerBuilder builder)
        {
            builder.Register<IGameStateSwitcher, GameStateMachine>(Lifetime.Singleton);
            builder.Register<BootstrapGameState>(Lifetime.Singleton);
            builder.Register<LoadNextLevelGameState>(Lifetime.Singleton);
            builder.Register<LoadMainMenuGameState>(Lifetime.Singleton);
        }
    }
}