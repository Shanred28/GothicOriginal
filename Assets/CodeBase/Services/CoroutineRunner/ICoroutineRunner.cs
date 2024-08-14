using System.Collections;
using UnityEngine;

namespace CodeBase.Services.CoroutineRunner
{
    public interface ICoroutineRunner : IService
    {
        public Coroutine StartCoroutine(IEnumerator coroutine);
    }
}

