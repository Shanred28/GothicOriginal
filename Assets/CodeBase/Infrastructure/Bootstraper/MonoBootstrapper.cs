using UnityEngine;

namespace CodeBase.Infrastructure.Bootstraper
{
    public abstract class MonoBootstrapper : MonoBehaviour
    {
        public abstract void OnBindResolved();
    }
}