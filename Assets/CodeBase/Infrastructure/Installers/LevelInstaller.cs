using CodeBase.Infrastructure.Bootstraper;
using CodeBase.Services.StateMachine.LevelStateMachine;
using CodeBase.Services.StateMachine.LevelStateMachine.Interface;
using CodeBase.Services.StateMachine.LevelStateMachine.LevelState;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace CodeBase.Infrastructure.Installers
{
    public class LevelInstaller : SceneContext.SceneContext
    {
        protected override void RegisterLevelServices(IContainerBuilder builder)
        {
            Debug.Log("LEVEL: Install");
            builder.RegisterEntryPoint<LevelBootstrapper>();
            
        }
        
        protected override void RegisterLevelStateMachine(IContainerBuilder builder)
        {
            builder.Register<ILevelStateSwitcher, LevelStateMachine>(Lifetime.Singleton);

            builder.Register<LevelBootstrapState>(Lifetime.Singleton);
            builder.Register<LevelBootstrapMainMenuState>(Lifetime.Singleton);
            builder.Register<LevelBootstrapMainSceneState>(Lifetime.Singleton);
        }
    }
}

