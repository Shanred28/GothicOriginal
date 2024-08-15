using CodeBase.Infrastructure.ProjectContext;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace CodeBase.Infrastructure.SceneContext
{
    public class SceneContext : LifetimeScope
    {
        protected override void Awake()
        {
            
            ProjectContextFactory.TryCreate();

            //ProjectContext.InjectToInstallers(monoInstallers);
            Debug.Log("SceneContext: Bind");
            //InstallBindings();
            base.Awake();
            //ProjectContext.InjectToAllMonoBehaviour();
          //  Debug.Log("SceneContext: Init");
           // OnBindResolved();
        }
        
        protected override void Configure(IContainerBuilder builder)
        {
            Debug.Log("SceneContext: Configure");
            RegisterLevelServices(builder);
            RegisterLevelStateMachine(builder);
        }

        protected virtual void RegisterLevelServices(IContainerBuilder builder)
        {
            
        }

        protected virtual void RegisterLevelStateMachine(IContainerBuilder builder)
        {
            
        }
        
    }
}

