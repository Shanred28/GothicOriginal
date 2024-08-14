using CodeBase.Configs;
using CodeBase.Configs.Interface;
using CodeBase.Infrastructure.Bootstraper;
using CodeBase.Services.CoroutineRunner;
using CodeBase.Services.Factory.EntityFactory;
using CodeBase.Services.Factory.EntityFactory.Interface;
using CodeBase.Services.GameStateMachine;
using CodeBase.Services.WindowsProvider;
using VContainer;
using VContainer.Unity;
using CodeBase.Services.Scene;
using CodeBase.Services.StateMachine.GameStateMachine.GameState;
using CodeBase.Services.StateMachine.GameStateMachine.Interface;
using UnityEngine;

namespace CodeBase.Infrastructure.Installers
{
    public class GamesInstaller : LifetimeScope
    {
        [SerializeField] private GameBootstrapper bootstrap;

        protected override void Configure(IContainerBuilder builder)
        {
            RegisterGameServices(builder);
            RegisterGameStateMachine(builder);
        }

        private void RegisterGameServices(IContainerBuilder builder)
        {
            builder.Register<IWindowsProvider,WindowsProvider>(Lifetime.Singleton);
            builder.Register<ISceneLoader,SceneLoader>(Lifetime.Singleton);
            builder.Register<ICoroutineRunner, CoroutineRunner>(Lifetime.Singleton);
            builder.Register<IConfigsProvider,ConfigsProvider>(Lifetime.Singleton);
            builder.Register<IGameFactory,GameFactory>(Lifetime.Singleton);
            
        }
        
        private void RegisterGameStateMachine(IContainerBuilder builder)
        {
            builder.Register<IGameStateSwitcher, GameStateMachine>(Lifetime.Singleton);
            builder.Register<BootstrapGameState>(Lifetime.Singleton);
            builder.Register<LoadNextLevelGameState>(Lifetime.Singleton);
            builder.Register<LoadMainMenuGameState>(Lifetime.Singleton);
        }
    }
}