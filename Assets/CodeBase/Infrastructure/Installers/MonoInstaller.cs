using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace CodeBase.Infrastructure.Installers
{
    public abstract class MonoInstaller : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            
        }

        public virtual void InstallBindings() { }
    }
}