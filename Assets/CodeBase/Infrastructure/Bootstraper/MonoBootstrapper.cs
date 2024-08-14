using UnityEngine;
using VContainer.Unity;

namespace CodeBase.Infrastructure.Bootstraper
{
    public abstract class MonoBootstrapper : MonoBehaviour
    {
        public abstract void OnBindResolved();
    }
}